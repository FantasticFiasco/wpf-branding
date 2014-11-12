using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Mono.Cecil;

namespace CopyAssemblyInformation.IL
{
    public class Writer
    {
        private readonly string fileName;

        public Writer(string fileName)
        {
            if (fileName == null)
                throw new ArgumentNullException("fileName");
            if (!File.Exists(fileName))
                throw new ArgumentException(string.Format("File '{0}' does not exist.", fileName), "fileName");

            this.fileName = fileName;
        }

        public void Write(AssemblyInformation assemblyInformation)
        {
            if (assemblyInformation == null)
                throw new ArgumentNullException("assemblyInformation");

            AssemblyDefinition assemblyDefinition = AssemblyDefinition.ReadAssembly(fileName);
            ModuleDefinition moduleDefinition = assemblyDefinition.MainModule;

            WriteAttribute<AssemblyCompanyAttribute>(assemblyDefinition, moduleDefinition, assemblyInformation.Company);
            WriteAttribute<AssemblyProductAttribute>(assemblyDefinition, moduleDefinition, assemblyInformation.Product);
            WriteAttribute<AssemblyCopyrightAttribute>(assemblyDefinition, moduleDefinition, assemblyInformation.Copyright);
            WriteAttribute<AssemblyTrademarkAttribute>(assemblyDefinition, moduleDefinition, assemblyInformation.Trademark);

            assemblyDefinition.Write(fileName);
        }

        private static void WriteAttribute<T>(
            AssemblyDefinition assemblyDefinition,
            ModuleDefinition moduleDefinition,
            string value) where T : Attribute
        {
            if (assemblyDefinition == null)
                throw new ArgumentNullException("assemblyDefinition");
            if (moduleDefinition == null)
                throw new ArgumentNullException("moduleDefinition");
            if (value == null)
                return;

            CustomAttribute attribute = GetOrCreateAttribute<T>(assemblyDefinition, moduleDefinition);

            attribute.ConstructorArguments.Clear();
            attribute.ConstructorArguments.Add(new CustomAttributeArgument(moduleDefinition.TypeSystem.String, value));
        }

        private static CustomAttribute GetOrCreateAttribute<T>(
            AssemblyDefinition assemblyDefinition,
            ModuleDefinition moduleDefinition) where T : Attribute
        {
            CustomAttribute existingAttribute = assemblyDefinition.CustomAttributes.SingleOrDefault(
                attribute => attribute.AttributeType.FullName == typeof(T).FullName);

            if (existingAttribute != null)
            {
                return existingAttribute;
            }

            ConstructorInfo constructorInfo = typeof(T).GetConstructor(new[] { typeof(string) });
            MethodReference attributeConstructor = moduleDefinition.Import(constructorInfo);

            return new CustomAttribute(attributeConstructor);
        }
    }
}
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Mono.Cecil;

namespace BrandFile.IntermediateLanguage
{
    /// <summary>
    /// Class capable of writing IL information.
    /// </summary>
    public class Writer
    {
        private readonly string fileName;

        /// <summary>
        /// Initializes a new instance of the <see cref="Writer"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file to write IL information to.</param>
        public Writer(string fileName)
        {
            if (fileName == null)
                throw new ArgumentNullException("fileName");
            if (!File.Exists(fileName))
                throw new ArgumentException(string.Format("File '{0}' does not exist.", fileName), "fileName");

            this.fileName = fileName;
        }

        /// <summary>
        /// Writes IL information.
        /// </summary>
        public void Write(Information information)
        {
            if (information == null)
                throw new ArgumentNullException("information");

            AssemblyDefinition assemblyDefinition = AssemblyDefinition.ReadAssembly(fileName);
            ModuleDefinition moduleDefinition = assemblyDefinition.MainModule;

            WriteAttribute<AssemblyCompanyAttribute>(assemblyDefinition, moduleDefinition, information.Company);
            WriteAttribute<AssemblyProductAttribute>(assemblyDefinition, moduleDefinition, information.Product);
            WriteAttribute<AssemblyCopyrightAttribute>(assemblyDefinition, moduleDefinition, information.Copyright);
            WriteAttribute<AssemblyTrademarkAttribute>(assemblyDefinition, moduleDefinition, information.Trademark);

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
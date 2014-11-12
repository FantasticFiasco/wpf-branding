using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Mono.Cecil;

namespace CopyAssemblyInformation.IL
{
    public class Reader
    {
        private readonly string fileName;

        public Reader(string fileName)
        {
            if (fileName == null)
                throw new ArgumentNullException("fileName");
            if (!File.Exists(fileName))
                throw new ArgumentException(string.Format("File '{0}' does not exist.", fileName), "fileName");

            this.fileName = fileName;
        }

        public AssemblyInformation Read()
        {
            AssemblyDefinition assemblyDefinition = AssemblyDefinition.ReadAssembly(fileName);

            var assemblyInformation = new AssemblyInformation
            {
                Company = ReadAttribute<AssemblyCompanyAttribute>(assemblyDefinition),
                Product = ReadAttribute<AssemblyProductAttribute>(assemblyDefinition),
                Title = ReadAttribute<AssemblyTitleAttribute>(assemblyDefinition),
                Description = ReadAttribute<AssemblyDescriptionAttribute>(assemblyDefinition),
                Copyright = ReadAttribute<AssemblyCopyrightAttribute>(assemblyDefinition),
                Configuration = ReadAttribute<AssemblyConfigurationAttribute>(assemblyDefinition),
                Trademark = ReadAttribute<AssemblyTrademarkAttribute>(assemblyDefinition)
            };

            return assemblyInformation;
        }

        private static string ReadAttribute<T>(AssemblyDefinition assemblyDefinition) where T : Attribute
        {
            if (assemblyDefinition == null)
                throw new ArgumentNullException("assemblyDefinition");

            CustomAttribute existingAttribute = assemblyDefinition.CustomAttributes.SingleOrDefault(
                attribute => attribute.AttributeType.FullName == typeof(T).FullName);

            if (existingAttribute == null)
            {
                return null;
            }

            return existingAttribute.ConstructorArguments.First().Value as string;
        }
    }
}
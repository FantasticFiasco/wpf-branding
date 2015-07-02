using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Mono.Cecil;

namespace BrandFile.IntermediateLanguage
{
    /// <summary>
    /// Class capable of reading IL information.
    /// </summary>
    public class Reader
    {
        private readonly string fileName;

        /// <summary>
        /// Initializes a new instance of the <see cref="Reader"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file to read IL information from.</param>
        public Reader(string fileName)
        {
            if (fileName == null)
                throw new ArgumentNullException("fileName");
            if (!File.Exists(fileName))
                throw new ArgumentException(string.Format("File '{0}' does not exist.", fileName), "fileName");

            this.fileName = fileName;
        }

        /// <summary>
        /// Reads IL information.
        /// </summary>
        public Information Read()
        {
            AssemblyDefinition assemblyDefinition = AssemblyDefinition.ReadAssembly(fileName);

            return new Information
            {
                Company = ReadAttribute<AssemblyCompanyAttribute>(assemblyDefinition),
                Product = ReadAttribute<AssemblyProductAttribute>(assemblyDefinition),
                Copyright = ReadAttribute<AssemblyCopyrightAttribute>(assemblyDefinition),
                Trademark = ReadAttribute<AssemblyTrademarkAttribute>(assemblyDefinition)
            };
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
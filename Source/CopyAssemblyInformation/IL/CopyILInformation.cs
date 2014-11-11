using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Mono.Cecil;

namespace CopyAssemblyInformation.IL
{
    public class CopyILInformation
    {
        private readonly string sourceFileName;
        private readonly string targetFileName;

        public CopyILInformation(string sourceFileName, string targetFileName)
        {
            if (sourceFileName == null)
                throw new ArgumentNullException("sourceFileName");
            if (targetFileName == null)
                throw new ArgumentNullException("targetFileName");
            if (!File.Exists(sourceFileName))
                throw new ArgumentException("File does not exist.", "sourceFileName");
            if (!File.Exists(targetFileName))
                throw new ArgumentException("File does not exist.", "targetFileName");

            this.sourceFileName = sourceFileName;
            this.targetFileName = targetFileName;
        }

        public void Run()
        {
            AssemblyInformation sourceAssemblyInformation = ReadAssemblyInformation();
            WriteAssemblyInformation(sourceAssemblyInformation);
        }

        private AssemblyInformation ReadAssemblyInformation()
        {
            AssemblyDefinition assemblyDefinition = AssemblyDefinition.ReadAssembly(sourceFileName);
            
            var assemblyInformation = new AssemblyInformation
            {
                Company = Read<AssemblyCompanyAttribute>(assemblyDefinition),
                Product = Read<AssemblyProductAttribute>(assemblyDefinition),
                Title = Read<AssemblyTitleAttribute>(assemblyDefinition),
                Description = Read<AssemblyDescriptionAttribute>(assemblyDefinition),
                Copyright = Read<AssemblyCopyrightAttribute>(assemblyDefinition),
                Configuration = Read<AssemblyConfigurationAttribute>(assemblyDefinition),
                Trademark = Read<AssemblyTrademarkAttribute>(assemblyDefinition)
            };
            
            return assemblyInformation;
        }

        private static string Read<T>(AssemblyDefinition assemblyDefinition) where T : Attribute
        {
            CustomAttribute existingAttribute = assemblyDefinition.CustomAttributes
                .SingleOrDefault(attribute => attribute.AttributeType.FullName == typeof(T).FullName);

            if (existingAttribute == null)
            {
                return null;
            }

            return existingAttribute.ConstructorArguments.First().Value as string;
        }

        private void WriteAssemblyInformation(AssemblyInformation sourceAssemblyInformation)
        {
            throw new NotImplementedException();
        }

        private void UpdateIfNotNull<T>(string value) where T : Attribute
        {
            if (value != null)
            {
                Update<T>(value);
            }
        }

        private void Update<T>(string value) where T : Attribute
        {
            CustomAttribute attribute = GetExistingOrCreateNew<T>();
            
            attribute.ConstructorArguments.Clear();
            attribute.ConstructorArguments.Add(new CustomAttributeArgument(module.TypeSystem.String, value));
        }

        private CustomAttribute GetExistingOrCreateNew<T>() where T : Attribute
        {
            CustomAttribute existingAttribute = assemblyDefinition.CustomAttributes
                .SingleOrDefault(attribute => attribute.AttributeType.FullName == typeof(T).FullName);

            if (existingAttribute != null)
            {
                return existingAttribute;
            }

            ConstructorInfo constructorInfo = typeof(T).GetConstructor(new[] { typeof(string) });
            MethodReference attributeConstructor = module.Import(constructorInfo);

            return new CustomAttribute(attributeConstructor);
        }

        

        private class AssemblyInformation
        {
            public string Company { get; set; }
            public string Product { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Copyright { get; set; }
            public string Configuration { get; set; }
            public string Trademark { get; set; }
        }
    }
}
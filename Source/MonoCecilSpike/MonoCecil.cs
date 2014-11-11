using System;
using System.Linq;
using System.Reflection;
using Mono.Cecil;

namespace MonoCecilSpike
{
    internal class MonoCecil
    {
        private readonly AssemblyDefinition assemblyDefinition;
        private readonly ModuleDefinition module;
        private readonly string newFileName;

        internal MonoCecil(string fileName, string newFileName)
        {
            this.newFileName = newFileName;

            assemblyDefinition = AssemblyDefinition.ReadAssembly(fileName);
            module = assemblyDefinition.MainModule;
        }

        internal void Brand(
            string company,
            string product,
            string title,
            string decription,
            string copyright,
            string configuration,
            string trademark,
            string culture,
            string version,
            string fileVersion)
        {
            SetAttribute<AssemblyCompanyAttribute>(company);
            SetAttribute<AssemblyProductAttribute>(product);
            SetAttribute<AssemblyTitleAttribute>(title);
            SetAttribute<AssemblyDescriptionAttribute>(decription);
            SetAttribute<AssemblyCopyrightAttribute>(copyright);
            SetAttribute<AssemblyConfigurationAttribute>(configuration);
            SetAttribute<AssemblyTrademarkAttribute>(trademark);
            SetAttribute<AssemblyCultureAttribute>(culture);
            SetAttribute<AssemblyVersionAttribute>(version);
            SetAttribute<AssemblyFileVersionAttribute>(fileVersion);

            assemblyDefinition.Write(newFileName);
        }

        private void SetAttribute<T>(string value) where T : Attribute
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

            ConstructorInfo constructorInfo = typeof(T)
                .GetConstructor(new[] { typeof(string) });

            MethodReference attributeConstructor = module.Import(constructorInfo);

            return new CustomAttribute(attributeConstructor);
        }
    }
}
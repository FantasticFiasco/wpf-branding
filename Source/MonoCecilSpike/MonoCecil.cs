using System;
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
            string copyright)
        {
            AddAttribute<AssemblyCompanyAttribute>(company);
            AddAttribute<AssemblyProductAttribute>(product);
            AddAttribute<AssemblyTitleAttribute>(title);
            AddAttribute<AssemblyDescriptionAttribute>(decription);
            AddAttribute<AssemblyCopyrightAttribute>(copyright);

            assemblyDefinition.Write(newFileName);
        }

        private void AddAttribute<T>(string value) where T : Attribute
        {
            ConstructorInfo constructorInfo = typeof(T)
                .GetConstructor(new[] { typeof(string) });

            MethodReference attributeConstructor = module.Import(constructorInfo);

            var attribute = new CustomAttribute(attributeConstructor);
            attribute.ConstructorArguments.Add(new CustomAttributeArgument(module.TypeSystem.String, value));

            assemblyDefinition.CustomAttributes.Add(attribute);
        }
    }
}
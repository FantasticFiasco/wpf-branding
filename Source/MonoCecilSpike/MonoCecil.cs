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

        internal void Brand(string copyright)
        {
            AddAssemblyCopyright(copyright);

            assemblyDefinition.Write(newFileName);
        }

        private void AddAssemblyCopyright(string copyright)
        {
            ConstructorInfo constructorInfo = typeof(AssemblyCopyrightAttribute)
                .GetConstructor(new[] { typeof(string) });
            
            MethodReference attributeConstructor = module.Import(constructorInfo);

            var attribute = new CustomAttribute(attributeConstructor);
            attribute.ConstructorArguments.Add(new CustomAttributeArgument(module.TypeSystem.String, copyright));

            assemblyDefinition.CustomAttributes.Add(attribute);
        }
    }
}
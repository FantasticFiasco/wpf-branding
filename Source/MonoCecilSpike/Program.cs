using System.IO;
using System.Reflection;
using Mono.Cecil;

namespace MonoCecilSpike
{
    class Program
    {
        static void Main()
        {
            // http://stackoverflow.com/questions/8388196/adding-custom-attributes-using-mono-cecil

            string fileName = Path.Combine(CurrentDirectory, "WpfBranding.exe");
            var assemblyDefinition = AssemblyDefinition.ReadAssembly(fileName);
            ModuleDefinition module = assemblyDefinition.MainModule;

            MethodReference attributeConstructor = module.Import(
                typeof(AssemblyCopyrightAttribute).GetConstructor(new[] { typeof(string) }));

            var attribute = new CustomAttribute(attributeConstructor);
            attribute.ConstructorArguments.Add(new CustomAttributeArgument(module.TypeSystem.String, "Kalle"));

            assemblyDefinition.CustomAttributes.Add(attribute);

            assemblyDefinition.Write(Path.Combine(CurrentDirectory, "WpfBranding(2).exe"));
        }

        private static string CurrentDirectory
        {
            get { return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location); }
        }
    }
}

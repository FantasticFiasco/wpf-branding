using System;
using System.IO;
using System.Reflection;
using CopyAssemblyInformation.IntermediateLanguage;
using Plossum.CommandLine;

namespace CopyAssemblyInformation
{
    class Program
    {
        private const int TextWidth = 78;

        private static CommandLineParser parser;
        
        static void Main()
        {
            var options = new Options();

            parser = new CommandLineParser(options);
            parser.Parse();

            // Print header
            Console.WriteLine(parser.UsageInfo.GetHeaderAsString(TextWidth));

            if (options.Help)
            {
                // Help was specified
                PrintUsage();
            }
            else if (parser.HasErrors)
            {
                // Arguments contains errors
                PrintError();
            }
            else
            {
                Console.WriteLine("Copying assembly information from '{0}' to '{1}'...", options.Source, options.Target);

                var copyILInformation = new CopyILInformation(options.Source, options.Target);
                copyILInformation.Run();

                // TODO: Implement

                Console.WriteLine();
            }
        }

        private static void PrintUsage()
        {
            Console.WriteLine("Usage:");
            Console.WriteLine("   {0} /source:file /target:file", ExecutingFileName);
            Console.WriteLine();
            Console.WriteLine(parser.UsageInfo.GetOptionsAsString(TextWidth));
        }

        private static void PrintError()
        {
            Console.WriteLine(parser.UsageInfo.GetErrorsAsString(TextWidth));
        }

        private static string ExecutingFileName
        {
            get
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                return Path.GetFileName(assembly.Location);
            }
        }
    }
}
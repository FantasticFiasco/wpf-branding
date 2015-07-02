using System;
using System.IO;
using System.Reflection;
using BrandFile.FileVersion;
using BrandFile.IntermediateLanguage;
using Plossum.CommandLine;

namespace BrandFile
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
                Console.WriteLine(
                    "Copying IL information from '{0}' to '{1}'...",
                    options.Source,
                    options.Target);

                new CopyIntermediateLanguageInformation(options.Source, options.Target)
                    .Run();

                Console.WriteLine(
                    "Copying file version information from '{0}' to '{1}'...",
                    options.Source,
                    options.Target);

                new CopyFileVersionInformation(options.Source, options.Target)
                    .Run();

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
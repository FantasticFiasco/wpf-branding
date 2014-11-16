using Plossum.CommandLine;

namespace BrandFile
{
    /// <summary>
    /// Class describing the arguments specified when starting this console application.
    /// </summary>
    [CommandLineManager(EnabledOptionStyles = OptionStyles.Windows)]
    public class Options
    {
        [CommandLineOption(
            Name = "?",
            Aliases = "h,help",
            Description = "Displays this help text.")]
        public bool Help { get; set; }

        [CommandLineOption(
               Name = "source",
               Description = "The name of the source file, i.e. the file to copy brand information from.",
               MinOccurs = 1)]
        public string Source { get; set; }

        [CommandLineOption(
               Name = "target",
               Description = "The name of the target file, i.e. the file to copy brand information to.",
               MinOccurs = 1)]
        public string Target { get; set; }
    }
}
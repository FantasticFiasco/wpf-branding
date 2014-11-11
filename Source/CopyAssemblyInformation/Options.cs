using Plossum.CommandLine;

namespace CopyAssemblyInformation
{
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
               Description = "The name of the source assembly, i.e. the assembly to copy information from.",
               MinOccurs = 1)]
        public string Source { get; set; }

        [CommandLineOption(
               Name = "target",
               Description = "The name of the target assembly, i.e. the assembly to copy information to.",
               MinOccurs = 1)]
        public string Target { get; set; }
    }
}
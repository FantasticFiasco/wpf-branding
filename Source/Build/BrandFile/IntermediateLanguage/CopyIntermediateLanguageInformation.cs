using System;
using System.IO;

namespace BrandFile.IntermediateLanguage
{
    /// <summary>
    /// Copies brand related IL information from one file to another. 
    /// </summary>
    public class CopyIntermediateLanguageInformation
    {
        private readonly string sourceFileName;
        private readonly string targetFileName;

        /// <summary>
        /// Initializes a new instance of the <see cref="CopyIntermediateLanguageInformation"/> class.
        /// </summary>
        /// <param name="sourceFileName">
        /// Name of the source file to copy brand information from.
        /// </param>
        /// <param name="targetFileName">
        /// Name of the target file to copy brand information to.
        /// </param>
        public CopyIntermediateLanguageInformation(string sourceFileName, string targetFileName)
        {
            if (sourceFileName == null)
                throw new ArgumentNullException("sourceFileName");
            if (targetFileName == null)
                throw new ArgumentNullException("targetFileName");
            if (!File.Exists(sourceFileName))
                throw new ArgumentException(string.Format("File '{0}' does not exist.", sourceFileName), "sourceFileName");
            if (!File.Exists(targetFileName))
                throw new ArgumentException(string.Format("File '{0}' does not exist.", targetFileName), "targetFileName");

            this.sourceFileName = sourceFileName;
            this.targetFileName = targetFileName;
        }

        /// <summary>
        /// Copies brand information.
        /// </summary>
        public void Run()
        {
            var reader = new Reader(sourceFileName);
            Information information = reader.Read();

            var writer = new Writer(targetFileName);
            writer.Write(information);
        }
    }
}
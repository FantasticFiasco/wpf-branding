using System;
using System.IO;

namespace CopyAssemblyInformation.FileVersion
{
    public class CopyFileVersionInformation
    {
        private readonly string sourceFileName;
        private readonly string targetFileName;

        public CopyFileVersionInformation(string sourceFileName, string targetFileName)
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

        public void Run()
        {
            var reader = new Reader(sourceFileName);
            Information information = reader.Read();

            var writer = new Writer(targetFileName);
            writer.Write(information);
        }
    }
}
using System;
using System.Diagnostics;
using System.IO;

namespace BrandFile.FileVersion
{
    /// <summary>
    /// Class capable of reading file version resource information.
    /// </summary>
    public class Reader
    {
        private readonly string fileName;

        /// <summary>
        /// Initializes a new instance of the <see cref="Reader"/> class.
        /// </summary>
        /// <param name="fileName">
        /// Name of the file to read file version resourse information from.
        /// </param>
        public Reader(string fileName)
        {
            if (fileName == null)
                throw new ArgumentNullException("fileName");
            if (!File.Exists(fileName))
                throw new ArgumentException(string.Format("File '{0}' does not exist.", fileName), "fileName");

            this.fileName = fileName;
        }

        /// <summary>
        /// Reads file version resource information.
        /// </summary>
        public Information Read()
        {
            FileVersionInfo fileVersionInfo = FileVersionInfo.GetVersionInfo(fileName);

            return new Information
            {
                Company = fileVersionInfo.CompanyName,
                Product = fileVersionInfo.ProductName,
                Copyright = fileVersionInfo.LegalCopyright
            };
        }
    }
}
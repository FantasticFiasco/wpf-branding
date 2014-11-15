using System;
using System.Diagnostics;
using System.IO;

namespace BrandFile.FileVersion
{
    public class Reader
    {
        private readonly string fileName;

        public Reader(string fileName)
        {
            if (fileName == null)
                throw new ArgumentNullException("fileName");
            if (!File.Exists(fileName))
                throw new ArgumentException(string.Format("File '{0}' does not exist.", fileName), "fileName");

            this.fileName = fileName;
        }

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
﻿using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;

namespace BrandFile.FileVersion
{
    /// <summary>
    /// Class capable of writing file version resource information.
    /// </summary>
    public class Writer
    {
        private const string ToolName = "verpatch.exe";
        private const string ResourceName = "BrandFile.FileVersion." + ToolName;

        private readonly string fileName;

        /// <summary>
        /// Initializes a new instance of the <see cref="Writer"/> class.
        /// </summary>
        /// <param name="fileName">
        /// Name of the file to write file version resourse information to.
        /// </param>
        public Writer(string fileName)
        {
            if (fileName == null)
                throw new ArgumentNullException("fileName");
            if (!File.Exists(fileName))
                throw new ArgumentException(string.Format("File '{0}' does not exist.", fileName), "fileName");

            this.fileName = fileName;
        }

        /// <summary>
        /// Writes file version resource information.
        /// </summary>
        public void Write(Information information)
        {
            ExtractTool();
            RunTool(information);
            DeleteTool();
        }

        private void RunTool(Information information)
        {
            // Example:
            // verpatch.exe MyFile.exe /s CompanyName "My Company" /s LegalCopyright "My Copyright" /s ProductName "My Product"
            string arguments = string.Format(
                "\"{0}\" /s CompanyName \"{1}\" /s LegalCopyright \"{2}\" /s ProductName \"{3}\"",
                fileName,
                information.Company,
                information.Copyright,
                information.Product);

            var info = new ProcessStartInfo(ToolFilePath, arguments)
            {
                CreateNoWindow = true,
                UseShellExecute = false
            };
            
            Process process = Process.Start(info);
            process.WaitForExit();
        }

        private static void ExtractTool()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            using (Stream stream = assembly.GetManifestResourceStream(ResourceName))
            using (var writer = new FileStream(ToolFilePath, FileMode.Create))
            {
                var buffer = new byte[1024];

                int bytesRead;
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    writer.Write(buffer, 0, bytesRead);
                }
            }
        }

        private static void DeleteTool()
        {
            File.Delete(ToolFilePath);
        }

        private static string ToolFilePath
        {
            get { return Path.Combine(Path.GetTempPath(), ToolName); }
        }
    }
}
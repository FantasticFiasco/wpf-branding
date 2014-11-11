using System.IO;
using System.Reflection;

namespace MonoCecilSpike
{
    class Program
    {
        // TODO: Update version
        // TODO: Update app icon
        // TODO: Make sure attribute is replaced, not only added
        static void Main()
        {
            string fileName = Path.Combine(CurrentDirectory, "WpfBranding.exe");
            string newFileName = Path.Combine(CurrentDirectory, "WpfBranding(2).exe");

            string company = "Some company";
            string product = "Some product";
            string title = "Some title";
            string description = "Some description";
            string copyright = "Some copyright";
            

            File.Copy(fileName, newFileName, true);

            //BrandResources(fileName, newFileName, copyright, company);
            var monoCecil = new MonoCecil(fileName, newFileName);
            monoCecil.Brand(company, product, title, description, copyright);
        }

        //private static void BrandResources(string fileName, string newFileName, string copyright, string company)
        //{
        //    string filename = fileName;// Path.Combine(Environment.SystemDirectory, "atl.dll");
        //    //Assert.IsTrue(File.Exists(filename));
        //    string targetFilename = newFileName;
        //    File.Copy(filename, targetFilename, true);
        //    Console.WriteLine(targetFilename);
        //    VersionResource existingVersionResource = new VersionResource();
        //    existingVersionResource.DeleteFrom(targetFilename);

        //    VersionResource versionResource = new VersionResource();
        //    versionResource.FileVersion = "1.2.3.4";
        //    versionResource.ProductVersion = "4.5.6.7";

        //    StringFileInfo stringFileInfo = new StringFileInfo();
        //    versionResource[stringFileInfo.Key] = stringFileInfo;
        //    StringTable stringFileInfoStrings = new StringTable(); // "040904b0"
        //    stringFileInfoStrings.LanguageID = 1033;
        //    stringFileInfoStrings.CodePage = 1200;
        //    //Assert.AreEqual(1033, stringFileInfoStrings.LanguageID);
        //    //Assert.AreEqual(1200, stringFileInfoStrings.CodePage);
        //    stringFileInfo.Strings.Add(stringFileInfoStrings.Key, stringFileInfoStrings);
        //    stringFileInfoStrings["ProductName"] = "ResourceLib";
        //    stringFileInfoStrings["FileDescription"] = "File updated by ResourceLib\0";
        //    stringFileInfoStrings["CompanyName"] = "Vestris Inc.";
        //    stringFileInfoStrings["LegalCopyright"] = "All Rights Reserved\0";
        //    stringFileInfoStrings["EmptyValue"] = "";
        //    stringFileInfoStrings["Comments"] = string.Format("{0}\0", Guid.NewGuid());
        //    stringFileInfoStrings["ProductVersion"] = string.Format("{0}\0", versionResource.ProductVersion);

        //    VarFileInfo varFileInfo = new VarFileInfo();
        //    versionResource[varFileInfo.Key] = varFileInfo;
        //    VarTable varFileInfoTranslation = new VarTable("Translation");
        //    varFileInfo.Vars.Add(varFileInfoTranslation.Key, varFileInfoTranslation);
        //    varFileInfoTranslation[ResourceUtil.USENGLISHLANGID] = 1300;

        //    versionResource.SaveTo(targetFilename);
        //    Console.WriteLine("Reloading {0}", targetFilename);

        //    VersionResource newVersionResource = new VersionResource();
        //    newVersionResource.LoadFrom(targetFilename);
        //    //DumpResource.Dump(newVersionResource);

        //    //Assert.AreEqual(newVersionResource.FileVersion, versionResource.FileVersion);
        //    //Assert.AreEqual(newVersionResource.ProductVersion, versionResource.ProductVersion);

        //    //var versionResource = new VersionResource();




        //    //versionResource.LoadFrom(fileName);

        //    //string key = new StringFileInfo().Key;
        //    //var stringFileInfo  = (StringFileInfo)versionResource.Resources[key];
        //    //stringFileInfo["LegalCopyright"] = copyright + "\0";
        //    //stringFileInfo["CompanyName"] = company;

        //    ////versionResource.DeleteFrom(newFileName);
        //    //versionResource.SaveTo(newFileName);


        //    ////versionResource.Resources
        //}

        private static string CurrentDirectory
        {
            get { return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location); }
        }
    }
}

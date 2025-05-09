using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace PackItSetup
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string appdata = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string appPath = $@"{appdata}\Programs\Ikakusa";
            if (!Directory.Exists(appPath))
            {
                Directory.CreateDirectory(appPath);
            }
            File.Copy($@"{Environment.CurrentDirectory}\PackIt.exe", $@"{appPath}\PackIt.exe", true);
            string path = $@"{appPath}\PackIt.exe";
            RegistryKey key1 = Registry.ClassesRoot.CreateSubKey(@"*\shell\PackIt");
            key1.SetValue("", "Pack to a folder", RegistryValueKind.ExpandString);
            key1.SetValue("Icon", path, RegistryValueKind.ExpandString);

            RegistryKey key2 = Registry.ClassesRoot.CreateSubKey(@"*\shell\PackIt\command");
            key2.SetValue("", $"\"{path}\" %1", RegistryValueKind.ExpandString);

            key2.Close();
            key1.Close();
        }
    }
}

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

            string copy = $@"{Environment.CurrentDirectory}\PackItCopy.exe";
            string move = $@"{Environment.CurrentDirectory}\PackItMove.exe";

            string path1 = $@"{appPath}\PackItCopy.exe";
            string path2 = $@"{appPath}\PackItMove.exe";

            File.Copy(copy, $@"{appPath}\PackItCopy.exe", true);
            File.Copy(move, $@"{appPath}\PackItMove.exe", true);

            RegistryKey key1 = Registry.ClassesRoot.CreateSubKey(@"AllFileSystemObjects\shell\PackItCopy");
            key1.SetValue("", "Pack to a folder (Copy)", RegistryValueKind.ExpandString);
            key1.SetValue("Icon", path1, RegistryValueKind.ExpandString);

            RegistryKey key2 = Registry.ClassesRoot.CreateSubKey(@"AllFileSystemObjects\shell\PackItCopy\command");
            key2.SetValue("", $"\"{path1}\" %1", RegistryValueKind.ExpandString);

            key2.Close();
            key1.Close();

            RegistryKey key3 = Registry.ClassesRoot.CreateSubKey(@"AllFileSystemObjects\shell\PackItMove");
            key3.SetValue("", "Pack to a folder (Move)", RegistryValueKind.ExpandString);
            key3.SetValue("Icon", path2, RegistryValueKind.ExpandString);

            RegistryKey key4 = Registry.ClassesRoot.CreateSubKey(@"AllFileSystemObjects\shell\PackItMove\command");
            key4.SetValue("", $"\"{path2}\" %1", RegistryValueKind.ExpandString);

            key4.Close();
            key3.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PackIt
{
    internal class Program
    {
        static Random random = new Random();
        static string get_random(int length)
        {
            string strs = "abcdefghijklmnopqrstuvwxyz1234567890";
            StringBuilder sb = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                int rnd = random.Next(strs.Length);
                char c = strs[rnd];
                sb.Append(c);
            }
            return sb.ToString();
        }
        static void Main(string[] args)
        {
            string outputPath = Directory.GetParent(args[0]).FullName;
            string path = Path.GetFullPath(Path.Combine(outputPath, $"new_pack"));
            Directory.CreateDirectory(path);
            foreach (string arg in args)
            {
                string name = Path.GetFileName(arg);
                File.Copy(arg, Path.GetFullPath(Path.Combine(path, name)));
            }
        }
    }
}

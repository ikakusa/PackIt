using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PackItMove
{
    internal class Program
    {
        static void Move_Directory(string source, string dest, string destName)
        {
            Directory.Move(source, Path.Combine(destName, Path.GetFileName(source)));
            var files = Directory.GetFiles(source);
            Parallel.ForEach(files, file =>
            {
                File.Move(file, Path.Combine(dest, Path.GetFileName(file)));
            });

            var directories = Directory.GetDirectories(source);
            foreach (var d in directories)
            {
                Move_Directory(d, Path.Combine(dest, Path.GetFileName(d)), dest);
            }
        }
        static void Main(string[] args)
        {
            string outputPath = Directory.GetParent(args[0]).FullName;
            string path = Path.GetFullPath(Path.Combine(outputPath, $"new_pack"));
            Directory.CreateDirectory(path);

            foreach (string arg in args)
            {
                string name = Path.GetFileName(arg);
                if (File.GetAttributes(name).HasFlag(FileAttributes.Directory))
                {
                    Move_Directory(name, Path.Combine(path, Path.GetFileName(name)), path);
                }
                else
                {
                    File.Move(arg, Path.GetFullPath(Path.Combine(path, name)));
                }
            }
        }
    }
}

﻿using System;
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

        static void Copy_Directory(string source, string dest, string destName)
        {
            Directory.CreateDirectory(Path.Combine(destName, Path.GetFileName(source)));
            var files = Directory.GetFiles(source);
            Parallel.ForEach(files, file =>
            {
                File.Copy(file, Path.Combine(dest, Path.GetFileName(file)), true);
            });

            var directories = Directory.GetDirectories(source);
            foreach (var d in directories)
            {
                Copy_Directory(d, Path.Combine(dest, Path.GetFileName(d)), dest);
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
                    Copy_Directory(name, Path.Combine(path, Path.GetFileName(name)), path);
                }
                else
                {
                    File.Copy(arg, Path.GetFullPath(Path.Combine(path, name)));
                }
            }
        }
    }
}

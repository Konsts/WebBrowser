using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DirectoryLibary.Model;

namespace DirectoryLibary
{
    public static class WorkWithDirectories
    {
        private const int _100Mb = 104857600;
        private const int _50Mb = 52428800;
        private const int _10Mb = 10485760;
        public static List<DirectoryModel> GetAllDrives()
        {
            var Directories = DriveInfo.GetDrives().Select(s => s.RootDirectory.FullName).ToList();
            List<DirectoryModel> DirectoriesList = new List<DirectoryModel>();
            foreach (var Directory in Directories)
            {
                DirectoriesList.Add(new DirectoryModel { Path = Directory, Type = ItemType.Disk });
            }

            return DirectoriesList;
        }

        public static List<DirectoryModel> GetListSubdirectoriesAndFiles(string path)
        {
            List<DirectoryModel> Directories = new List<DirectoryModel>();
            DirectoryInfo directory = new DirectoryInfo(path);
            try
            {
                foreach (var dir in directory.EnumerateDirectories())
                {
                    try
                    {
                        FileAttributes f = File.GetAttributes(dir.FullName);
                        if ((f & FileAttributes.ReparsePoint) == FileAttributes.ReparsePoint)
                            continue;
                        if ((f & FileAttributes.Hidden) == FileAttributes.Hidden)
                            continue;
                        Directories.Add(new DirectoryModel { Path = dir.FullName, Type = ItemType.Directory });
                    }
                    catch { }
                }
                foreach (var fileInfo in directory.EnumerateFiles())
                {
                    try
                    {
                        FileAttributes f = File.GetAttributes(fileInfo.FullName);
                        if ((f & FileAttributes.Hidden) == FileAttributes.Hidden)
                            continue;
                        Directories.Add(new DirectoryModel { Path = fileInfo.FullName.Replace(@"\","\\"), Type = ItemType.File });
                    }
                    catch {}
                }
            }
            catch { }
            return Directories;
        }

        public static Statistic GetStatistics(string path)
        {
            int numberOfLess10MbFiles = 0;
            int numberOfless50MbFiles = 0;
            int numberOfMore100MbFiles = 0;
            int errors = 0;

            Stack<string> dirs = new Stack<string>(20);

            if (!System.IO.Directory.Exists(path))
            {
                errors++;
                return new Statistic { Errors = 1 };
            }
            dirs.Push(path);

            while (dirs.Count > 0)
            {

                string currentDir = dirs.Pop();
                string[] subDirs;
                try
                {
                    subDirs = System.IO.Directory.GetDirectories(currentDir);
                }
                catch (Exception)
                {
                    errors++;
                    continue;
                }
                string[] files = null;
                try
                {
                    files = System.IO.Directory.GetFiles(currentDir);
                }

                catch (Exception)
                {
                    errors++;
                    continue;
                }
                foreach (var s in files)
                {
                    try
                    {
                        FileInfo f = new FileInfo(s);
                        if ((f.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                            continue;
                        if (f.Length <= _10Mb)
                        {
                            numberOfLess10MbFiles++;
                        }
                        if (f.Length >= _10Mb & f.Length <= _50Mb)
                        {
                            numberOfless50MbFiles++;
                        }
                        if (f.Length >= _100Mb)
                        {
                            numberOfMore100MbFiles++;
                        }

                    }
                    catch (Exception)
                    {
                        errors++;
                    }
                }
                foreach (string str in subDirs)
                {
                    try
                    {
                        FileAttributes f = File.GetAttributes(str);
                        if ((f & FileAttributes.ReparsePoint) == FileAttributes.ReparsePoint)
                            continue;
                        if ((f & FileAttributes.Hidden) == FileAttributes.Hidden)
                            continue;
                        dirs.Push(str);
                    }
                    catch (Exception)
                    {
                        errors++;
                        continue;
                    }
                }
            }
            return new Statistic
            {
                Less10Mb = numberOfLess10MbFiles,
                More10MbAndLess50Mb = numberOfless50MbFiles,
                More100Mb = numberOfMore100MbFiles,
                Errors = errors
            };
        }
    }
}

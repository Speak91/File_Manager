using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace ConsoleApp18
{

    class FileManager
    {
        string previousDirectory = string.Empty;
        string path = string.Empty;
        public List<string> list = new List<String>();

        public FileManager()
        {

        }

        public string FoldersAndFiles(string path) //действия над папками и файлами
        {

            try
            {
                string[] folders = Directory.GetDirectories(path);
                string[] files = Directory.GetFiles(path);
                PrintFoldersAndFiles(folders, files);
                if (folders.Length > files.Length)
                {
                    Console.SetCursorPosition(0, folders.Length + 2);
                }
                else
                {
                    Console.SetCursorPosition(0, files.Length + 2);
                }
                return previousDirectory = path;

            }
            catch (DriveNotFoundException)
            {
                Console.WriteLine("Устройство не готово");
                return "0";
            }
            catch (Exception)
            {
                Console.WriteLine("Неправильно указан путь или команда");
                return "0";
            }


        }
        public string PrintFoldersAndFiles(string[] folders, string[] files) //вывод файлов и папок
        {
            Console.Clear();
            int count = 2;
            Console.SetCursorPosition(3, 0);
            Console.WriteLine("           Папки");
            Console.SetCursorPosition(3, 1);
            Console.Write("=========================" + "                      " + "================");
            foreach (var item in folders)
            {
                Console.SetCursorPosition(3, count++);
                Console.WriteLine(item);
            }

            count = 2;
            Console.SetCursorPosition(50, 0);
            Console.WriteLine("       Файлы");

            foreach (var item in files)
            {
                Console.SetCursorPosition(50, count++);
                Console.WriteLine(item);
            }
            if (folders.Length > files.Length)
            {
                Console.SetCursorPosition(0, folders.Length + 1);
                for (int i = 0; i < 3; i++)
                {
                    Console.Write("=========================");
                }
            }
            else
            {
                Console.SetCursorPosition(0, files.Length + 1);
                for (int i = 0; i < 3; i++)
                {
                    Console.Write("=========================");
                }
            }
            return previousDirectory = path;

        }
        public void LogicalDrives()//Вывод дисков 
        {
            Console.Clear();
            DriveInfo[] logicalDrives = DriveInfo.GetDrives();
            foreach (var item in logicalDrives)
            {
                Console.WriteLine($"Диск {item.Name} ");
                switch (item.DriveType)
                {
                    case DriveType.Unknown:
                        Console.WriteLine(" Неизвестный тип диска");
                        break;
                    case DriveType.NoRootDirectory:
                        break;
                    case DriveType.Removable:
                        Console.WriteLine(" Тип диска Сьемный диск");
                        break;
                    case DriveType.Fixed:
                        Console.WriteLine(" Тип диска Жесткий диск");
                        Console.WriteLine($" Общий обьем {item.TotalSize / 1024} Кб");
                        Console.WriteLine($" Свободный обьем {item.TotalFreeSpace / 1024} Кб");
                        break;
                    case DriveType.Network:
                        Console.WriteLine(" Тип диска Сетевой диск");
                        break;
                    case DriveType.CDRom:
                        try
                        {
                            Console.WriteLine(" Тип диска СDRom или DVDRom ");
                            Console.WriteLine($" Общий обьем {item.TotalSize / 1024} Кб");
                            Console.WriteLine($" Свободный обьем {item.TotalFreeSpace / 1024} Кб");
                        }
                        catch (Exception)
                        {
                            Console.WriteLine(" Устройство не готово");
                        }

                        break;
                    case DriveType.Ram:
                        break;
                    default:
                        break;
                }


            }
            Console.WriteLine("Выберите диск");
            string selectLogicalDrive = Console.ReadLine();
            while (selectLogicalDrive == "")
            {
                Console.WriteLine(@"Неправильный путь, путь вводится в формате C:\ где C имя диска");
                selectLogicalDrive = Console.ReadLine();
            }

            FoldersAndFiles(selectLogicalDrive);

        } 

        public void DirectoryCopy(string sourceDirectoryName, string destinationDirectoryName, bool copySubDirs) //Копирование папки с файлами
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(sourceDirectoryName);
                if (!dir.Exists)
                {
                    throw new DirectoryNotFoundException(
                        "Source directory does not exist or could not be found: "
                        + sourceDirectoryName);
                }
                DirectoryInfo[] dirs = dir.GetDirectories();

                Directory.CreateDirectory(destinationDirectoryName);

                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo file in files)
                {
                    string tempPath = Path.Combine(destinationDirectoryName, file.Name);
                    file.CopyTo(tempPath, false);
                }
                if (copySubDirs)
                {
                    foreach (DirectoryInfo subdir in dirs)
                    {
                        string tempPath = Path.Combine(destinationDirectoryName, subdir.Name);
                        DirectoryCopy(subdir.FullName, tempPath, copySubDirs);
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Неправильно указан путь или команда");
            }
        } 

        public void RecurseDelete(string directoryPath) //Рекурсивное удаление
        {
            string[] directories = Directory.GetDirectories(directoryPath);
            foreach (var directory in directories)
            {
                RecurseDelete(directory);
            }
            Directory.Delete(directoryPath);
        }
        public void DeleteFile(string pathFile) //Удаление файла
        {
            try
            {
                File.Delete(pathFile);
                Console.WriteLine("Успешно удалено");

            }
            catch (Exception)
            {

                Console.WriteLine("Произошла ошибка");
            }

        }

        public void FileInfo(string pathFile) //вывод информаци о файле
        {
            Console.Clear();
            FileInfo file = new FileInfo(pathFile);
            Console.WriteLine($"Размер: {file.Length / 1024}, KB");
            Console.WriteLine($"Название: {file.Name}");
            Console.WriteLine($"Расположение: {file.FullName}");
            Console.WriteLine($"Создан: {file.CreationTimeUtc}");
        }
        public void CopyFile(string pathFile) //Копирование файла
        {
            try
            {
                FileInfo copyFile = new FileInfo(pathFile);
                Console.WriteLine("Введите путь куда необходимо скопировать файл");
                string destinationFileName = Console.ReadLine();
                File.Copy(pathFile, destinationFileName, true);
                Console.WriteLine("Успешно скопировано");
            }
            catch (Exception)
            {
                Console.WriteLine("Неправильно указан путь или такого файла не существует");
            }

        }
        public void FolderInfo(string pathFolder) //Информация о папке
        {
            int countFiles = 0;
            int countFolders = 0;
            foreach (var item in Directory.GetFiles(pathFolder))
            {
                countFiles++;
            }
            foreach (var item in Directory.GetDirectories(pathFolder))
            {
                countFolders++;
            }
            Console.WriteLine($"Расположение {Directory.GetParent(pathFolder)}");
            Console.WriteLine($"Создан {Directory.GetCreationTime(pathFolder)}");
            Console.WriteLine($"Файлов в папке {pathFolder} = {countFiles}");
            Console.WriteLine($"Папок в папке {countFolders}");
        }
        public void Help() // вывод помощи 
        {
            Console.WriteLine("ВНИМАНИЕ ВСЕ КОМАНДЫ ПИШУТСЯ НА ЛАТИНИЦЕ (АНГЛИЙСКОМ)");
            Console.WriteLine(@"Вывести список дисков ld");
            Console.WriteLine(@"Переход на диск: C:\ где С буква диска");
            Console.WriteLine(@"Копирование каталога: ls C:\folder где ls команда, C буква диска, а folder наименование папки");
            Console.WriteLine(@"Копирование файла: cp C:\source.txt ");
            Console.WriteLine(@"Переход по папкам: C:\folder где C буква диска, а folder наименование папки");
            Console.WriteLine(@"Удаление файла: rm C:\source.txt");
            Console.WriteLine(@"Удаление каталога: rd C:\folder где rd команда,C буква диска, а folder наименование папки");
            Console.WriteLine(@"Информация о файле: file C:\source.txt");
            Console.WriteLine(@"Информация о папке: folder C:\folder");

        }
    }
}

using System;
using System.IO;

namespace ConsoleApp18
{
    class Program
    {
        static void Main(string[] args)
        {
            FileManager fileManager = new FileManager();
            Console.SetBufferSize(94, 38);
            Console.SetWindowSize(94, 38);
            Console.ForegroundColor = ConsoleColor.Cyan;
            while (true)
            {
                string sourceDirectoryName; //Первоначальное расположение папки
                string directoryPath; //Путь к папке
                string pathFile; //Путь к файлу 
                string command; //ввод команды
                Console.WriteLine("Введите команду или help для справки: ");
                command = Console.ReadLine();
                string[] commands = command.Split(' ');
                try
                {
                    switch (commands[0])
                    {
                        case "ls": //Копирование 
                            sourceDirectoryName = Convert.ToString(commands[1]);
                            Console.WriteLine(@"Введите путь куда хотите скопировать в формате C:\source Где С:\ буква диска source папка");
                            string destinationDirectoryName = Convert.ToString(commands[2]);
                            fileManager.DirectoryCopy(sourceDirectoryName, destinationDirectoryName, true);
                            break;
                        case "rd": //Удаление каталога рекурсивно
                            directoryPath = Convert.ToString(commands[1]);
                            fileManager.RecurseDelete(directoryPath);
                            break;
                        case "rm": //Удаление файла
                            pathFile = Convert.ToString(commands[1]);
                            fileManager.DeleteFile(pathFile);
                            break;
                        case "ld":
                            fileManager.LogicalDrives(); //вывод дисков компьютера
                            break;
                        case "file": //Вывод информации о файле
                            try
                            {
                                pathFile = Convert.ToString(commands[1]);
                                FileInfo file = new FileInfo(pathFile);
                                Console.WriteLine($"Информация о файле {pathFile}");
                                Console.WriteLine($"Размер: {file.Length / 1024}, KB");
                                Console.WriteLine($"Название: {file.Name}");
                                Console.WriteLine($"Расположение: {file.FullName}");
                                Console.WriteLine($"Создан: {file.CreationTimeUtc}");
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Неправильно указано имя файла");
                            }
                            break;
                        case "cp": //Копирование файла
                            pathFile = Convert.ToString(commands[1]);
                            fileManager.CopyFile(pathFile);
                            break;
                        case "help": //Вывод помощи
                            fileManager.Help();
                            break;
                        case "folder": //информация о папке
                            directoryPath = Convert.ToString(commands[1]);
                            fileManager.FolderInfo(directoryPath);
                            break;
                        default:
                            fileManager.FoldersAndFiles(command);
                            break;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Неправильно указан путь или команда");
                }

            }


        }

    }
}

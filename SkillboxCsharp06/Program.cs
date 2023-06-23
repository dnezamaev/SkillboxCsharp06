using System;
using System.IO;

namespace SkillboxCsharp06
{
    internal class Program
    {
        /// <summary>
        /// Путь к файлу с данными.
        /// </summary>
        const string DataFilePath = "workers.txt";

        /// <summary>
        /// Название "столбцов" в структуре файла.
        /// </summary>
        static string[] DataColumnNames =
        {
            "ID", "Дата добавления", "Ф.И.О.", "Возраст",
            "Рост", "Дата рождения", "Место рождения"
        };

        /// <summary>
        /// Разделитель, используемый в файле.
        /// </summary>
        static string FileColumnSeparator = "#";

        static void Main(string[] args)
        {
            // Проверяем наличие файла и выводим меню.
            CheckFileExists();
            Menu();
        }

        /// <summary>
        /// Проверяет наличие файла и создает при необходимости.
        /// </summary>
        static void CheckFileExists()
        {
            // Проверяем наличие файла.
            if (!File.Exists(DataFilePath))
            {
                // Создаем файл и сразу же закрываем поток.
                File.Create(DataFilePath).Close();
                Console.WriteLine("Файл создан");
            }

        }
        
        /// <summary>
        /// Выводит меню и разбирает вывод пользователя.
        /// Передаёт обработку задачи в другие методы.
        /// </summary>
        static void Menu()
        {
            // Бесконечный цикл вывода меню.
            while (true)
            {
                Console.WriteLine("1 - вывести список сотрудников на экран");
                Console.WriteLine("2 - добавить нового сотрудника");
                Console.WriteLine("0 - выход");
                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case "0": // Выход.
                        return;
                    case "1": // Вывод списка сотрудников.
                        PrintAllWorkers();
                        break;
                    case "2": // Добавление сотрудника.
                        AddWorkerToFile();
                        break;
                    default: // Обработка несуществующего пункта меню.
                        Console.WriteLine("Неизвестный пункт меню");
                        break;
                }
            }
        }

        /// <summary>
        /// Запрашивает у пользователя информацию о сотруднике и
        /// записывает её в файл.
        /// </summary>
        static void AddWorkerToFile()
        {
            // Массив данных о новом сотрудники.
            // Размер массива совпадает с количеством столбцов в структуре файла.
            string[] dataItems = new string[DataColumnNames.Length];

            // Пробегаемся по каждому элементу структуры.
            for (int i = 0; i < dataItems.Length; i++)
            {
                // Дату добавления вводить не нужно.
                if (i == 1)
                {
                    // Она заполняется автоматически.
                    dataItems[i] = DateTime.Now.ToString();
                    continue;
                }

                // Выводим приглашение для пользователя и сохраняем введённое
                // в нужный элемент массива.
                Console.Write($"Введите {DataColumnNames[i]}: ");
                dataItems[i] = Console.ReadLine();
            }

            // Склеиваем полученные значения через разделитель.
            string totalWorkerInfo = string.Join(FileColumnSeparator, dataItems);

            // Создаем записывающий поток для файла.
            // Второй параметр означает добавление в конец файла, а не перезапись.
            // using гарантирует закрытие потока.
            using (StreamWriter writer = new StreamWriter(DataFilePath, true))
            {
                writer.WriteLine(totalWorkerInfo);
            }
        }

        /// <summary>
        /// Выводит список сотрудник из файла на экран.
        /// </summary>
        static void PrintAllWorkers()
        {
            // Создаем читающий поток для файла.
            // using гарантирует закрытие потока.
            using (StreamReader reader = new StreamReader(DataFilePath))
            {
                // Цикл чтения до конца потока.
                while (!reader.EndOfStream)
                {
                    // Читаем очередную строку из файла.
                    string line = reader.ReadLine();

                    // Разбиваем на элементы.
                    string[] dataItems = line.Split(FileColumnSeparator);

                    // Выводим информацию о сотруднике.
                    for (int i = 0; i < dataItems.Length; i++)
                    {
                        Console.WriteLine(
                            $"{DataColumnNames[i]}: {dataItems[i]}"); 
                    }

                    Console.WriteLine();
                }
            }
        }
    }

}
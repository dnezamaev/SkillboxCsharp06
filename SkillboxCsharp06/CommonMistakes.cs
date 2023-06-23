using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkillboxCsharp06
{
    internal class AbsolutePath
    {
        /// <summary>
        /// Ошибка. Использование абсолютного пути к файлу.
        /// На системе может быть запрещена запись в указанную директорию.
        /// Может не быть диска с: (например, на Linux).
        /// Может не существовать такой директории.
        /// Пользователям не нравится, когда программа создает файлы где попало.
        /// </summary>
        void Mistake()
        {
            using (StreamWriter writer = new StreamWriter(@"C:\MyCoolFile.txt")) { }
        }

        /// <summary>
        /// Решение.
        /// Использовать относительные пути. 
        /// Для этого достаточно указать просто имя файла без буквы диска.
        /// </summary>
        void Solution()
        {
            using (StreamWriter writer = new StreamWriter("MyCoolFile.txt")) { }
        }
    }

    class DuplicateFilePath
    {
        /// <summary>
        /// Ошибка. Дублирование пути к файлу.
        /// Если потребуется изменить имя файла, 
        /// нужно будет бегать по всему коду и менять его в нескольких местах.
        /// </summary>
        void Mistake()
        {
            using (StreamReader reader = new StreamReader("MyCoolFile.txt")) { }

            using (StreamWriter writer = new StreamWriter("MyCoolFile.txt")) { }

            File.ReadAllLines("MyCoolFile.txt");
        }

        /// <summary>
        /// Решение.
        /// Завести переменную или константу для пути.
        /// Передавать её в качестве параметра методам по необходимости.
        /// </summary>
        /// <param name="filePath"></param>
        void Solution(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath)) { }

            using (StreamWriter writer = new StreamWriter(filePath)) { }

            File.ReadAllLines(filePath);
        }
    }

    class FileCreateWithoutClose
    {
        /// <summary>
        /// Ошибка. Использование File.Create без закрытия потока.
        /// Этот метод создает файл и возращает поток для работы с ним.
        /// Если его не закрыть, то повторное открытие файла может вызвать ошибку.
        /// </summary>
        void Mistake(string DataFilePath)
        {
            File.Create(DataFilePath);
        }

        /// <summary>
        /// Решение.
        /// Закрывать поток сразу же после создания файла.
        /// </summary>
        void Solution1(string DataFilePath)
        {
            File.Create(DataFilePath).Close();
        }

        /// <summary>
        /// Решение.
        /// Сохранять поток в переменной для дальнейшго использования и
        /// закрывать его через using.
        /// </summary>
        void Solution2(string DataFilePath)
        {
            using (FileStream stream = File.Create(DataFilePath))
            {
                // Работа с потоком stream.
            }
        }
    }
}

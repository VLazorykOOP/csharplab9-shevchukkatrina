using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Lab9
{
    class Lab9T2
    {
        public void Run()
        {
            Console.OutputEncoding = Encoding.UTF8;
            string filePath = "students.txt";

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Файл students.txt не знайдено.");
                return;
            }

            Queue<string> passedQueue = new Queue<string>();
            Queue<string> failedQueue = new Queue<string>();

            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                bool isHeader = true;

                while ((line = reader.ReadLine()) != null)
                {
                    if (isHeader)
                    {
                        isHeader = false;
                        continue; // Пропустити заголовок
                    }

                    string[] data = line.Split(",");
                    if (data.Length < 6)
                    {
                        Console.WriteLine($"Пропущено рядок через неправильний формат: {line}");
                        continue;
                    }

                    if (!int.TryParse(data[3], out int grade1) ||
                        !int.TryParse(data[4], out int grade2) ||
                        !int.TryParse(data[5], out int grade3))
                    {
                        Console.WriteLine($"Неправильний формат оцінок у рядку: {line}");
                        failedQueue.Enqueue(line);
                        continue;
                    }

                    // Якщо є 2 - не здав, якщо лише 3 або більше - здав
                    if (grade1 == 2 || grade2 == 2 || grade3 == 2)
                        failedQueue.Enqueue(line);
                    else
                        passedQueue.Enqueue(line);
                }
            }

            Console.WriteLine("Студенти, які успішно здали сесію:");
            foreach (var student in passedQueue) Console.WriteLine(student);

            Console.WriteLine("\nСтуденти, які не здали сесію:");
            foreach (var student in failedQueue) Console.WriteLine(student);
        }
    }
}

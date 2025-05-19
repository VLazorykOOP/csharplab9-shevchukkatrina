using System;
using System.IO;
using System.Collections;
using System.Text;

namespace Lab9
{
    // Клас для порівняння студентів за прізвищем (демонстрація IComparer)
    class StudentComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            string s1 = x as string;
            string s2 = y as string;

            if (s1 == null || s2 == null)
                return 0;

            // Припустимо, прізвище — другий елемент, розділений комами
            string lastName1 = s1.Split(',')[1];
            string lastName2 = s2.Split(',')[1];

            return string.Compare(lastName1, lastName2, StringComparison.OrdinalIgnoreCase);
        }
    }

    class Lab9T4 : IEnumerable, ICloneable
    {
        private ArrayList passedList = new ArrayList();
        private ArrayList failedList = new ArrayList();

        public void Run()
        {
            Console.OutputEncoding = Encoding.UTF8;
            string filePath = "students.txt";

            if (!File.Exists(filePath))
            {
                Console.WriteLine("Файл students.txt не знайдено.");
                return;
            }

            passedList.Clear();
            failedList.Clear();

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

                    string[] data = line.Split(',');
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
                        failedList.Add(line);
                        continue;
                    }

                    if (grade1 == 2 || grade2 == 2 || grade3 == 2)
                        failedList.Add(line);
                    else
                        passedList.Add(line);
                }
            }

            // Використаємо StudentComparer для сортування
            StudentComparer comparer = new StudentComparer();
            passedList.Sort(comparer);
            failedList.Sort(comparer);

            Console.WriteLine("Студенти, які успішно здали сесію:");
            foreach (string student in passedList)
                Console.WriteLine(student);

            Console.WriteLine("\nСтуденти, які не здали сесію:");
            foreach (string student in failedList)
                Console.WriteLine(student);
        }

        // Реалізація IEnumerable для всіх студентів (пройдемося по двох списках)
        public IEnumerator GetEnumerator()
        {
            foreach (var item in passedList)
                yield return item;
            foreach (var item in failedList)
                yield return item;
        }

        // Реалізація ICloneable
        public object Clone()
        {
            Lab9T4 clone = new Lab9T4();
            clone.passedList = (ArrayList)this.passedList.Clone();
            clone.failedList = (ArrayList)this.failedList.Clone();
            return clone;
        }
    }
}

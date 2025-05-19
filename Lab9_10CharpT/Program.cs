using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Lab9
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine("Оберіть номер завдання (1-5):");
            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                switch (choice)
                {
                    case 1:
                        Lab9T1 lab9task1 = new Lab9T1();
                        lab9task1.Run();
                        break;
                    case 2:
                        Lab9T2 lab9task2 = new Lab9T2();
                        lab9task2.Run();
                        break;
                    case 3:
                        Lab9T3 lab9task3 = new Lab9T3();
                        lab9task3.Run();
                        break;
                    case 4:
                        Lab9T4 lab9task4 = new Lab9T4();
                        lab9task4.Run();
                        break;
                    case 5:
                        Lab9T5 lab9task5 = new Lab9T5();
                        lab9task5.Run();
                        break;
                    default:
                        Console.WriteLine("Невірний номер завдання.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Введено некоректне значення.");
            }
        }
    }
}

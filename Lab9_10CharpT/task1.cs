using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Lab9
{
    class Lab9T1
    {
        public void Run()
        {
            Console.OutputEncoding = Encoding.UTF8;
            try
            {
                string formula = File.ReadAllText("formula.txt");
                int result = EvaluateFormula(formula);
                Console.WriteLine($"Результат обчислення: {result}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }

        private int EvaluateFormula(string formula)
        {
            Stack<int> stack = new Stack<int>();

            for (int i = formula.Length - 1; i >= 0; i--)
            {
                char ch = formula[i];

                if (char.IsDigit(ch))
                {
                    stack.Push(ch - '0');
                }
                else if (ch == 'p' || ch == 'm')
                {
                    int a = stack.Pop();
                    int b = stack.Pop();
                    int result = 0;

                    if (ch == 'p')
                    {
                        result = (a + b) % 10;
                    }
                    else if (ch == 'm')
                    {
                        result = (a - b + 10) % 10;
                    }

                    stack.Push(result);
                }
            }

            return stack.Pop();
        }
    }
}

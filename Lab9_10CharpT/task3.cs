using System;
using System.IO;
using System.Collections;
using System.Text;

namespace Lab9
{
    class Lab9T3 : IEnumerable, ICloneable
    {
        private ArrayList stack = new ArrayList();

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
            stack.Clear();

            for (int i = formula.Length - 1; i >= 0; i--)
            {
                char ch = formula[i];

                if (char.IsDigit(ch))
                {
                    stack.Add(ch - '0');  // push
                }
                else if (ch == 'p' || ch == 'm')
                {
                    int a = (int)Pop();
                    int b = (int)Pop();
                    int result = 0;

                    if (ch == 'p')
                    {
                        result = (a + b) % 10;
                    }
                    else if (ch == 'm')
                    {
                        result = (a - b + 10) % 10;
                    }

                    stack.Add(result);
                }
            }

            return (int)Pop();
        }

        private object Pop()
        {
            if (stack.Count == 0)
                throw new InvalidOperationException("Stack is empty");

            object value = stack[stack.Count - 1];
            stack.RemoveAt(stack.Count - 1);
            return value;
        }

        // Реалізація IEnumerable
        public IEnumerator GetEnumerator()
        {
            return stack.GetEnumerator();
        }

        // Реалізація ICloneable
        public object Clone()
        {
            Lab9T3 clone = new Lab9T3();
            clone.stack = (ArrayList)this.stack.Clone();
            return clone;
        }
    }
}

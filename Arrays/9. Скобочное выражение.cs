using System;

namespace IJunior
{
    class Program
    {
        static void Main()
        {
            string text;

            char leftCurlyBrace = '(';
            char rightCurlyBrace = ')';

            int currentDepth = 0;
            int maxDepth = 0;

            Console.Write("Введите скобочное выражение: ");
            text = Console.ReadLine();

            foreach (char symbol in text)
            {
                if (symbol == leftCurlyBrace)
                {
                    currentDepth++;

                    if (maxDepth < currentDepth)
                        maxDepth = currentDepth;
                }
                else if (symbol == rightCurlyBrace)
                {
                    currentDepth--;
                        
                    if(currentDepth < 0)
                        break;
                }
            }

            if (currentDepth != 0)
                Console.WriteLine("Некорректное выражение");
            else
                Console.WriteLine($"Корректное выражение. Глубина: {maxDepth}");
        }
    }
}
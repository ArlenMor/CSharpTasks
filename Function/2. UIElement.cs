using System;

namespace IJunior
{
    internal class Program
    {
        static void Main()
        {
            int filledPercent = GetScalePercentage();

            Console.Write("Введите позицию X: ");
            int positionX = Convert.ToInt32(Console.ReadLine()) - 1;

            Console.Write("Введите позицию Y: ");
            int positionY = Convert.ToInt32(Console.ReadLine()) - 1;

            Console.Clear();

            DrawBar(positionX, positionY, filledPercent, ConsoleColor.Green);
        }

        private static int GetScalePercentage()
        {
            bool isPercentageValueValid = false;

            int minFilledPercentage = 0;
            int maxFilledPercentage = 100;

            int filledPercentage = 0;

            while(!isPercentageValueValid)
            {
                Console.Write("Введите процент закрашеного бара: ");
                string inputPercent = Console.ReadLine();

                bool isInputPercentValid = int.TryParse(inputPercent, out filledPercentage);

                if(!isInputPercentValid)
                {
                    Console.WriteLine("Можно вводить только целые числа.");
                }
                else
                {
                    if(filledPercentage < minFilledPercentage || filledPercentage > maxFilledPercentage)
                    {
                        Console.WriteLine($"Процент должен входить в диапазон от {minFilledPercentage} до {maxFilledPercentage}");
                    }
                    else
                    {
                        isPercentageValueValid = true;
                    }
                }
            }

            return filledPercentage;
        }

        private static void DrawBar(int positionX, int positionY, int filledPercent, ConsoleColor bgColor, char bgSymbol = '#', char emptySymbol = '_')
        {
            ConsoleColor defaultBgColor = Console.BackgroundColor;

            string bar;

            int maxAreaValue = 10;
            int maxPercentage = 100;
            float filledArea = (float)filledPercent / maxPercentage;
            int filledScaleValue = (int)(filledArea * maxAreaValue);

            bar = FillBar(filledScaleValue, bgSymbol);

            if (positionX < 0)
                positionX = 0;

            if (positionY < 0)
                positionY = 0;

            Console.SetCursorPosition(positionX, positionY);
            Console.Write("[");
            Console.BackgroundColor = bgColor;
            Console.Write(bar);
            Console.BackgroundColor = defaultBgColor;

            int emptyArea = maxAreaValue - filledScaleValue;

            bar = FillBar(emptyArea, emptySymbol);

            Console.Write(bar + ']');
        }

        private static string FillBar(int numberOfSymbol, char fillSymbol)
        {
            string bar = string.Empty;

            for(int i = 0; i < numberOfSymbol; i++)
                bar += fillSymbol;

            return bar;
        }
    }
}
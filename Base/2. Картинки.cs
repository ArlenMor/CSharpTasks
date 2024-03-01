using System;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            int imagesInRow = 3;
            int images = 52;

            int filledRows = images / imagesInRow;
            int numberOverflowImages = images % imagesInRow;

            Console.WriteLine($"Всего заполненных рядов: {filledRows}");
            Console.WriteLine($"Сверх меры: {numberOverflowImages}");
        }
    }
}
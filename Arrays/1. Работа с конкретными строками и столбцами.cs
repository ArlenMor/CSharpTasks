using System;

namespace IJunior
{
    internal class Program
    {
        static void Main()
        {
            int[,] matrix = new int[5, 5];

            int rowIndex = 1;
            int columnIndex = 0;
            int rowSum = 0;
            int columnProduct = 1;
            int bottomLimit = 1;
            int upLimit = 10;

            Random random = new Random();

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = random.Next(bottomLimit, upLimit);
                    Console.Write($"{matrix[i, j]} ");
                }

                Console.WriteLine();
            }

            for (int i = 0;i < matrix.GetLength(1); i++)
                rowSum += matrix[rowIndex, i];

            for(int i = 0; i < matrix.GetLength(0); i++)
                columnProduct *= matrix[i, columnIndex];

            Console.WriteLine($"Сумма {rowIndex + 1} строки = {rowSum}");
            Console.WriteLine($"Произведение {columnIndex + 1} столбца = {columnProduct}");
        }
    }
}
using System;

namespace IJunior
{
    internal class Program
    {
        static void Main()
        {
            int[,] matrix = new int[10, 10];

            int bottomLimit = 1;
            int upLimit = 10;
            int maxValueOfMatrix = int.MinValue;
            int replaceValue = 0;

            Random random = new Random();

            Console.WriteLine("Изначальная матрица: ");

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    matrix[i, j] = random.Next(bottomLimit, upLimit);
                    Console.Write($"{matrix[i, j]} ");
                }

                Console.WriteLine();
            }

            for (int i = 0; i < matrix.GetLength(0); i++)
                for (int j = 0; j < matrix.GetLength(1); j++)
                    if (maxValueOfMatrix < matrix[i, j])
                        maxValueOfMatrix = matrix[i, j];

            Console.WriteLine($"\nМаксимальный элемент матрицы: {maxValueOfMatrix}\n");
            Console.WriteLine($"Матрица, в которой {maxValueOfMatrix} заменены на {replaceValue}:");

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (maxValueOfMatrix == matrix[i, j])
                        matrix[i, j] = replaceValue;

                    Console.Write($"{matrix[i, j]} ");
                }

                Console.WriteLine();
            }
        }
    }
}
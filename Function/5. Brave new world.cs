using System;

namespace IJunior
{
    internal class Program
    {
        static void Main()
        {
            char[,] map = { {'#', '#', '#','#','#','#','#','#','#','#','#','#','#','#','#','#','#',},
                            {'#', ' ', '#',' ',' ',' ',' ','#',' ',' ',' ','#',' ',' ','.',' ','#' },
                            {'#', ' ', '#','.',' ',' ',' ','#',' ','.',' ','#',' ',' ',' ',' ','#' },
                            {'#', ' ', '#',' ',' ',' ',' ','#',' ',' ',' ','#',' ',' ',' ',' ','#' },
                            {'#', ' ', '#',' ',' ',' ',' ','#',' ',' ',' ','#',' ',' ',' ',' ','#' },
                            {'#', ' ', '#',' ',' ',' ',' ','#',' ','#','#','#',' ',' ',' ',' ','#' },
                            {'#', ' ', '#',' ',' ',' ',' ','#',' ',' ',' ',' ',' ',' ',' ',' ','#' },
                            {'#', ' ', '#',' ',' ','.','#','#','#',' ',' ',' ',' ',' ',' ',' ','#' },
                            {'#', ' ', ' ',' ',' ','#','#',' ',' ',' ','.','.','.','.',' ',' ','#' },
                            {'#', ' ', ' ',' ',' ','#',' ',' ',' ',' ',' ',' ',' ',' ',' ',' ','#' },
                            {'#', ' ', ' ',' ',' ','#',' ','#',' ',' ',' ','#','#','#','#',' ','#' },
                            {'#', ' ', '#',' ',' ',' ',' ','#',' ',' ',' ','#','.',' ',' ',' ','#' },
                            {'#', ' ', '#',' ',' ',' ',' ','#',' ',' ',' ','#',' ',' ',' ',' ','#' },
                            {'#', ' ', '#',' ',' ',' ',' ','#',' ',' ',' ','#',' ',' ',' ',' ','#' },
                            {'#', ' ', '#',' ',' ',' ',' ','#',' ',' ',' ',' ',' ',' ',' ','.','#' },
                            {'#', ' ', '#',' ',' ',' ',' ','#',' ',' ',' ','#',' ',' ',' ',' ','#' },
                            {'#', '#', '#','#','#','#','#','#','#','#','#','#','#','#','#','#','#',},
            };

            char playerSymbol = '@';

            int playerScore = 0;
            int playerPositionX = 5;
            int playerPositionY = 3;
            int maxScore;

            Console.CursorVisible = false;

            maxScore = GetMaxCoins(map);

            while (playerScore < maxScore)
            {
                Console.Clear();
                DrawMap(map);

                Console.SetCursorPosition(playerPositionX, playerPositionY);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(playerSymbol);

                Console.SetCursorPosition(17, 0);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"Score: {playerScore}");
                Console.SetCursorPosition(17, 1);
                Console.Write("Передвигайтесь при помощи стрелок и собирайте точки");

                ConsoleKeyInfo pressedKey = Console.ReadKey();

                MovePlayer(pressedKey, ref playerPositionX, ref playerPositionY, map, ref playerScore);
            }

            Console.Clear();
            DrawMap(map);
            Console.SetCursorPosition(17, 0);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"Score: {playerScore}");
            Console.SetCursorPosition(17, 1);
            Console.Write("Вы собрали все точки. Игра окончена, молодец!");
            Console.SetCursorPosition(17, 2);
            Console.Write("Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }

        private static void MovePlayer(ConsoleKeyInfo pressedKey, ref int playerPositionX, ref int playerPositionY, char[,] map, ref int score)
        {
            char emptySymbol = ' ';
            char coinSymbol = '.';
            char wallSymbol = '#';

            int[] direction = GetDirection(pressedKey);

            int nextPlayerPositionX = direction[0] + playerPositionX;
            int nextPlayerPositionY = direction[1] + playerPositionY;

            char nextCell = map[nextPlayerPositionY, nextPlayerPositionX];

            if (nextCell != wallSymbol)
            {
                playerPositionX = nextPlayerPositionX;
                playerPositionY = nextPlayerPositionY;

                if (nextCell == coinSymbol)
                {
                    score++;
                    map[nextPlayerPositionY, nextPlayerPositionX] = emptySymbol;
                }
            }
        }

        private static int GetMaxCoins(char[,] map, char coinSymbol = '.')
        {
            int scoreCount = 0;

            for(int i = 0; i < map.GetLength(0); i++)
                for(int j = 0; j < map.GetLength(1); j++)
                    if (map[i,j] == coinSymbol)
                        scoreCount++;

            return scoreCount;
        }

        private static int[] GetDirection(ConsoleKeyInfo pressedKey)
        {
            int[] direction = { 0, 0 };

            ConsoleKey moveUpCommand = ConsoleKey.UpArrow;
            ConsoleKey moveDownCommand = ConsoleKey.DownArrow;
            ConsoleKey moveRightCommand = ConsoleKey.RightArrow;
            ConsoleKey moveLeftCommand = ConsoleKey.LeftArrow;

            if (pressedKey.Key == moveUpCommand)
                direction[1] = -1;
            else if (pressedKey.Key == moveDownCommand)
                direction[1] = 1;
            else if (pressedKey.Key == moveRightCommand)
                direction[0] = 1;
            else if (pressedKey.Key == moveLeftCommand)
                direction[0] = -1;

            return direction;
        }

        private static void DrawMap(char[,] map)
        {
            ConsoleColor defaultColor = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Blue;

            for (int i = 0; i < map.GetLength(0); i++)
            {
                for (int j = 0; j < map.GetLength(1); j++)
                    Console.Write($"{map[i, j]}");

                Console.WriteLine();
            }
        }
    }
}

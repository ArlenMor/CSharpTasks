using System;
using System.Collections.Generic;
using System.Linq;

namespace IJunior2
{
    internal class Program
    {
        static void Main()
        {
            List<Player> players = new List<Player>
            {
                new Player("Cr1stal", 1, 1),
                new Player("Утка_в_тапках", 5, 50),
                new Player("He}I{g@H4uk", 23, 212),
                new Player("Fluffy", 10, 102),
                new Player("Шаман-наркоман", 33, 400),
                new Player("Злобный_бульбулятор", 2, 15),
                new Player("LIJyXeP", 50, 1000),
                new Player("MaJIeHkuu_Ho_BeJIuKuu", 63, 702),
                new Player("АНТИ ПЕТУХ", 80, 895),
                new Player("K_I_N_G", 73, 800),
            };

            Console.WriteLine("Все игроки сервера: ");

            for(int i = 0; i < players.Count; i++)
            {
                Console.Write((i + 1) + ". ");
                players[i].ShowInfo();
            }

            int numberOfBest = 3;

            List<Player> bestPlayersByLevel = players.OrderByDescending(player => player.Level).Take(numberOfBest).ToList();

            Console.WriteLine("\n\nЛучшие игроки сервера по уровню: ");

            foreach (var player in bestPlayersByLevel)
                player.ShowInfo();

            List<Player> BestPlayersByStrenght = players.OrderByDescending(player => player.Strenght).Take(numberOfBest).ToList();

            Console.WriteLine("\n\nЛучшие игроки сервера по силе: ");

            foreach (var player in BestPlayersByStrenght)
                player.ShowInfo();
        }
    }

    class Player
    {
        public Player(string name, int level, int strenght)
        {
            Name = name;
            Level = level;
            Strenght = strenght;
        }

        public string Name { get; private set; }
        public int Level { get; private set; }
        public int Strenght { get; private set; }

        public void ShowInfo() => Console.WriteLine($"{Name}. Уровень: {Level}. Сила: {Strenght}");
    }
}
using System;

namespace IJunior
{
    internal class Program
    {
        static void Main()
        {
            Player player = new Player(1, "Tom", 100);
            
            player.ShowPlayerInfo();
        }
        
        class Player
        {
            private int _id;

            public Player(int id, string name, float health)
            {
                _id = id;
                Name = name;
                Health = health;
            }

            public string Name { get; private set; }
            public float Health { get; private set; }
            
            public void ShowPlayerInfo()
            {
                Console.WriteLine($"Имя игрока: {Name}\nКоличество здоровья: {Health}");
            }
        }
    }
}
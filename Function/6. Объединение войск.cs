using System;
using System.Collections.Generic;
using System.Linq;

namespace IJunior2
{
    internal class Program
    {
        static void Main()
        {
            List<Soldier> firstGroup = new List<Soldier>
            {
                new Soldier("Иванов"),
                new Soldier("Петров"),
                new Soldier("Бобров"),
                new Soldier("Бедских")
            };

            List<Soldier> secondGroup = new List<Soldier>
            {
                new Soldier("Сидоров"),
                new Soldier("Черных"),
                new Soldier("Григорьев")
            };

            Console.WriteLine("До перевода: \n");

            ShowSoldiers(firstGroup);
            Console.WriteLine();
            ShowSoldiers(secondGroup);

            string filter = "Б";

            List<Soldier> choosenSoldires = firstGroup.Where(soldier => soldier.Surname.ToUpper().StartsWith(filter)).ToList();

            secondGroup = choosenSoldires.Union(secondGroup).ToList();

            foreach(var soldier in secondGroup)
            {
                if (soldier.Surname.ToUpper().StartsWith(filter))
                    firstGroup.Remove(soldier);
            }

            Console.WriteLine("\nПосле перевода: \n");

            ShowSoldiers(firstGroup);
            Console.WriteLine();
            ShowSoldiers(secondGroup);
        }

        private static void ShowSoldiers(List<Soldier> soliders)
        {
            for (int i = 0; i < soliders.Count; i++)
            {
                Console.Write($"{i + 1}. ");
                soliders[i].ShowInfo();
            }
        }
    }

    class Soldier
    {
        public Soldier(string surname)
        {
            Surname = surname;
        }

        public string Surname { get; private set; }

        public void ShowInfo() => Console.WriteLine($"{Surname}.");
    }
}
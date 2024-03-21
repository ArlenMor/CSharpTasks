using System;
using System.Collections.Generic;
using System.Linq;

namespace IJunior2
{
    internal class Program
    {
        static void Main()
        {
            List<Offender> _offenders = new List<Offender>
            {
                new Offender("Иван", "Иванов", 170, 75, "Русский", true),
                new Offender("Василий", "Васильев", 187, 90, "Русский",true),
                new Offender("Петр", "Петров", 193, 100, "Русский",false),
                new Offender("Григорий", "Григорьев", 200, 80, "Русский",false),
                new Offender("Jack", "Aldridge", 177, 90, "Американец",true),
                new Offender("Thomas", "Davies", 180, 100, "Американец",true),
                new Offender("Connor", "Ellington", 193, 90, "Американец",false),
                new Offender("Joseph", "Evans", 200, 80, "Американец",false),
                new Offender("Karl", "Schmidt", 177, 90, "Немец",true),
                new Offender("Richard", "Müller", 183, 80, "Немец",true),
                new Offender("Otto", "Schneider", 197, 75, "Немец",false),
                new Offender("Ludwig", "Hoffmann", 200, 90, "Немец",false),
                new Offender("Gabriel", "Petit", 173, 80, "Француз",true),
                new Offender("Raphael", "Robert", 187, 80, "Француз",true),
                new Offender("Lucas", "Dubois", 193, 80, "Француз",false),
                new Offender("Maël", "Bernard", 107, 75, "Француз",false)
            };

            Console.WriteLine("Список всех известных преступников: ");

            foreach (var offender in _offenders)
                offender.ShowInfo();

            Console.Write("Введите рост: ");
            int growth = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите вес: ");
            int weight = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите национальность: ");
            string nationality = Console.ReadLine();

            var filteredOffenders = _offenders.Where(offender => offender.Growth == growth &&
                                                                    offender.Weight == weight &&
                                                                    offender.Nationality == nationality &&
                                                                    offender.IsPrisoner == false);

            Console.WriteLine("Результат:");

            if (filteredOffenders.Count() == 0)
                Console.WriteLine("Конкретно с такими значениями никого не нашлось...");
            else
                foreach (var offender in filteredOffenders)
                    Console.WriteLine($"{offender.Name} {offender.Surname}");

        }
    }

    class Offender
    {
        public Offender(string name, string surname, int growth, int weight, string nationality, bool isPrisoner)
        {
            Name = name;
            Surname = surname;
            Growth = growth;
            Weight = weight;
            Nationality = nationality;
            IsPrisoner = isPrisoner;
        }

        public string Name { get; private set; }
        public string Surname { get; private set; }
        public int Growth { get; private set; }
        public int Weight { get; private set; }
        public string Nationality { get; private set; }
        public bool IsPrisoner { get; private set; }

        public void ShowInfo()
        {
            Console.Write($"{Name} {Surname}. Рост: {Growth}. Вес: {Weight}. Национальность: {Nationality}. ");

            if (IsPrisoner)
                Console.WriteLine("Заключенный.");
            else
                Console.WriteLine("На свободе.");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace IJunior2
{
    internal class Program
    {
        static void Main()
        {
            string crimeAmnesty = "антиправительственное";

            List<Offender> offenders = new List<Offender>
            {
                new Offender("Иванов", "Иван", "Иванович", "Антиправительственное"),
                new Offender("Петров", "Иван", "Федорович", "Антиправительственное"),
                new Offender("Федоров", "Петр", "Иванович", "Антиправительственное"),
                new Offender("Юсупов", "Эдуард", "Димович", "Кража"),
                new Offender("Крестов", "Альберт", "Анатольевич", "Разбой"),
                new Offender("Кругов", "Федор", "Аркадьевич", "Убийство"),
                new Offender("Штольц", "Вадим", "Анатольевич", "Убийство")
            };

            Console.WriteLine("Добро пожаловать в великое государство Арстоцка!!!");

            Console.WriteLine("Список наших заключенных: \n");

            foreach (Offender offender in offenders)
                offender.ShowInfo();

            Console.WriteLine($"\nВеликое государство Арстоцка объявляет амнистию для всех лиц, которые совершили {crimeAmnesty} преступление!");

            var filteredOffenders = offenders.Where(offender => offender.CrimeTitle.ToLower() != crimeAmnesty.ToLower()).ToList();

            Console.WriteLine("\nСписок наших заключенных после амнистии: \n");

            foreach(Offender offender in filteredOffenders)
            {
                offender.ShowInfo();
            }
        }
    }

    class Offender
    {
        public Offender(string name, string surname, string patronymic, string crimeTitle)
        {
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            CrimeTitle = crimeTitle;
        }

        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Patronymic { get; private set; }
        public string CrimeTitle { get; private set; }

        public void ShowInfo() => Console.WriteLine($"{Surname} {Name} {Patronymic}. Преступление: {CrimeTitle}.");
    }
}
using System;
using System.Collections.Generic;

namespace IJunior2
{
    internal class Program
    {
        static void Main()
        {
            Zoo zoo = new Zoo(new List<Valliere>
            {
                new Valliere("Красный", new List<Animal>
                {
                    new Bear("Медведь", Gender.Male),
                    new Bear("Медведица", Gender.Female)
                }),
                new Valliere("Синий", new List<Animal>
                {
                    new Wolf("Волк", Gender.Male),
                    new Wolf("Волчица", Gender.Female),
                    new Wolf("Волчонок", Gender.Male),
                    new Wolf("Волчонок", Gender.Female)
                }),
                new Valliere("Зеленый", new List<Animal>
                {
                    new Moose("Лось", Gender.Male),
                    new Moose("Лосиха", Gender.Female),
                    new Deer("Олень", Gender.Male),
                    new Deer("Олениха", Gender.Female)
                }),
                new Valliere("Оранжевый", new List<Animal>
                {
                    new Fox("Лиса", Gender.Female),
                    new Rabbit("Заяц", Gender.Male),
                    new Rabbit("Заяц", Gender.Male),
                    new Rabbit("Заяц", Gender.Male),
                    new Rabbit("Заяц", Gender.Male),
                    new Rabbit("Зайчиха", Gender.Female),
                    new Rabbit("Зайчиха", Gender.Female),
                    new Rabbit("Зайчиха", Gender.Female)
                }),
            });

            zoo.Work();
        }
    }

    class Zoo
    {
        List<Valliere> _vallieres;

        public Zoo(List<Valliere> vallieres)
        {
            _vallieres = vallieres;
        }

        public void Work()
        {
            bool isWork = true;

            while(isWork)
            {
                string exitCommand = "exit";

                Console.WriteLine(">>> Зоопарк <<<");

                ShowInfo();

                Console.WriteLine($"Выберите вальер, к которому хотите подойти или введите \"{exitCommand}\" чтобы выйти: ");

                int index;
                string userInput = Console.ReadLine();

                if(userInput == exitCommand)
                {
                    isWork = false;
                }else if(int.TryParse(userInput, out index))
                {
                    if(index >= 1 && index <= _vallieres.Count)
                    {
                        Console.Clear();
                        _vallieres[index - 1].ShowInfo();
                    }
                    else
                    {
                        Console.WriteLine("Вы ввели неверный номер вальера...");
                    }
                }
                else
                {
                    Console.WriteLine("Вы ввели что-то не то.. Попробуйте снова.");
                }

                Console.WriteLine("Нажмите любую клавишу, чтобы продолжить");
                Console.ReadKey();
                Console.Clear();
            }
        }

        public void ShowInfo()
        {
            Console.WriteLine("Вальеры в зоопарке: ");

            for(int i = 0; i < _vallieres.Count; i++)
                Console.WriteLine($"{i + 1}. {_vallieres[i].Name}");
        }
    }

    class Valliere
    {
        List<Animal> _animals;

        public Valliere(string name, List<Animal> aminals)
        {
            Name = name;
            _animals = aminals;
        }

        public string Name { get; private set; }
        public int NumberOfAnimals => _animals.Count;

        public void ShowInfo()
        {
            Console.WriteLine($"Вальер под названием \"{Name}\".");
            Console.WriteLine($"В этом вальере находится {NumberOfAnimals} животных, а именно:");

            for(int i = 0; i < NumberOfAnimals; i++)
            {
                Console.Write($"{i + 1}. {_animals[i].Name} ");

                if (_animals[i].Gender == Gender.Female)
                    Console.Write("женского пола. ");
                else
                    Console.Write("мужского пола. ");

                Console.Write("Слышен ");
                _animals[i].MakeSound();
            }
        }
    }

    enum Gender
    {
        Male,
        Female
    }

    abstract class Animal
    {
        public Animal(string name, Gender gender)
        {
            Name = name;
            Gender = gender;
        }

        public string Name { get; private set; }
        public Gender Gender { get; private set; }

        abstract public void MakeSound();
    }

    class Bear : Animal
    {
        public Bear(string name, Gender gender) : base(name, gender)
        {
        }

        public override void MakeSound()
        {
            Console.WriteLine("рык медведя.");
        }
    }

    class Wolf : Animal
    {
        public Wolf(string name, Gender gender) : base(name, gender)
        {
        }

        public override void MakeSound()
        {
            Console.WriteLine("вой волка.");
        }
    }

    class Fox : Animal
    {
        public Fox(string name, Gender gender) : base(name, gender)
        {
        }

        public override void MakeSound()
        {
            Console.WriteLine("звук лисы.");
        }
    }

    class Deer : Animal
    {
        public Deer(string name, Gender gender) : base(name, gender)
        {
        }

        public override void MakeSound()
        {
            Console.WriteLine("звук оленя.");
        }
    }

    class Moose : Animal
    {
        public Moose(string name, Gender gender) : base(name, gender)
        {
        }

        public override void MakeSound()
        {
            Console.WriteLine("звук лося.");
        }
    }

    class Rabbit : Animal
    {
        public Rabbit(string name, Gender gender) : base(name, gender)
        {
        }

        public override void MakeSound()
        {
            Console.WriteLine("звук зайца.");
        }
    }
}
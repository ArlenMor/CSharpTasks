using System;
using System.Collections.Generic;
using System.Threading;

namespace IJunior2
{
    enum Attributes
    {
        Strength,
        Agility,
        Stamina
    }

    internal class Program
    {
        static void Main()
        {
            Battle battleArena = new Battle();

            battleArena.Start();
        }
    }

    class Battle
    {
        public void Start()
        {
            List<Creature> firstSquad = CreateCreatures();
            List<Creature> secondSquad = CreateCreatures();

            FightBetweenCreatureSquads(firstSquad, secondSquad);

            ShowWinner(firstSquad, secondSquad);
        }

        private void ShowCreatureSquads(List<Creature> firstSquad, List<Creature> secondSquad)
        {
            Console.WriteLine("Первый отряд: ");
            ShowCreatures(firstSquad, 3);

            Console.WriteLine("Второй отряд: ");
            ShowCreatures(secondSquad, 9);
        }

        private void ShowCreatures(List<Creature> creatures, int cursorPositionTop, int tabValue = 22, int tabCounter = 0)
        {
            for (int i = 0; i < creatures.Count; i++)
            {
                creatures[i].ShowInfo(tabCounter, cursorPositionTop);
                tabCounter += tabValue;
                Console.WriteLine();
            }
        }

        private List<Creature> CreateCreatures()
        {
            List<Creature> creatures = new List<Creature>();

            int randomOrc = 1;
            int randomGoblin = 2;
            int randomElf = 3;
            int randomHuman = 4;

            int maxCreatureInSquad = 5;

            for (int i = 0; i < maxCreatureInSquad; i++)
            {
                int health;
                int damage;
                int stamina;

                RandomParamsForCreature(out health, out damage, out stamina);

                int randomCreature = Utilites.RandomizeNumber(randomOrc, randomHuman);

                if (randomCreature == randomOrc)
                {
                    Orc creature = new Orc("Орк " + (i + 1), health, damage, stamina);
                    creatures.Add(creature);
                }
                else if (randomCreature == randomGoblin)
                {
                    Goblin creature = new Goblin("Гоблин " + (i + 1), health, damage, stamina);
                    creatures.Add(creature);
                }
                else if (randomCreature == randomElf)
                {
                    Elf creature = new Elf("Эльф " + (i + 1), health, damage, stamina);
                    creatures.Add(creature);
                }
                else
                {
                    Human creature = new Human("Человек " + (i + 1), health, damage, stamina);
                    creatures.Add(creature);
                }
            }

            return creatures;
        }

        private void RandomParamsForCreature(out int health, out int damage, out int stamina)
        {
            int maxHealth = 200;
            int minHealth = 100;

            int maxDamage = 40;
            int minDamage = 20;

            int maxStamina = 150;
            int minStamina = 100;

            health = Utilites.RandomizeNumber(minHealth, maxHealth);
            damage = Utilites.RandomizeNumber(minDamage, maxDamage);
            stamina = Utilites.RandomizeNumber(minStamina, maxStamina);
        }

        private void FightBetweenCreatureSquads(List<Creature> firstSquad, List<Creature> secondSquad)
        {
            while (firstSquad.Count > 0 && secondSquad.Count > 0)
            {
                Console.WriteLine(">>> Вы находитесь посреди поля боя...  <<<\n");

                ShowCreatureSquads(firstSquad, secondSquad);

                int firstCreatureIndex = Utilites.RandomizeNumber(0, firstSquad.Count - 1);
                int secondCreatureIndex = Utilites.RandomizeNumber(0, secondSquad.Count - 1);

                FightBetweenCreatures(firstSquad[firstCreatureIndex], secondSquad[secondCreatureIndex]);

                if (firstSquad[firstCreatureIndex].Health <= 0)
                    firstSquad.RemoveAt(firstCreatureIndex);

                if (secondSquad[secondCreatureIndex].Health <= 0)
                    secondSquad.RemoveAt(secondCreatureIndex);
            }
        }

        private void FightBetweenCreatures(Creature firstCreature, Creature secondCreature)
        {
            if (PrioritizeFirstTurn(firstCreature, secondCreature))
            {
                Console.Write("Ход первого отряда: ");
                Turn(firstCreature, secondCreature);
                if (secondCreature.Health > 0)
                {
                    Console.Write("Ход второго отряда: ");
                    Turn(secondCreature, firstCreature);
                }    
            }else
            {
                Console.Write("Ход второго отряда: ");
                Turn(secondCreature, firstCreature);

                if (firstCreature.Health > 0)
                {
                    Console.Write("Ход первого отряда: ");
                    Turn(firstCreature, secondCreature);
                }
            }

            Console.WriteLine("Нажмите любую клавишу, чтобы продолжить битву...");
            Console.ReadKey();
            Console.Clear();
        }

        private void ShowWinner(List<Creature> firstSquad, List<Creature> secondSquad)
        {
            if (firstSquad.Count <= 0)
                Console.WriteLine($">>> Победил второй отряд!!! <<<");
            else if (secondSquad.Count <= 0)
                Console.WriteLine($">>> Победил первый отряд!!! <<<");
            else
                Console.WriteLine($">>> Ого!!! Да у нас ничья!!! <<<");
        }

        private bool PrioritizeFirstTurn(Creature firstCreature, Creature secondCreature)
        {
            float summOfCurrentStaminaAbout = firstCreature.Stamina + secondCreature.Stamina;

            return firstCreature.Stamina / summOfCurrentStaminaAbout > secondCreature.Stamina / summOfCurrentStaminaAbout;
        }

        private void Turn(Creature enableCreature, Creature disableCreature)
        {
            Console.WriteLine($"{enableCreature.Name}");

            int minStaminaValueForRest = 25;

            if (enableCreature.Stamina < minStaminaValueForRest)
            {
                enableCreature.Rest();
                return;
            }

            int action = Utilites.RandomizeNumber(0, 2);

            if (action == 0)
            {
                Attack(enableCreature, disableCreature);
            }
            else if (action == 1)
            {
                if (enableCreature.Stamina > 75)
                {
                    Attack(enableCreature, disableCreature);
                }
                else
                {
                    Console.WriteLine($"{enableCreature.Name} отдыхает..");
                    enableCreature.Rest();
                }
            }
            else
            {
                Console.WriteLine($"{enableCreature.Name} использует способность!");
                enableCreature.UseAbilily();
            }
                
        }

        private void Attack(Creature attacker, Creature defender)
        {
            int damage = attacker.GetDamage();
            defender.TakeDamage(damage);
            Console.WriteLine($"{attacker.Name} нанёс {damage} урона");
        }
    }

    abstract class Creature
    {
        protected Creature(string name, int maxHealth, int damage, int stamina)
        {
            Name = name;
            Health = maxHealth;
            MaxHealth = maxHealth;
            Damage = damage;
            Stamina = stamina;
            MaxStamina = Stamina;
        }

        public string Name { get; private set; }
        public int Health { get; protected set; }
        public int MaxHealth { get; private set; }
        public int Damage { get; private set; }
        public int Stamina { get; protected set; }
        public int MaxStamina { get; private set; }

        public abstract int GetDamage();

        public abstract void TakeDamage(int damage);

        public abstract void Rest();

        public abstract void UseAbilily();

        public abstract Creature Clone();

        public void ShowInfo(int cursorPositionLeft = 0, int cursorPositionTop = 0)
        {
            Console.SetCursorPosition(cursorPositionLeft, cursorPositionTop++);

            Console.WriteLine($"Имя: {Name}");
            Console.SetCursorPosition(cursorPositionLeft, cursorPositionTop++);

            Console.WriteLine($"Здоровье: {Health}/{MaxHealth}");
            Console.SetCursorPosition(cursorPositionLeft, cursorPositionTop++);

            Console.WriteLine($"Выносливость: {Stamina}/{MaxStamina}");
            Console.SetCursorPosition(cursorPositionLeft, cursorPositionTop++);

            Console.WriteLine($"Урон: {Damage}");
        }

        protected int GetDamageDefault()
        {
            SpendStamina();

            return Damage;
        }

        protected void TakeDamageDefault(int damage)
        {
            Health -= damage;
        }

        protected void RestDefault()
        {
            int staminaRecovery = 50;

            Stamina += staminaRecovery;

            if (Stamina > MaxStamina)
                Stamina = MaxStamina;
        }

        protected void SpendStamina(int stamina = 25)
        {
            Stamina -= stamina;

            if (Stamina < 0)
                Stamina = 0;
        }
    }

    class Orc : Creature
    {
        bool _isRage;

        public Orc(string name, int maxHealth, int damage, int stamina) : base(name, maxHealth, damage, stamina)
        {
            _isRage = false;
        }

        public override Creature Clone()
        {
            return new Orc(Name, MaxHealth, Damage, Stamina);
        }

        public override int GetDamage()
        {
            if (_isRage)
            {
                _isRage = false;
                return Damage * 2;
            }
            else
            {
                return GetDamageDefault();
            }
        }

        public override void Rest()
        {
            RestDefault();
        }

        public override void TakeDamage(int damage)
        {
            if (_isRage)
                Health -= damage / 2;
            else
                TakeDamageDefault(damage);
        }

        public override void UseAbilily()
        {
            SpendStamina(40);
            _isRage = true;
        }
    }

    class Goblin : Creature
    {
        bool isReadyToEvade = false;
        int chanceToEvade = 40;

        public Goblin(string name, int maxHealth, int damage, int stamina) : base(name, maxHealth, damage, stamina)
        {
        }

        public override Creature Clone()
        {
            return new Goblin(Name, MaxHealth, Damage, Stamina);
        }

        public override int GetDamage()
        {
            return GetDamageDefault();
        }

        public override void Rest()
        {
            RestDefault();
        }

        public override void TakeDamage(int damage)
        {
            if (isReadyToEvade)
            {
                int hungredPercent = 100;

                if (chanceToEvade > Utilites.RandomizeNumber(0, hungredPercent))
                {
                    Console.WriteLine("Гоблин увернулся от атаки!!! Следующий урон не пройдёт...");
                    return;
                }
                else
                    TakeDamageDefault(damage);
            }    

            TakeDamageDefault(damage);
        }

        public override void UseAbilily()
        {
            SpendStamina(35);
            isReadyToEvade = true;
        }
    }

    class Elf : Creature
    {
        public Elf(string name, int maxHealth, int damage, int stamina) : base(name, maxHealth, damage, stamina)
        {
        }

        public override Creature Clone()
        {
            return new Elf(Name, MaxHealth, Damage, Stamina);
        }

        public override int GetDamage()
        {
            return GetDamageDefault();
        }

        public override void Rest()
        {
            RestDefault();
        }

        public override void TakeDamage(int damage)
        {
            TakeDamageDefault(damage);
        }

        public override void UseAbilily()
        {
            Health += 25;

            if (Health > MaxHealth)
                Health = MaxHealth;

            SpendStamina(50);
        }
    }

    class Human : Creature
    {
        public Human(string name, int maxHealth, int damage, int stamina) : base(name, maxHealth, damage, stamina)
        {
        }

        public override Creature Clone()
        {
            return new Human(Name, MaxHealth, Damage, Stamina);
        }

        public override int GetDamage()
        {
            SpendStamina(15);

            return Damage;
        }

        public override void Rest()
        {
            RestDefault();
        }

        public override void TakeDamage(int damage)
        {
            TakeDamageDefault(damage);
        }

        public override void UseAbilily()
        {
            Stamina = MaxStamina;
        }
    }

    class Utilites
    {
        private static Random s_random = new Random();

        public static int RandomizeNumber(int minNumber, int maxMunber)
        {
            return s_random.Next(minNumber, maxMunber + 1);
        }
    }
}
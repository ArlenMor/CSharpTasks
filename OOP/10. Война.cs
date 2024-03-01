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
            BattleArena battleArena = new BattleArena();

            battleArena.Work();
        }
    }

    class BattleArena
    {
        public void Work()
        {
            Console.WriteLine(">>> Добро пожаловать на боевую арену <<<\n");

            Console.WriteLine("Выберите двух бойцов, которых вы хотите свести в поединке (укажите норер): ");

            Console.Write("Подождите, идёт подготовка к бою");

            int numberOfDots = 3;

            for (int i = 0; i < numberOfDots; i++)
            {
                Console.Write('.');
                Thread.Sleep(1000);
            }

            Console.Clear();
        }

        private Soldier GetSoldier(List<Soldier> soldiers)
        {
            int index;

            Soldier soldier = null;

            do
            {
                if(int.TryParse(Console.ReadLine(), out index) == false)
                    Console.Write("Вы ввели что-то не то. Попробуйте ещё раз:");
                else if (index < 1 || index > soldiers.Count)
                    Console.Write("Вы ввели неверный индекс. Попроуйбет ещё раз:");
                else
                    soldier = soldiers[index - 1].Clone();
            } while (soldier == null);

            return soldier;
        }

        private void Fight(Soldier firstSoldier, Soldier secondSoldier)
        {
            while (firstSoldier.CurrentHealth > 0 && secondSoldier.CurrentHealth > 0)
            {
                firstSoldier.ShowInfo(50, 0);
                secondSoldier.ShowInfo(85, 0);

                Console.SetCursorPosition(0, 0);

                if (PrioritizeFirstTurn(firstSoldier, secondSoldier))
                {
                    Turn(firstSoldier, secondSoldier);
                    if (secondSoldier.CurrentHealth > 0)
                        Turn(secondSoldier, firstSoldier);
                }
                else
                {
                    Turn(secondSoldier, firstSoldier);
                    if (firstSoldier.CurrentHealth > 0)
                        Turn(firstSoldier, secondSoldier);
                }

                Console.SetCursorPosition(0, 5);

                Console.WriteLine("Нажмите любую клавишу, чтобы продолжить битву...");
                Console.ReadKey();
                Console.Clear();
            }

            Console.Clear();

            ShowWinner(firstSoldier, secondSoldier);

            Console.WriteLine("Нажмите любую клавишу, чтобы выйти...");
            Console.ReadKey();
        }

        private void ShowWinner(Soldier firstSoldier, Soldier secondSoldier)
        {
            if (firstSoldier.CurrentHealth < 0)
                Console.WriteLine($">>> Победил {secondSoldier.Name}!!! <<<");
            else if (secondSoldier.CurrentHealth < 0)
                Console.WriteLine($">>> Победил {firstSoldier.Name}!!! <<<");
            else
                Console.WriteLine($">>> Ого!!! Да у нас ничья!!! <<<");
        }

        private bool PrioritizeFirstTurn(Soldier firstSoldier, Soldier secondSoldier)
        {
            float summOfCurrentStaminaAbout = firstSoldier.CurrentStaminaAmount + secondSoldier.CurrentStaminaAmount;

            return firstSoldier.CurrentStaminaAmount / summOfCurrentStaminaAbout > secondSoldier.CurrentStaminaAmount / summOfCurrentStaminaAbout;
        }

        private void Turn(Soldier enableSoldier, Soldier disableSoldier)
        {
            Console.WriteLine($"Ход {enableSoldier.Name}");

            int minStaminaValueForRest = 15;

            if (enableSoldier.CurrentStaminaAmountInPercent < minStaminaValueForRest)
            {
                enableSoldier.Rest();
                return;
            }

            int action = Utilites.RandomNumber(0, 2);

            if (action == 0)
                Attack(enableSoldier, disableSoldier);
            else if (action == 1)
                if (enableSoldier.CurrentStaminaAmountInPercent > 90)
                    Attack(enableSoldier, disableSoldier);
                else
                    enableSoldier.Rest();
            else
                enableSoldier.UseAbilily();
        }

        private void Attack(Soldier attacker, Soldier defender)
        {
            float damage = attacker.GiveDamage();
            defender.TakeDamage(damage);
            Console.WriteLine($"{attacker.Name} нанёс {damage} урона");
        }
    }

    abstract class Soldier
    {
        protected Attribute _mainAtribute;

        public Soldier(string name, Attributes mainAtribute, int strength, int agility, int stamina)
        {
            Name = name;

            float minHealth = 120;
            float healthPerStrenght = 22;
            float minStaminaAmount = 75;
            float staminaAmountPerStamina = 12;

            Strength = new Attribute(strength);
            Agility = new Attribute(agility);
            Stamina = new Attribute(stamina);

            if (mainAtribute == Attributes.Strength)
                _mainAtribute = Strength;
            else if (mainAtribute == Attributes.Agility)
                _mainAtribute = Agility;
            else
                _mainAtribute = Stamina;

            MaxHealth = minHealth + (Strength.Value * healthPerStrenght);
            CurrentHealth = MaxHealth;

            Damage = _mainAtribute.Value;

            MaxStaminaAmount = minStaminaAmount + (Stamina.Value * staminaAmountPerStamina);
            CurrentStaminaAmount = MaxStaminaAmount;
        }

        public string Name { get; private set; }
        public Attribute Strength { get; private set; }
        public Attribute Agility { get; private set; }
        public Attribute Stamina { get; private set; }
        public float MaxHealth { get; private set; }
        public float CurrentHealth { get; protected set; }
        public float Damage { get; protected set; }
        public float MaxStaminaAmount { get; private set; }
        public float CurrentStaminaAmount { get; protected set; }
        public float CurrentStaminaAmountInPercent => MaxStaminaAmount / 100f * CurrentStaminaAmount;

        public abstract float GiveDamage();

        public abstract void TakeDamage(float damage);

        public abstract void Rest();

        public abstract void UseAbilily();

        public abstract Soldier Clone();

        public void ShowInfo(int cursorPositionLeft = 0, int cursorPositionTop = 0)
        {
            Console.SetCursorPosition(cursorPositionLeft, cursorPositionTop++);

            Console.WriteLine($"Имя: {Name}");
            Console.SetCursorPosition(cursorPositionLeft, cursorPositionTop++);

            Console.WriteLine($"Здоровье: {CurrentHealth}/{MaxHealth}");
            Console.SetCursorPosition(cursorPositionLeft, cursorPositionTop++);

            Console.WriteLine($"Выносливость: {CurrentStaminaAmount}/{MaxStaminaAmount}");
            Console.SetCursorPosition(cursorPositionLeft, cursorPositionTop++);

            Console.WriteLine($"Урон: {Damage}");
        }

        protected float GiveDamageDefault()
        {
            SpendStaminaOnHit();

            return Damage;
        }

        protected void TakeDamageDefault(float damage)
        {
            CurrentHealth -= damage;
        }

        protected void RestDefault()
        {
            float recoveryPercentage = 65f;
            float maxPercentage = 100f;

            float staminaRecovery = (MaxStaminaAmount - CurrentStaminaAmount) / maxPercentage * recoveryPercentage;

            Console.WriteLine($"{Name} восстановил {staminaRecovery} выносливости.");

            CurrentStaminaAmount += staminaRecovery;
        }

        protected void SpendStaminaOnHit(int hitCost = 25)
        {
            CurrentStaminaAmount -= hitCost;

            if (CurrentStaminaAmount < 0)
                CurrentStaminaAmount = 0;
        }
    }

    class Attribute
    {
        public Attribute(int value)
        {
            Value = value;
        }

        public int Value { get; private set; }
    }

    class Utilites
    {
        private static Random s_random = new Random();

        public static int RandomNumber(int minNumber, int maxMunber)
        {
            return s_random.Next(minNumber, maxMunber + 1);
        }
    }
}
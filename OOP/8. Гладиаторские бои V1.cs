using System;
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
        private const char ChooseFirstHeroCommand = '1';
        private const char ChooseSecondHeroCommand = '2';
        private const char ChooseThirdHeroCommand = '3';
        private const char ChooseFourthHeroCommand = '4';
        private const char ChooseFifthHeroCommand = '5';

        public void Work()
        {
            Hero firstHero;
            Hero secondHero;

            Console.WriteLine(">>> Добро пожаловать на боевую арену <<<\n");

            Console.WriteLine("Выберите двух бойцов, которых вы хотите свести в поединке (укажите норер): ");

            Console.WriteLine($"{ChooseFirstHeroCommand} - Ассасин");
            Console.WriteLine($"{ChooseSecondHeroCommand} - Паладин");
            Console.WriteLine($"{ChooseThirdHeroCommand} - Воин");
            Console.WriteLine($"{ChooseFourthHeroCommand} - Танцор");
            Console.WriteLine($"{ChooseFifthHeroCommand} - Троль");

            Console.Write("Введите номер первого бойца: ");
            firstHero = GetHero();

            Console.Write("Введите номер второго бойца: ");
            secondHero = GetHero();

            Console.WriteLine($"Первый герой: {firstHero.Name}");
            Console.WriteLine($"второй герой: {secondHero.Name}");

            Console.Write("Подождите, идёт подготовка к бою");

            for(int i = 0; i < 3; i++)
            {
                Console.Write('.'); 
                Thread.Sleep(1000);
            }

            Console.Clear();

            Fight(firstHero, secondHero);
        }

        private HeroFactory GetFactory(char heroType)
        {
            switch (heroType)
            {
                case ChooseFirstHeroCommand:
                    return new AssassinFactory("Ассасин", Attributes.Agility, 5, 20, 6, 2);

                case ChooseSecondHeroCommand:
                    return new PaladinFactory("Паладин", Attributes.Stamina, 10, 5, 15, 3);

                case ChooseThirdHeroCommand:
                    return new WarriorFactory("Воин", Attributes.Strength, 15, 5, 10, 2);

                case ChooseFourthHeroCommand:
                    return new DancerFactory("Танцор", Attributes.Agility, 5, 13, 20, 2);

                case ChooseFifthHeroCommand:
                    return new TrollFactory("Троль", Attributes.Strength, 20, 2, 20, 2);

                default:
                    return null;
            }
        }
            
        private Hero GetHero()
        {
            HeroFactory heroFactory;

            do
            {
                char heroType = Console.ReadKey().KeyChar;
                Console.WriteLine();
                heroFactory = GetFactory(heroType);

                if (heroFactory == null)
                    Console.Write("Вы ввели что-то не то, попробуйте ещё раз: ");
            } while (heroFactory == null);

            return heroFactory.GetHero();
        }

        private void Fight(Hero firstHero, Hero secondHero)
        {
            while (firstHero.CurrentHealth > 0 && secondHero.CurrentHealth> 0)
            { 
                firstHero.ShowInfo(50, 0);
                secondHero.ShowInfo(85, 0);

                Console.SetCursorPosition(0, 0);

                if (ItTurnFirstHero(firstHero, secondHero))
                {
                    Turn(firstHero, secondHero);
                    if(secondHero.CurrentHealth > 0)
                        Turn(secondHero, firstHero);
                }
                else
                {
                    Turn(secondHero, firstHero);
                    if (firstHero.CurrentHealth > 0)
                        Turn(firstHero, secondHero);
                }

                Console.SetCursorPosition(0, 5);

                Console.WriteLine("Нажмите любую клавишу, чтобы продолжить битву...");
                Console.ReadKey();
                Console.Clear();
            }

            Console.Clear();

            if (firstHero.CurrentHealth < 0)
                Console.WriteLine($">>> Победил {secondHero.Name}!!! <<<");
            else if (secondHero.CurrentHealth < 0)
                Console.WriteLine($">>> Победил {firstHero.Name}!!! <<<");
            else
                Console.WriteLine($">>> Ого!!! Да у нас ничья!!! <<<");

            Console.WriteLine("Нажмите любую клавишу, чтобы выйти...");
            Console.ReadKey();
        }

        private bool ItTurnFirstHero(Hero firstHero, Hero secondHero)
        {
            float summOfCurrentStaminaAbout = firstHero.CurrentStaminaAmount + secondHero.CurrentStaminaAmount;

            return firstHero.CurrentStaminaAmount / summOfCurrentStaminaAbout > secondHero.CurrentStaminaAmount / summOfCurrentStaminaAbout;
        }

        private void Turn(Hero enableHero, Hero disableHero)
        {
            Console.WriteLine($"Ход {enableHero.Name}");

            int minStaminaValueForRest = 15;

            if(enableHero.CurrentStaminaAmountInPercent < minStaminaValueForRest)
            {
                enableHero.Rest();
                return;
            }

            int action = Utilites.RandomNumber(0, 2);

            if(action == 0)
                Attack(enableHero, disableHero);
            else if (action == 1)
                if(enableHero.CurrentStaminaAmountInPercent > 90)
                    Attack(enableHero, disableHero);
                else
                    enableHero.Rest();
            else
                enableHero.UseAbilily();
        }

        private void Attack(Hero attacker, Hero defender)
        {
            float damage = attacker.GiveDamage();
            defender.TakeDamage(damage);
            Console.WriteLine($"{attacker.Name} нанёс {damage} урона");
        }
    }

    abstract class Hero
    { 
        private Attribute _mainAtribute;

        public Hero(string name, Attributes mainAtribute, int strength, int agility, int stamina, float baseArmor)
        {
            Name = name;

            float minHealth = 120;
            float healthPerStrenght = 22;
            float healthRegenerationPerStrenght = 0.1f;
            float armorPerAgility = 0.167f;
            float atackSpeedPerAgility = 0.1f;
            float minAttackSpeed = 1f;
            float minStaminaAmount = 75;
            float staminaAmountPerStamina = 12;
            float staminaRegenerationPerStamina = 0.2f;

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
            HealthRegeneration = Strength.Value * healthRegenerationPerStrenght;

            Armor = baseArmor + (Agility.Value * armorPerAgility);

            Damage = _mainAtribute.Value;
            AttackSpeed = minAttackSpeed + Agility.Value * atackSpeedPerAgility;

            MaxStamina = minStaminaAmount + (Stamina.Value * staminaAmountPerStamina);
            CurrentStaminaAmount = MaxStamina;
            StaminaRegeneration = Stamina.Value * staminaRegenerationPerStamina;
        }

        public string Name { get; private set; }
        public Attribute Strength { get; private set; }
        public Attribute Agility { get; private set; }
        public Attribute Stamina { get; private set; }
        public float MaxHealth { get; private set; }
        public float CurrentHealth { get; protected set; }
        public float HealthRegeneration { get; protected set; }
        public float Armor { get; protected set; }
        public float Damage { get; protected set; }
        public float AttackSpeed { get; protected set; }
        public float MaxStamina { get; private set; }
        public float CurrentStaminaAmount { get; protected set; }
        public float CurrentStaminaAmountInPercent => MaxStamina / 100f * CurrentStaminaAmount;
        public float StaminaRegeneration { get; protected set; }

        public abstract float GiveDamage();
       
        public abstract void TakeDamage(float damage);

        public abstract void Rest();

        public abstract void UseAbilily();

        public void ShowInfo(int cursorPositionLeft = 0, int cursorPositionTop = 0)
        {
            Console.SetCursorPosition(cursorPositionLeft, cursorPositionTop++);

            Console.WriteLine($"Имя: {Name}");
            Console.SetCursorPosition(cursorPositionLeft, cursorPositionTop++);

            Console.WriteLine($"Броня: {Armor}");
            Console.SetCursorPosition(cursorPositionLeft, cursorPositionTop++);

            Console.WriteLine($"Здоровье: {CurrentHealth}/{MaxHealth}");
            Console.SetCursorPosition(cursorPositionLeft, cursorPositionTop++);

            Console.WriteLine($"Регенерация здоровья: {HealthRegeneration}");
            Console.SetCursorPosition(cursorPositionLeft, cursorPositionTop++);

            Console.WriteLine($"Выносливость: {CurrentStaminaAmount}/{MaxStamina}");
            Console.SetCursorPosition(cursorPositionLeft, cursorPositionTop++);

            Console.WriteLine($"Восстановление выносливости: {StaminaRegeneration}");
            Console.SetCursorPosition(cursorPositionLeft, cursorPositionTop++);
        }

        public void RegenerateHealth()
        {
            CurrentHealth += HealthRegeneration;

            if (CurrentHealth > MaxHealth)
                CurrentHealth = MaxHealth;
        }

        public void RegenerateStamina()
        {
            CurrentStaminaAmount += StaminaRegeneration;

            if (CurrentStaminaAmount > MaxStamina)
                CurrentStaminaAmount = MaxStamina;
        }

        protected float GiveDamageDefault()
        {
            float totalDamage = Damage * Convert.ToSingle(Math.Floor(AttackSpeed));

            SpendStaminaOnHit();

            return totalDamage;
        }

        protected void TakeDamageDefault(float damage)
        {
            float damageMultiplier;

            float damageMultiplierCoef = 0.06f;

            if (Armor != 0)
                damageMultiplier = 1 - (damageMultiplierCoef * Armor / (1 + (damageMultiplierCoef * Armor)));
            else
                damageMultiplier = 1;

            float totalDamage = damage * damageMultiplier;

            CurrentHealth -= totalDamage;
        }

        protected void RestDefault()
        {
            float recoveryPercentage = 65f;

            float staminaRecovery = (MaxStamina - CurrentStaminaAmount) / 100f * recoveryPercentage;

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

    class Assassin : Hero
    {
        private bool _isCriticalDamage;
        private int _criticalDamagePercent = 300;

        public Assassin(string name, Attributes mainAtribute, int strength, int agility, int stamina, float baseArmor) : base(name, mainAtribute, strength, agility, stamina, baseArmor)
        {
            _isCriticalDamage = false;
        }

        public override float GiveDamage()
        {
            if (_isCriticalDamage)
            {
                _isCriticalDamage = false;
                SpendStaminaOnHit();

                return (Damage * (_criticalDamagePercent / 100f)) + (Damage * Convert.ToSingle(Math.Floor(AttackSpeed - 1)));
            }
            else
            {
                return GiveDamageDefault();
            }
        }

        public override void TakeDamage(float damage)
        {
            TakeDamageDefault(damage);
        }

        public override void Rest()
        {
            RestDefault();
        }

        public override void UseAbilily()
        {
            Console.WriteLine("Ассасин готовится совершить смертельную атаку!!!");
            _isCriticalDamage = true;
        }
    }

    class Paladin : Hero
    {
        private int _damageBlockCount;
        private int _maxDamageBlockCount;

        public Paladin(string name, Attributes mainAtribute, int strength, int agility, int stamina, float baseArmor) : base(name, mainAtribute, strength, agility, stamina, baseArmor)
        {
            _damageBlockCount = 0;
            _maxDamageBlockCount = 2;
        }

        public override float GiveDamage()
        {
            return GiveDamageDefault();
        }


        public override void TakeDamage(float damage)
        {
            if (_damageBlockCount > 0)
            {
                _damageBlockCount--;
                return;
            }
            else
            {
                TakeDamageDefault(damage);
            }
        }

        public override void Rest()
        {
            RestDefault();
        }

        public override void UseAbilily()
        {
            Console.WriteLine($"Паладин поднимает свой щит и блокирует {_maxDamageBlockCount} удара!");
            _damageBlockCount = _maxDamageBlockCount;
        }
    }

    class Warrior : Hero
    {
        private bool _isBattleCry;
        private float _battleCryDamageBoostPerProcent = 200f;

        public Warrior(string name, Attributes mainAtribute, int strength, int agility, int stamina, float baseArmor) : base(name, mainAtribute, strength, agility, stamina, baseArmor)
        {
            _isBattleCry = false;
        }

        public override float GiveDamage()
        {
            if(_isBattleCry)
            {
                _isBattleCry = false;
                return GiveDamageDefault() * (_battleCryDamageBoostPerProcent / 100f);
            }
            else
            {
                return GiveDamageDefault();
            }
        }

        public override void TakeDamage(float damage)
        {
            TakeDamageDefault(damage);
        }

        public override void Rest()
        {
            RestDefault();
        }

        public override void UseAbilily()
        {
            Console.WriteLine("Воин издаёт устрашающий боевой клич!!!");
            _isBattleCry = true;
        }
    }

    class Dancer : Hero
    {
        private bool _isBattleDance;

        public Dancer(string name, Attributes mainAtribute, int strength, int agility, int stamina, float baseArmor) : base(name, mainAtribute, strength, agility, stamina, baseArmor)
        {
            _isBattleDance = false;
        }

        public override float GiveDamage()
        {
            _isBattleDance = false;
            return GiveDamageDefault();
        }

        public override void TakeDamage(float damage)
        {
            if (_isBattleDance)
                return;
            else
                TakeDamageDefault(damage);
        }

        public override void Rest()
        {
            RestDefault();
        }

        public override void UseAbilily()
        {
            Console.WriteLine("Что происходит? Он что, танцует?");
            _isBattleDance = true;
        }
    }

    class Troll : Hero
    {
        private bool _isStoneThrown;
        private int _chanceToHittingPerPercent;
        private int _criticalDamagePercent = 600;

        public Troll(string name, Attributes mainAtribute, int strength, int agility, int stamina, float baseArmor) : base(name, mainAtribute, strength, agility, stamina, baseArmor)
        {
            _isStoneThrown = false;
            _chanceToHittingPerPercent = 30;
        }

        public override float GiveDamage()
        {
            if(_isStoneThrown)
            {
                _isStoneThrown=false;

                SpendStaminaOnHit(50);

                int percent = Utilites.RandomNumber(0, 100);

                if (percent < _chanceToHittingPerPercent)
                    return Damage * (_criticalDamagePercent / 100f);
                else
                    return 0;
            }
            else
            {
                return GiveDamageDefault();
            }
        }

        public override void TakeDamage(float damage)
        {
            TakeDamageDefault(damage);
        }

        public override void Rest()
        {
            RestDefault();
        }

        public override void UseAbilily()
        {
            Console.WriteLine("Оооо нет... Троль взял в руки огромный валун!!!");
            _isStoneThrown = true;
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

    abstract class HeroFactory
    {
        public abstract Hero GetHero();
    }

    class AssassinFactory : HeroFactory
    {
        private string _name;
        private Attributes _mainAttribute;
        private int _strenght;
        private int _agility;
        private int _stamina;
        private int _baseArmor;

        public AssassinFactory(string name, Attributes attributes, int strenght, int agility, int stamina, int baseArmor)
        {
            _name = name;
            _mainAttribute = attributes;
            _strenght = strenght;
            _agility = agility;
            _stamina = stamina;
            _baseArmor = baseArmor;
        }

        public override Hero GetHero()
        {
            return new Assassin(_name, _mainAttribute, _strenght, _agility, _stamina, _baseArmor);
        }
    }

    class PaladinFactory : HeroFactory
    {
        private string _name;
        private Attributes _mainAttribute;
        private int _strenght;
        private int _agility;
        private int _stamina;
        private int _baseArmor;

        public PaladinFactory(string name, Attributes attributes, int strenght, int agility, int stamina, int baseArmor)
        {
            _name = name;
            _mainAttribute = attributes;
            _strenght = strenght;
            _agility = agility;
            _stamina = stamina;
            _baseArmor = baseArmor;
        }

        public override Hero GetHero()
        {
            return new Paladin(_name, _mainAttribute, _strenght, _agility, _stamina, _baseArmor);
        }
    }

    class WarriorFactory : HeroFactory
    {
        private string _name;
        private Attributes _mainAttribute;
        private int _strenght;
        private int _agility;
        private int _stamina;
        private int _baseArmor;

        public WarriorFactory(string name, Attributes attributes, int strenght, int agility, int stamina, int baseArmor)
        {
            _name = name;
            _mainAttribute = attributes;
            _strenght = strenght;
            _agility = agility;
            _stamina = stamina;
            _baseArmor = baseArmor;
        }

        public override Hero GetHero()
        {
            return new Warrior(_name, _mainAttribute, _strenght, _agility, _stamina, _baseArmor);
        }
    }

    class DancerFactory : HeroFactory
    {
        private string _name;
        private Attributes _mainAttribute;
        private int _strenght;
        private int _agility;
        private int _stamina;
        private int _baseArmor;

        public DancerFactory(string name, Attributes attributes, int strenght, int agility, int stamina, int baseArmor)
        {
            _name = name;
            _mainAttribute = attributes;
            _strenght = strenght;
            _agility = agility;
            _stamina = stamina;
            _baseArmor = baseArmor;
        }

        public override Hero GetHero()
        {
            return new Dancer(_name, _mainAttribute, _strenght, _agility, _stamina, _baseArmor);
        }
    }

    class TrollFactory : HeroFactory
    {
        private string _name;
        private Attributes _mainAttribute;
        private int _strenght;
        private int _agility;
        private int _stamina;
        private int _baseArmor;

        public TrollFactory(string name, Attributes attributes, int strenght, int agility, int stamina, int baseArmor)
        {
            _name = name;
            _mainAttribute = attributes;
            _strenght = strenght;
            _agility = agility;
            _stamina = stamina;
            _baseArmor = baseArmor;
        }

        public override Hero GetHero()
        {
            return new Troll(_name, _mainAttribute, _strenght, _agility, _stamina, _baseArmor);
        }
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
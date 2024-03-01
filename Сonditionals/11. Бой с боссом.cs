using System;

namespace IJunior
{
    class Program
    {
        static void Main()
        {
            const string UserFireBallCommand = "1";
            const string UserIceRayCommand = "2";
            const string UserSpellBuffCommand = "3";
            const string UserHealthPotionCommand = "4";

            const int WeekArmorRand = 0;
            const int MediumArmorRand = 1;
            const int StrongArmorRand = 2;
            const int MaxArmorRand = 3;

            const int EnemyPhantomArrowCommand = 0;
            const int EnemyPhantomWolfBiteCommand = 1;
            const int EnemySpellBuffCommand = 2;
            const int EnemyHealthPotionCommand = 3;

            int numberOfArmorOptions = 4;
            int enemyAbilityRange = 4;
            int enemyTurn;
            int numberOfUserHealthPotion = 2;
            int healthUserRecovery = 50;
            int maxUserHealth = 100;
            int numberOfEnemyHealthPotion = 1;
            int healthEnemyRecovery = 65;
            int maxEnemyHealth = 150;
            int roundNumber = 1;

            float userHp = 100;
            float userArmor;
            float fireBallDamage = 75;
            float iceRayDamage = 60;
            float userSpellBuffCoef = 1.25f;
            float enemyHp = 150;
            float enemyArmor;
            float phantomArrowDamage = 55;
            float phantomWolfBiteDamage = 80;
            float enemySpellBuffCoef = 1.1f;

            string userCommand;

            bool isUserSpellBuffed = false;
            bool isEnemySpellBuffed = false;

            Random random = new Random();
            int armorRandom = random.Next(0, numberOfArmorOptions);

            Console.WriteLine("*Добро пожаловать на магическую битву!!! Сегодня у нас поединок между начинающим магом Арантума, представляющий честь своего клана Ташимоби, который будет сражаться против бывалого орка-мага, известного своей сильной связью с духами. Пусть победит сильнейший! Да начнётся битва!*");

            Console.Write("Ты видишь перед собой рунный камень защины. Как только ты поднимаешь его, ты ");

            switch (armorRandom)
            {
                case WeekArmorRand:
                    userArmor = 25;
                    Console.WriteLine($"чувствуешь слабую защиту (Твоя броня: {userArmor})");
                    break;

                case MediumArmorRand:
                    userArmor = 50;
                    Console.WriteLine($"чувствуешь среднюю защиту (Твоя броня: {userArmor})");
                    break;

                case StrongArmorRand:
                    userArmor = 75;
                    Console.WriteLine($"чувствуешь сильную защиту (Твоя броня: {userArmor})");
                    break;

                case MaxArmorRand:
                    userArmor = 100;
                    Console.WriteLine($"чувствуешь максимальную защиту (Твоя броня: {userArmor})");
                    break;

                default:
                    userArmor = 0;
                    Console.WriteLine("обращаешь свой взор на зрителей, в поисках поддержки.");
                    break;
            }

            armorRandom = random.Next(0, numberOfArmorOptions);

            switch (armorRandom)
            {
                case WeekArmorRand:
                    enemyArmor = 25;
                    break;

                case MediumArmorRand:
                    enemyArmor = 50;
                    break;

                case StrongArmorRand:
                    enemyArmor = 75;
                    break;

                case MaxArmorRand:
                    enemyArmor = 100;
                    break;

                default:
                    enemyArmor = 0;
                    break;
            }

            while (userHp > 0 && enemyHp > 0)
            {
                Console.WriteLine($"Раунд номер {roundNumber}. У врага {enemyHp} здоровья и {enemyArmor} брони, а у тебя {userHp} зодровья и {userArmor} брони.");
                Console.WriteLine("Твой ход:");
                Console.WriteLine($"{UserFireBallCommand}. Метнуть огненный шар (Урона без усиления - {fireBallDamage}).");
                Console.WriteLine($"{UserIceRayCommand}. Выпустить ледяной луч (Урона без усиления - {iceRayDamage}).");
                Console.WriteLine($"{UserSpellBuffCommand}. Усилить следующее заклинание (Увеличивает урон от следующего заклинания на {userSpellBuffCoef}).");
                Console.WriteLine($"{UserHealthPotionCommand}. Выпить зелье здоровья. Осталось {numberOfUserHealthPotion} шт.");

                Console.Write("--> ");
                userCommand = Console.ReadLine();

                switch (userCommand)
                {
                    case UserFireBallCommand:
                        if (isUserSpellBuffed)
                        {
                            float buffedDamage = fireBallDamage * userSpellBuffCoef;

                            if (enemyArmor - buffedDamage > 0)
                            {
                                enemyArmor -= buffedDamage;
                                Console.WriteLine($"Твои руки разгораются сильнее обычного, но этого не хватает, чтобы пробить броню! (Ты нанёс {buffedDamage} урона по броне)");
                            }
                            else
                            {
                                Console.WriteLine($"Твои руки разгораются сильнее обычного и ты бросаешь огненный шар прямо в лицо противника! (Ты нанёс {buffedDamage} урона, {enemyArmor} из которых заблокировала броня.)"); 
                                enemyHp -= buffedDamage - enemyArmor;
                                enemyArmor = 0;
                            }

                            isUserSpellBuffed = false;
                        }
                        else
                        {
                            if (enemyArmor - fireBallDamage > 0)
                            {
                                enemyArmor -= fireBallDamage;
                                Console.WriteLine($"Твои руки загораются и выпускают несколько огненных снарядов, которые разбиваются о броню противника! (Ты нанёс {fireBallDamage} урона по броне)");
                            }
                            else
                            {
                                Console.WriteLine($"Твои руки загораются и выпускают несколько огненных снарядов, прожигающие кожу противника! (Ты нанёс {fireBallDamage} урона, {enemyArmor} из которых заблокировала броня.)");
                                enemyHp -= fireBallDamage - enemyArmor;
                                enemyArmor = 0;
                            }
                        }
                        break;

                    case UserIceRayCommand:
                        if (isUserSpellBuffed)
                        {
                            float buffedDamage = iceRayDamage * userSpellBuffCoef;

                            if (enemyArmor - buffedDamage > 0)
                            {
                                enemyArmor -= buffedDamage;
                                Console.WriteLine($"Из твоей груди вырывается сильнейший ледяной луч, но его останавливает вражеская броня! (Ты нанёс {buffedDamage} урона по броне)");
                            }
                            else
                            {
                                Console.WriteLine($"Из твоей груди вырывается сильнейший ледяной луч, который впивается в тело противника! (Ты нанёс {buffedDamage} урона, {enemyArmor} из которых заблокировала броня.)");
                                enemyHp -= buffedDamage - enemyArmor;
                                enemyArmor = 0;
                            }

                            isUserSpellBuffed = false;
                        }
                        else
                        {
                            if (enemyArmor - iceRayDamage > 0)
                            {
                                enemyArmor -= iceRayDamage;
                                Console.WriteLine($"Твои руки леденеют, выпуская несколько ледяных лучей, которые обмораживают броню противника! (Ты нанёс {iceRayDamage} урона по броне)");
                            }
                            else
                            {
                                Console.WriteLine($"Твои руки леденеют, выпуская несколько ледяных лучей, которые впиваются в тело противника! (Ты нанёс {iceRayDamage} урона, {enemyArmor} из которых заблокировала броня.)");
                                enemyHp -= iceRayDamage - enemyArmor;
                                enemyArmor = 0;
                            }
                        }
                        break;

                    case UserSpellBuffCommand:
                        Console.WriteLine("Вы концентрируете энергию на своём медальоне, усиливая своё следующее заклинание.");
                        isUserSpellBuffed = true;
                        break;

                    case UserHealthPotionCommand:
                        if (numberOfUserHealthPotion > 0)
                        {
                            if (userHp + healthUserRecovery > maxUserHealth)
                                userHp = maxUserHealth;
                            else
                                userHp += healthUserRecovery;

                            Console.WriteLine($"Ты выпиваешь зелье здоровья и разбиваешь флакон о землю, сейчас тебе не до него. Теперь у тебя {userHp} здоровья");
                            numberOfUserHealthPotion--;
                        }
                        else
                            Console.WriteLine("Ты тянешься за фляжкой с зельем, но обнаруживаешь, что оно пустое. Драгоценное время потеряно! Ты пропускаешь ход.");
                        break;

                    default:
                        Console.WriteLine("Ты переволновался и запутался в своих заклинаниях!!! У тебя ничего не получилось, ты пропускаешь ход. Будь внимательней!");
                        break;
                }

                Console.WriteLine("Похоже, ты сделал что мог, сейчас очередь противника.");

                enemyTurn = random.Next(0, enemyAbilityRange);

                switch (enemyTurn)
                {
                    case EnemyPhantomArrowCommand:
                        if (isEnemySpellBuffed)
                        {
                            float buffedDamage = phantomArrowDamage * enemySpellBuffCoef;

                            if (userArmor - buffedDamage > 0)
                            {
                                userArmor -= buffedDamage;
                                Console.WriteLine($"Противник запускает в тебя огненную призрачную стрелу, которая отскакивает от твоей брони! (Ты потерял {buffedDamage} брони).");
                            }
                            else
                            {
                                Console.WriteLine($"Противник запускает в тебя огненную призрачную стрелу, которая впивается тебе в плечо! (Ты получил {buffedDamage} урона, {userArmor} из которых заблокировала броня.)");
                                userHp -= buffedDamage - userArmor;
                                userArmor = 0;
                            }

                            isEnemySpellBuffed = false;
                        }
                        else
                        {
                            if (userArmor - phantomArrowDamage > 0)
                            {
                                userArmor -= phantomArrowDamage;
                                Console.WriteLine($"Противник запускает в тебя  призрачную стрелу, которая разбивается о броню! (Ты потерял {phantomArrowDamage} брони).");
                            }
                            else
                            {
                                Console.WriteLine($"Противник запускает в тебя  призрачную стрелу, которая задевает твой торс по касательной! (Ты получил {phantomArrowDamage} урона, {userArmor} из которых заблокировала броня.)");
                                userHp -= phantomArrowDamage - userHp;
                                userArmor = 0;
                            }
                        }
                        break;

                    case EnemyPhantomWolfBiteCommand:
                        if (isEnemySpellBuffed)
                        {
                            float buffedDamage = phantomWolfBiteDamage * enemySpellBuffCoef;

                            if (userArmor - buffedDamage > 0)
                            {
                                userArmor -= phantomWolfBiteDamage * enemySpellBuffCoef;
                                Console.WriteLine($"Противник спускает с цепи призрачного цербера, который разрывает твою броню! (Ты потерял {buffedDamage} брони).");
                            }
                            else
                            {
                                Console.WriteLine($"Противник спускает с цепи призрачного цербера, который впивается в твою шею! (Ты получил {buffedDamage} урона, {userArmor} из которых заблокировала броня.)");
                                userHp -= buffedDamage - userArmor;
                                userArmor = 0;
                            }

                            isEnemySpellBuffed = false;
                        }
                        else
                        {
                            if (userArmor - phantomWolfBiteDamage > 0)
                            {
                                userArmor -= phantomWolfBiteDamage;
                                Console.WriteLine($"Противник спускает с цепи призрачного цербера, зубы которого останавливает твоя броня! (Ты потерял {phantomWolfBiteDamage} брони).");
                            }
                            else
                            {
                                Console.WriteLine($"Противник спускает с цепи призрачного цербера, который прокусывает твою ногу! (Ты получил {phantomWolfBiteDamage} урона, {userArmor} из которых заблокировала броня.)");
                                userHp -= phantomWolfBiteDamage - userArmor;
                                userArmor = 0;
                            }
                        }
                        break;

                    case EnemySpellBuffCommand:
                        Console.WriteLine("Ты видишь, как рядом с врагом начинают кружится духи, а он замер в молитве. Это отличный момент для удара! Но что он задумал..");
                        isEnemySpellBuffed = true;
                        break;

                    case EnemyHealthPotionCommand:
                        if (numberOfEnemyHealthPotion > 0)
                        {
                            if (enemyHp + healthEnemyRecovery > maxEnemyHealth)
                                enemyHp = maxEnemyHealth;
                            else
                                enemyHp += healthEnemyRecovery;

                            Console.WriteLine($"Враг достаёт из подсумка какую-то зеленую жижу и выпивает её. Он восстановился, и теперь у него {enemyHp} здоровья");
                            numberOfEnemyHealthPotion--;
                        }
                        else
                            Console.WriteLine("Враг тянется в подсумок, но его рука останавливается там в замешательстве. Это твой шанс! Атакуй!");
                        break;

                    default:
                        Console.WriteLine("Ты видишь, как противник готовится совершить заклинание, как вдруг блик солнце падаем ему ровно глаз! Он замешкался. Удача на твоей стороне, атакуй!");
                        break;
                }

                roundNumber++;
            }

            if (userHp <= 0 && enemyHp <= 0)
                Console.WriteLine("В этой ожесточенной битве вы умудрились убить друг друга. Похоже, никто из вас не смог одержать верх над другим. Ничья...");
            else if (userHp <= 0)
                Console.WriteLine("О нет, духи настигли тебя. Ты покидаешь этот мир с одной лишь мыслью в голове: \"Я проиграл\"");
            else
                Console.WriteLine("Ты выпускаешь своё последнее заклинание и видишь, как тело противника перестаёт двигаться. Больше никаких битв. Ты победил. LEVELUP!!!");
        }
    }
}
using System;

namespace IJunior
{
    class Program
    {
        static void Main()
        {
            const string ShowNoteCommand = "1";
            const string ShowTimeCommand = "2";
            const string RecreateNoteCommand = "3";
            const string RecreateTimeCommand = "4";
            const string DeleteNoteCommand = "5";
            const string ExitCommand = "6";
            const string EmptyNote = "Пусто";
            const string Agreement = "y";
            const string Disagreement = "n";

            bool isProgrammRunning = true;
            bool isIncorrectInput;
            bool isContinueUntilDecision;

            string userCommand;
            string note = EmptyNote;
            string timeDecision;

            int timeInNoteHour = 0, timeInNoteMinutes = 0;
            int minMinutesInHour = 0, maxMinutesInHour = 59;
            int minHoursInDay = 0, maxHoursInDay = 23;

            Console.WriteLine("->Добро пожаловать в приложение \"заметОЧКА\"! Это бета тест, поэтому пока что Вы можете сохранить только одну заметку.");
            Console.WriteLine("->Что вы хотите сделать? (Напишите номер строки)");

            Console.WriteLine($"{ShowNoteCommand}. Посмотреть заметку.");
            Console.WriteLine($"{ShowTimeCommand}. Узнать время, указанное в заметке.");
            Console.WriteLine($"{RecreateNoteCommand}. Перезаписать заметку на новую.");
            Console.WriteLine($"{RecreateTimeCommand}. Перезаписать время в заметке.");
            Console.WriteLine($"{DeleteNoteCommand}. Удалить заметку.");
            Console.WriteLine($"{ExitCommand}. Выйти из программы \"заметОЧКА\".");

            while (isProgrammRunning)
            {
                Console.Write("-->");
                userCommand = Console.ReadLine();

                switch (userCommand)
                {
                    case ShowNoteCommand:
                        Console.WriteLine("->Строка ниже - Ваша заметка:");
                        Console.WriteLine($"->{note}");
                        Console.Write($"->Хотите узнать время, указанное в заметке? {Agreement}/{Disagreement}: ");

                        isContinueUntilDecision = true;

                        while (isContinueUntilDecision)
                        {
                            timeDecision = Console.ReadLine();

                            if (timeDecision == Agreement)
                            {
                                Console.WriteLine($"->Время - {timeInNoteHour}:{timeInNoteMinutes}");
                                isContinueUntilDecision = false;
                            }
                            else if (timeDecision == Disagreement)
                            {
                                Console.WriteLine("->На нет и суда нет.");
                                isContinueUntilDecision = false;
                            }
                            else
                            {
                                Console.Write("->Не понял Вас. Введите y или n:");
                            }
                        }
                        break;

                    case ShowTimeCommand:
                        Console.WriteLine($"->Время - {timeInNoteHour}:{timeInNoteMinutes}");
                        break;

                    case RecreateNoteCommand:
                        Console.WriteLine("->Введите новую заметку:");
                        Console.Write("-->");
                        note = Console.ReadLine();
                        Console.WriteLine("->Сохранил.");
                        Console.Write($"->Хотите устоновить время для заметки? {Agreement}/{Disagreement}: ");

                        isContinueUntilDecision = true;

                        while (isContinueUntilDecision)
                        {
                            timeDecision = Console.ReadLine();

                            if (timeDecision == Agreement)
                            {
                                isIncorrectInput = true;

                                while (isIncorrectInput)
                                {
                                    Console.Write($"->Введите час: ");
                                    timeInNoteHour = Convert.ToInt32(Console.ReadLine());

                                    if (timeInNoteHour >= minHoursInDay && timeInNoteHour <= maxHoursInDay)
                                    {
                                        isIncorrectInput = false;
                                        continue;
                                    }

                                    Console.WriteLine("->Вы, видимо, ошиблись, ведь такого часа нет. Попробуйте ещё раз.");
                                }

                                isIncorrectInput = true;

                                while (isIncorrectInput)
                                {
                                    Console.Write($"->Введите минуты: ");
                                    timeInNoteMinutes = Convert.ToInt32(Console.ReadLine());

                                    if (timeInNoteMinutes >= minMinutesInHour && timeInNoteMinutes <= maxMinutesInHour)
                                    {
                                        isIncorrectInput = false;
                                        continue;
                                    }

                                    Console.WriteLine("->Неверное количество минут. Попробуйте ещё раз.");
                                }

                                isContinueUntilDecision = false;
                            }
                            else if (timeDecision == Disagreement)
                            {
                                Console.WriteLine("->На нет и суда нет.");
                                isContinueUntilDecision = false;
                            }
                            else
                            {
                                Console.Write("->Не понял Вас. Введите y или n:");
                            }
                        }
                        break;

                    case RecreateTimeCommand:
                        Console.WriteLine("->Хорошо, тогда приступим:");

                        isIncorrectInput = true;

                        while (isIncorrectInput)
                        {
                            Console.Write($"->Введите час: ");
                            timeInNoteHour = Convert.ToInt32(Console.ReadLine());

                            if (timeInNoteHour >= minHoursInDay && timeInNoteHour <= maxHoursInDay)
                            {
                                isIncorrectInput = false;
                                continue;
                            }

                            Console.WriteLine("->Вы, видимо, ошиблись, ведь такого часа нет. Попробуйте ещё раз.");
                        }

                        isIncorrectInput = true;

                        while (isIncorrectInput)
                        {
                            Console.Write($"->Введите минуты: ");
                            timeInNoteMinutes = Convert.ToInt32(Console.ReadLine());

                            if (timeInNoteMinutes >= minMinutesInHour && timeInNoteMinutes <= maxMinutesInHour)
                            {
                                isIncorrectInput = false;
                                continue;
                            }
                                
                            Console.WriteLine("->Неверное количество минут. Попробуйте ещё раз.");
                        }
                        break;

                    case DeleteNoteCommand:
                        Console.WriteLine("->Хорошо. У Вас теперь пустая заметка");
                        note = EmptyNote;
                        timeInNoteHour = 0;
                        timeInNoteMinutes = 0;
                        break;

                    case ExitCommand:
                        Console.WriteLine("->Нажмите ESC");

                        if (Console.ReadKey(true).Key == ConsoleKey.Escape)
                        {
                            Console.WriteLine("->Приложение заметОЧКА закрывается. До встречи!");
                            isProgrammRunning = false;
                        }
                        break;

                    default:
                        Console.WriteLine("Мне жаль, но такого варианта нет... Попробуйте выбрать что-нибудь из меню:");
                        break;
                }
            }
        }
    }
}
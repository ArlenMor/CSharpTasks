using System;
using System.Collections.Generic;

namespace IJunior
{
    internal class Program
    {
        static void Main()
        {
            const string AddDossierCommand = "1";
            const string ShowAllDossiersCommand = "2";
            const string RemoveDossierCommand = "3";
            const string ExitCommand = "4";

            Dictionary<string, string> dossiers = new Dictionary<string, string>();

            bool isOpen = true;

            while (isOpen)
            {
                Console.WriteLine("----------Архив досье----------\n");
                Console.WriteLine("----------Возможности----------");
                Console.WriteLine($"{AddDossierCommand}. Добавить досье.");
                Console.WriteLine($"{ShowAllDossiersCommand}. Показать все досье.");
                Console.WriteLine($"{RemoveDossierCommand}. Удалить досье.");
                Console.WriteLine($"{ExitCommand}. Закрыть программу.");

                switch (Console.ReadLine())
                {
                    case AddDossierCommand:
                        AddDossier(dossiers);
                        break;

                    case ShowAllDossiersCommand:
                        ShowAllDossiers(dossiers);
                        break;

                    case RemoveDossierCommand:
                        RemoveDossier(dossiers);
                        break;

                    case ExitCommand:
                        isOpen = false;
                        break;

                    default:
                        Console.Clear();
                        break;
                }

                Console.SetCursorPosition(64, 1);
                Console.WriteLine("Для продолжения нажмите любую клавишу...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        private static void AddDossier(Dictionary<string, string> dossiers)
        {
            Console.Write("Введите ФИО: ");
            string newName = Console.ReadLine();

            if (dossiers.ContainsKey(newName) == false)
            {
                Console.Write("Введите название должности: ");
                string newJobTitle = Console.ReadLine();

                dossiers.Add(newName, newJobTitle);
            }
            else
            {
                Console.WriteLine("Такое имя уже существует.");
            }
        }

        private static void ShowAllDossiers(Dictionary<string, string> dossiers)
        {
            Console.CursorVisible = false;

            Console.SetCursorPosition(32, 0);
            Console.WriteLine("-----------Все досье-----------");
            Console.SetCursorPosition(64, 0);
            Console.WriteLine("Нажмите клавишу, чтобы продолжить...");

            int numberOfDossier = 1;
            int[] cursorPosition = { 32, 2 };

            foreach (var dossier in dossiers)
            {
                Console.SetCursorPosition(cursorPosition[0], cursorPosition[1]);
                Console.WriteLine($"{numberOfDossier}. {dossier.Key} - {dossier.Value}");
                cursorPosition[1]++;
                numberOfDossier++;
            }

            Console.CursorVisible = true;
        }

        private static void RemoveDossier(Dictionary<string, string> dossiers)
        {
            if (dossiers.Count == 0)
            {
                Console.WriteLine("Список пуст. Нечего удалять.");
                return;
            }

            Console.WriteLine("Введите ФИО досье, которое хотите удалить: ");
            string nameToDelete = Console.ReadLine();

            if (dossiers.ContainsKey(nameToDelete))
            {
                dossiers.Remove(nameToDelete);
                Console.WriteLine("Успешно.");
            }
            else
            {
                Console.WriteLine("ФИО не найдена. Проверьте корректность написания.");
            }
        }
    }
}
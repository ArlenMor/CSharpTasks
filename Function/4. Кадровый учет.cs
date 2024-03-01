using System;

namespace IJunior
{
    internal class Program
    {
        static void Main()
        {
            const string AddDossierCommand = "1";
            const string ShowAllDossiersCommand = "2";
            const string SearchDossierBySecondNameCommand = "3";
            const string RemoveDossierCommand = "4";
            const string ExitCommand = "5";

            string[] dossierFullNames = new string[0];
            string[] dossierJobTitles = new string[0];

            bool isOpen = true;

            while (isOpen)
            {
                Console.WriteLine("----------Архив досье----------\n");
                Console.WriteLine("----------Возможности----------");
                Console.WriteLine($"{AddDossierCommand}. Добавить досье.");
                Console.WriteLine($"{ShowAllDossiersCommand}. Вывести все досье.");
                Console.WriteLine($"{SearchDossierBySecondNameCommand}. Поиск досье по фамилии.");
                Console.WriteLine($"{RemoveDossierCommand}. Удалить досье.");
                Console.WriteLine($"{ExitCommand}. Закрыть программу.");

                switch (Console.ReadLine())
                {
                    case AddDossierCommand:
                        AddDossier(ref dossierFullNames, ref dossierJobTitles);
                        break;

                    case ShowAllDossiersCommand:
                        ShowAllDossiers(dossierFullNames, dossierJobTitles);
                        break;

                    case SearchDossierBySecondNameCommand:
                        SearchDossierBySecondName(dossierFullNames, dossierJobTitles);
                        break;

                    case RemoveDossierCommand:
                        RemoveDossier(ref dossierFullNames, ref dossierJobTitles);
                        break;

                    case ExitCommand:
                        isOpen = false;
                        break;

                    default:
                        Console.Clear();
                        break;
                }
            }
        }

        private static void AddDossier(ref string[] names, ref string[] jobTitles)
        {
            Console.Write("Введите ФИО: ");
            string newName = Console.ReadLine();

            Console.Write("Введите название должности: ");
            string newJobTitle = Console.ReadLine();

            names = AddElementInArray(names, newName);
            jobTitles = AddElementInArray(jobTitles, newJobTitle);

            Console.WriteLine("Для продолжения нажмите любую клавишу...");
            Console.ReadKey();
            Console.Clear();
        }

        private static void ShowAllDossiers(string[] names, string[] jobTitles)
        {
            Console.CursorVisible = false;

            Console.SetCursorPosition(32, 0);
            Console.WriteLine("-----------Все досье-----------");
            Console.SetCursorPosition(64, 0);
            Console.WriteLine("Нажмите клавишу, чтобы продолжить...");

            for (int i = 0; i < names.Length && i < jobTitles.Length; i++)
            {
                Console.SetCursorPosition(32, i + 2);
                Console.WriteLine($"{i + 1}. {names[i]} - {jobTitles[i]}");
            }

            Console.CursorVisible = true;
            Console.ReadKey();
            Console.Clear();
        }

        private static void SearchDossierBySecondName(string[] names, string[] jobTitles)
        {
            bool isFound = false;

            Console.Write("Введите фамилию: ");
            string inputName = Console.ReadLine();

            for (int i = 0; i < names.Length; i++)
            {
                string[] tempArray = names[i].Split();

                if (tempArray[0].ToLower() == inputName.ToLower())
                {
                    Console.WriteLine($"Досье найдено под номером: {i + 1}.");
                    Console.WriteLine($"Досье пользователя: {names[i]} - {jobTitles[i]}.");

                    isFound = true;
                }
            }

            if (!isFound)
                Console.WriteLine("Досье с такой фамилией не найдено.");

            Console.WriteLine("Для продолжения нажмите любую клавишу...");
            Console.ReadKey();
            Console.Clear();
        }

        private static void RemoveDossier(ref string[] names, ref string[] jobTitles)
        {
            Console.WriteLine("Введите номер досье, которые хотите удалить: ");

            if (int.TryParse(Console.ReadLine(), out int remoteIndex))
            {
                if (remoteIndex <= names.Length && remoteIndex >= 1)
                {
                    names = RemoveIndexFromArray(names, remoteIndex - 1);
                    jobTitles = RemoveIndexFromArray(jobTitles, remoteIndex - 1);

                    Console.WriteLine("Успешно.");
                }
                else
                {
                    Console.WriteLine("Введён неверный номер досье.");
                }
            }
            else
            {
                Console.WriteLine("Введён неверный номер досье.");
            }

            Console.WriteLine("Для продолжения нажмите любую клавишу...");
            Console.ReadKey();
            Console.Clear();
        }

        private static string[] RemoveIndexFromArray(string[] array, int remoteIndex)
        {
            string[] tempArray = new string[array.Length - 1];

            for (int i = 0; i < remoteIndex; i++)
                tempArray[i] = array[i];

            for (int i = remoteIndex + 1; i < array.Length; i++)
                tempArray[i - 1] = array[i];

            return tempArray;
        }
        private static string[] AddElementInArray(string[] array, string value)
        {
            string[] tmpArray = new string[array.Length + 1];

            for (int i = 0; i < array.Length; i++)
                tmpArray[i] = array[i];

            tmpArray[tmpArray.Length - 1] = value;

            return tmpArray;
        }
    }
}
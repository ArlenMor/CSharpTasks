using System;
using System.Collections.Generic;
using System.Linq;

namespace IJunior2
{
    internal class Program
    {
        static void Main()
        {
            Hospital hospital = new Hospital();

            bool isWork = true;

            while(isWork)
            {
                const string SortByFullNameCommand = "1";
                const string SortByAgeCommand = "2";
                const string ShowPatientsWithDiagnosisCommand = "3";
                const string ExitCommand = "4";

                Console.WriteLine("Добро пожаловать в больницу.\n");

                Console.WriteLine("Список наших больных: \n");
                hospital.ShowInfo();

                Console.WriteLine("\nВыберите действие: ");
                Console.WriteLine($"{SortByFullNameCommand}. Отсортировать больных по имени.");
                Console.WriteLine($"{SortByAgeCommand}. Отсортировать больных по возрасту.");
                Console.WriteLine($"{ShowPatientsWithDiagnosisCommand}. Показать больных с определенным диагнозом.");
                Console.WriteLine($"{ExitCommand}. Закрыть программу.");

                string userInput = Console.ReadLine();

                switch (userInput)
                {
                    case SortByFullNameCommand:
                        hospital.SortPatientsByFullName();
                        break;
                    case SortByAgeCommand:
                        hospital.SortPatientsByAge();
                        break;
                    case ShowPatientsWithDiagnosisCommand:
                        hospital.ShowPatientWithDiagnosis();
                        break;
                    case ExitCommand:
                        isWork = false;
                        break;
                    default:
                        Console.WriteLine("Вы ввелич то-то не то. Попробуйте ещё раз...");
                        break;
                }

                Console.WriteLine("Нажмите любую клавишу, чтобы продолжить...");
                Console.ReadKey();
                Console.Clear();
            }
        }
    }

    class Hospital
    {
        List<Patient> _patients;

        public Hospital()
        {
            _patients = new List<Patient>
            {
                new Patient("Иванов","Иван","Иванович","Грипп",55),
                new Patient("Иванов","Егор","Аркадьевич","Простуда",16),
                new Patient("Иванов","Вадим","Павлович","Грыжа",40),
                new Patient("Иванов","Владимир","Аркадьевич","Рак",29),
                new Patient("Крестов","Петр","Анатольевич","Рак",94),
                new Patient("Кругов","Эдуард","Аркадьевич","Ожоги",32),
                new Patient("Штольц","Вадим","Анатольевич","Простуда",20),
                new Patient("Паровозов","Аркадий","Владимирович","Ожоги",31),
                new Patient("Каменькин","Михаил","Федорович","Инсульт",85),
                new Patient("Маменькин","Анатолий","Анатольевич","Простуда",16),
            };
        }

        public void ShowInfo()
        {
            foreach (Patient patient in _patients)
                patient.ShowInfo();
        }

        public void SortPatientsByFullName()
        {
            _patients = _patients.OrderBy(patient => patient.Patronymic).
                                    OrderBy(patient => patient.Name).
                                    OrderBy(patient => patient.Surname).ToList();
        }

        public void SortPatientsByAge()
        {
            _patients = _patients.OrderBy(patient => patient.Patronymic).
                                    OrderBy(patient => patient.Name).
                                    OrderBy(patient => patient.Surname).
                                    OrderBy(patient => patient.Age).ToList();
        }

        public void ShowPatientWithDiagnosis()
        {
            Console.WriteLine("Введите название диагноза: ");

            string diagnosis = Console.ReadLine();

            List<Patient> selectedPatients = _patients.Where(patient => patient.Diagnosis.ToLower() == diagnosis.ToLower()).ToList();

            if(selectedPatients.Count != 0)
            {
                Console.WriteLine($"Список поциентов с диагнозом: {diagnosis.ToLower()}");

                foreach (Patient patient in selectedPatients)
                    patient.ShowInfo();
            }
            else
            {
                Console.WriteLine("Пациентов с таким заболеванием не обнаружино...");
            }
        }
    }

    class Patient
    {
        public Patient(string surname, string name, string patronymic, string diagnosis, int age)
        {
            Name = name;
            Surname = surname;
            Patronymic = patronymic;
            Diagnosis = diagnosis;
            Age = age;
        }

        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Patronymic { get; private set; }
        public string Diagnosis { get; private set; }
        public int Age { get; private set; }

        public void ShowInfo() => Console.WriteLine($"{Surname} {Name} {Patronymic}. Возраст: {Age}. Диагноз: {Diagnosis}");
    }
}
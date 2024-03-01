using System;
using System.Collections.Generic;

namespace IJunior
{
    internal class Program
    {
        static void Main()
        {
            Menu menu = new Menu();

            menu.Work();
        }
    }

    class Menu
        {
            private const string AddPlayerCommand = "1";
            private const string BanPlayerByIdCommand = "2";
            private const string UnbanPlayerByIdCommand = "3";
            private const string DeletePlayerCommand = "4";
            private const string ShowPlayersCommand = "5";
            private const string ExitCommand = "6";

            private Database _database = new Database();

            public void Work()
            {
                bool isWorking = true;

                while (isWorking)
                {
                    Console.WriteLine($"Добавить игрока - {AddPlayerCommand}");
                    Console.WriteLine($"Забанить игрока по ID - {BanPlayerByIdCommand}");
                    Console.WriteLine($"Разбанить игрока по ID - {UnbanPlayerByIdCommand}");
                    Console.WriteLine($"Удалить игрока - {DeletePlayerCommand}");
                    Console.WriteLine($"Показать всю информацию о игроках - {ShowPlayersCommand}");
                    Console.WriteLine($"Закрыть программу - {ExitCommand}");

                    string userInput = Console.ReadLine();

                    switch (userInput)
                    {
                        case AddPlayerCommand:
                            AddPlayer();
                            break;

                        case BanPlayerByIdCommand:
                            BanUserById();
                            break;

                        case UnbanPlayerByIdCommand:
                            UnbanPlayerById();
                            break;

                        case DeletePlayerCommand:
                            DeletePlayer();
                            break;

                        case ShowPlayersCommand:
                            ShowPlayers();
                            break;

                        case ExitCommand:
                            isWorking = false;
                            break;

                        default:
                            ShowErrorMessage();
                            break;
                    }

                    Console.WriteLine("Нажмите любую клавишу для продолжения...");
                    Console.ReadKey();
                    Console.Clear();
                }
            }

            private void ShowPlayers()
            {
                Console.SetCursorPosition(0, 15);
                _database.ShowAllPlayersInfo();
            }

            private void AddPlayer()
            {
                string playerName;

                int newId;
                int playerLevel = 1;

                bool isCorrectLevel = false;

                Console.Write("Введите имя игрока: ");
                playerName = Console.ReadLine();

                while (isCorrectLevel == false)
                {
                    Console.Write("Введите уровень игрока: ");
                    playerLevel = ReadNonnegativInt(Console.ReadLine());

                    if (playerLevel != 0)
                        isCorrectLevel = true;
                }

                newId = _database.IdCount;

                Player newPlayer = new Player(newId, false, playerName, playerLevel);

                _database.AddPlayer(newPlayer);
            }

            private void BanUserById()
            {
                if (_database.HasPlayers() == false)
                    return;

                int id = ReadId();

                if (_database.TryFindPlayerById(id, out Player player))
                    player.Ban();
                else
                    Console.WriteLine($"Игрок с id = {id} не найден в базе данных.");
            }

            private void UnbanPlayerById()
            {
                if (_database.HasPlayers() == false)
                    return;

                int id = ReadId();

                if (_database.TryFindPlayerById(id, out Player player))
                    player.Unban();
                else
                    Console.WriteLine($"Игрок с id = {id} не найден в базе данных.");
            }

            private int ReadNonnegativInt(string inputString)
            {
                int number;

                if (int.TryParse(inputString, out number) == false)
                {
                    Console.WriteLine("Нужно ввести целое неотрицательное число, а не строку.");
                    return 0;
                }

                if (number <= 0)
                {
                    Console.WriteLine("Нужно ввести целое неотрицательное число.");
                    return 0;
                }

                return number;
            }

            private void DeletePlayer()
            {
                if (_database.HasPlayers() == false)
                    return;

                int id = ReadId();

                if (_database.TryFindPlayerById(id, out Player remotePlayer) == true)
                    _database.RemovePlayer(remotePlayer);
                else
                    Console.WriteLine($"Игрко с id = {id} не найден в базе данных.");
            }

            private void ShowErrorMessage()
            {
                Console.WriteLine("Нет такой команды.");
            }

            private int ReadId()
            {
                bool isCorrectId = false;
                int id = 0;

                while (isCorrectId == false)
                {
                    Console.Write("Введите id игрока, чтобы разбанить его: ");
                    id = ReadNonnegativInt(Console.ReadLine());

                    if (id != 0)
                        isCorrectId = true;
                }

                return id;
            }
        }

        class Player
        {
            public Player(int id, bool isBanned, string name, int level)
            {
                Id = id;
                IsBanned = isBanned;
                Name = name;
                Level = level;
            }

            public bool IsBanned { get; private set; }
            public int Id { get; }
            public string Name { get; }
            public int Level { get; }

            public void Ban()
            {
                IsBanned = true;
            }

            public void Unban()
            {
                IsBanned = false;
            }
        }

        class Database
        {
            private List<Player> _players = new List<Player>();

            public Database()
            {
                IdCount = 1;
            }

            public int IdCount { get; private set; }

            public void AddPlayer(Player newPlayer)
            {
                _players.Add(newPlayer);
                IdCount++;
            }

            public void RemovePlayer(Player deletedPlayer)
            {
                _players.Remove(deletedPlayer);
            }

            public void ShowAllPlayersInfo()
            {
                foreach (Player player in _players)
                    Console.WriteLine($"Id = {player.Id}. Name = {player.Name}. Level = {player.Level}. Ban = {player.IsBanned}");
            }

            public bool TryFindPlayerById(int id, out Player findedPlayer)
            {
                foreach (Player player in _players)
                    if (player.Id == id)
                    {
                        findedPlayer = player;
                        return true;
                    }

                findedPlayer = null;
                return false;
            }

            public bool HasPlayers()
            {
                return _players.Count > 0;
            }
        }
}
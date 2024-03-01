using System;
using System.Collections.Generic;

namespace IJunior
{
    internal class Program
    {
        static void Main()
        {
            Casino casino = new Casino();

            casino.Work();
        }
    }

    public class Casino
    {
        private Deck _deck = new Deck();
        private Player _player = new Player();

        public void Work()
        {
            bool isOpen = true;

            const ConsoleKey TakeNextCardCommand = ConsoleKey.Enter;
            const ConsoleKey StopTakeCardCommand = ConsoleKey.Escape;

            while (isOpen)
            {
                if (_deck.GetCurrentCardsCount <= 0)
                {
                    ShowPlayerCards();
                    break;
                }

                _deck.PrintCards();
                _player.PrintCards();

                Console.SetCursorPosition(0, 6);
                Console.WriteLine($"Чтобы взять карту, нажмите {TakeNextCardCommand}.");
                Console.WriteLine($"Чтобы прекратить, нажмите {StopTakeCardCommand}.");

                ConsoleKeyInfo userDecision = Console.ReadKey();

                switch (userDecision.Key)
                {
                    case TakeNextCardCommand:
                        GiveCardToPlayer();
                        break;

                    case StopTakeCardCommand:
                        ShowPlayerCards();
                        isOpen = false;
                        break;

                    default:
                        Console.WriteLine("Вы ввели что-то не то.");
                        break;
                }

                Console.Clear();
            }
        }

        private void GiveCardToPlayer()
        {
            _player.TakeCard(_deck.GiveCard());
        }

        private void ShowPlayerCards()
        {
            Console.Clear();
            Console.WriteLine("Ваша итоговая рука: ");
            _player.PrintCards(0, 1);

            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу чтобы выйти...");
            Console.ReadKey();
        }
    }

    public class Player
    {
        private List<Card> _cards = new List<Card>();

        public void TakeCard(Card card)
        {
            if (card != null)
                _cards.Add(card);
        }

        public void PrintCards(int cursorPositionLeft = 40, int cursorPositionTop = 0, int cardsInRow = 13)
        {
            Console.SetCursorPosition(cursorPositionLeft, cursorPositionTop);
            Console.WriteLine("Карты в руке: ");
            Console.SetCursorPosition(cursorPositionLeft, ++cursorPositionTop);

            for (int i = 1; i <= _cards.Count; i++)
            {
                _cards[i - 1].ShowInfo();

                if (i % cardsInRow == 0)
                    Console.SetCursorPosition(cursorPositionLeft, ++cursorPositionTop);
            }
        }
    }

    public class Card
    {
        public Card(char key, char suit)
        {
            Key = key;
            Suit = suit;
        }

        public char Suit { get; }
        public char Key { get; }

        public void ShowInfo()
        {
            Console.Write($"{Key}{Suit} ");
        }
    }

    public class Deck
    {
        private List<Card> _cards;

        public Deck()
        {
            CreateCards();
            Shaffle();
        }

        public int GetCurrentCardsCount 
        {
            get
            {
                return _cards.Count;
            }
        }

        public void PrintCards(int cursorPositionLeft = 0, int cursorPositionTop = 0, int cardsInRow = 13)
        {
            Console.SetCursorPosition(cursorPositionLeft, cursorPositionTop);
            Console.WriteLine("Карты в колоде: ");
            Console.SetCursorPosition(cursorPositionLeft, ++cursorPositionTop);

            for (int i = 1; i <= _cards.Count; i++)
            {
                _cards[i - 1].ShowInfo();

                if (i % cardsInRow == 0)
                    Console.SetCursorPosition(cursorPositionLeft, ++cursorPositionTop);
            }
        }

        public Card GiveCard()
        {
            Card popCard = _cards[_cards.Count - 1];
            _cards.Remove(popCard);
            return popCard;
        }

        private void CreateCards()
        {
            char[] suits = { '♠', '♥', '♦', '♣' };
            char[] keys = { '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A' };

            int numberOfSuit = suits.Length;
            int numberOfCardOfSameSuit = keys.Length;

            _cards = new List<Card>();

            for (int i = 0; i < numberOfSuit; i++)
                for (int j = 0; j < numberOfCardOfSameSuit; j++)
                    _cards.Add(new Card(keys[j], suits[i]));
        }

        private void Shaffle()
        {
            Random random = new Random();

            for (int i = 0; i < _cards.Count; i++)
            {
                int randomIndex = random.Next(0, _cards.Count);

                Card card = _cards[i];
                _cards[i] = _cards[randomIndex];
                _cards[randomIndex] = card;
            }
        }
    }
}
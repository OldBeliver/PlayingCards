using System;
using System.Collections.Generic;

namespace PlayingCards
{
    class Program
    {
        static void Main(string[] args)
        {
            Croupier croupier = new Croupier();
            croupier.Work();
        }
    }

    class Card
    {
        private string _rank;
        private string _suit;

        public Card(string rank, string suit)
        {
            _rank = rank;
            _suit = suit;            
        }

        public void ShowInfo()
        {
            Console.Write($"{_rank}{_suit}");
        }
    }

    class Deck
    {
        private static Random _random;

        private List<Card> _cards;

        static Deck()
        {
            _random = new Random();
        }

        public Deck(List<Card> cards)
        {
            _cards = new List<Card>(cards);
        }

        public void ShowCards()
        {
            foreach (Card card in _cards)
            {
                card.ShowInfo();
                Console.Write($" ");
            }
        }

        public void Shuffle()
        {
            Card temporaryCard;

            for (int i = 0; i < _cards.Count; i++)
            {
                int anyIndex = _random.Next(_cards.Count);

                temporaryCard = _cards[i];
                _cards[i] = _cards[anyIndex];
                _cards[anyIndex] = temporaryCard;
            }
        }

        public Card GiveCard()
        {
            int upperCard = 0;

            Card card = _cards[upperCard];

            _cards.Remove(card);

            return card;
        }

        public int GetLength()
        {
            return _cards.Count;
        }
    }

    class Maker
    {
        private string[] _ranks;
        private string[] _suits;        

        public Maker()
        {
            _ranks = new string[] { "2", "3", "4", "5", "6", "7", "8", "9", "10", "В", "Д", "К", "Т" };
            _suits = new string[] { "♠", "♣", "♦", "♥" };
        }

        public Deck CreateNewDeck()
        {
            List<Card> cards = new List<Card>();
            
            for (int i = 0; i < _ranks.Length; i++)
            {
                for (int j = 0; j < _suits.Length; j++)
                {
                    cards.Add(new Card(_ranks[i], _suits[j]));
                }
            }

            return new Deck(cards);
        }
    }

    class Player
    {
        public List<Card> Cards { get; private set; }

        public Player()
        {
            Cards = new List<Card>();
        }


        public void TakeCard(Card card)
        {
            Cards.Add(card);
        }

        public void ShowCards()
        {
            foreach (Card card in Cards)
            {
                card.ShowInfo();
            }
        }
    }

    class Croupier
    {
        private Maker _maker;
        private Player _player;
        private Deck _deck;

        public Croupier()
        {
            _maker = new Maker();
            _player = new Player();

            _deck = _maker.CreateNewDeck();
            _deck.Shuffle();
        }

        public void Work()
        {   
            const string TakeCardCommand = "1";
            const string ExitCommand = "3";

            bool isExit = false;

            while (isExit == false)
            {
                Console.Clear();
                Console.WriteLine($"Колода карт");
                Console.WriteLine($"{TakeCardCommand}. Взять карту");
                Console.WriteLine($"{ExitCommand}. Выход");

                Console.WriteLine($"\nУ вас рентгеновское зрение и Вы видите колоду карт насквозь:");
                _deck.ShowCards();

                Console.WriteLine($"\n\nКарты в Вашей руке:");
                _player.ShowCards();

                Console.WriteLine($"\nВведите номер команды:");
                string userUnput = Console.ReadLine();

                switch (userUnput)
                {   
                    case TakeCardCommand:
                        HandOverCard();
                        break;
                    case ExitCommand:
                        isExit = true;
                        break;
                    default:
                        Console.WriteLine($"Ошибка ввода команды");
                        break;
                }
            }
        }

        private void HandOverCard()
        {
            if (_deck.GetLength() == 0)
            {
                Console.WriteLine($"В колоде нет карт");
                Console.ReadKey();
                return;
            }

            Card card = _deck.GiveCard();

            _player.TakeCard(card);
        }
    }
}

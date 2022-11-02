using System;
using System.Collections.Generic;

namespace PlayingCards
{
    class Program
    {
        static void Main(string[] args)
        {
            Dealer dealer = new Dealer();
            dealer.Work();
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

        public List<Card> Cards { get; private set; }

        static Deck()
        {
            _random = new Random();
        }

        public Deck(List<Card> cards)
        {
            Cards = cards;
        }

        

        public void ShowCards()
        {
            foreach (Card card in Cards)
            {
                card.ShowInfo();
                Console.Write($" ");
            }
        }

        public void Shuffle()
        {
            Card temporaryCard;

            for (int i = 0; i < Cards.Count; i++)
            {
                int anyIndex = _random.Next(Cards.Count);

                temporaryCard = Cards[i];
                Cards[i] = Cards[anyIndex];
                Cards[anyIndex] = temporaryCard;
            }
        }

        public Card GiveCard()
        {
            int upperCard = 0;

            Card card = Cards[upperCard];

            Cards.Remove(card);

            return card;
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

        public Deck CreateNewDeck(int cardsCount)
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
        public Player()
        {
            Cards = new List<Card>();
        }

        public List<Card> Cards { get; private set; }

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

    class Dealer
    {
        private Maker _maker;
        private Deck _deck;
        private int _cardsCountInNewDeck;
        private Player _player;

        public Dealer()
        {
            _maker = new Maker();
            _player = new Player();

            _cardsCountInNewDeck = 52;
            _deck = _maker.CreateNewDeck(_cardsCountInNewDeck);
        }

        public void Work()
        {
            const string OpenNewDeckCommand = "1";
            const string ShufleCommand = "2";
            const string TakeCardCommand = "3";
            const string ExitCommand = "4";

            bool isExit = false;

            while (isExit == false)
            {
                Console.Clear();
                Console.WriteLine($"Колода карт");
                Console.WriteLine($"{OpenNewDeckCommand}. Открыть новую колоду");
                Console.WriteLine($"{ShufleCommand}. Перемешать колоду");
                Console.WriteLine($"{TakeCardCommand}. Взять карту");
                Console.WriteLine($"{ExitCommand}. Выход");

                Console.WriteLine($"\nУ вас рентгеновское зрение и Вы видите колоду карт насквозь:");
                _deck.ShowCards();

                Console.WriteLine($"\nКарты в Вашей руке:");
                _player.ShowCards();

                Console.WriteLine($"\nВведите номер команды:");
                string userUnput = Console.ReadLine();

                switch (userUnput)
                {
                    case OpenNewDeckCommand:
                        OpenNewDeck();
                        break;
                    case ShufleCommand:
                        _deck.Shuffle();
                        break;
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

        private void OpenNewDeck()
        {
            int cardsCountInNewDeck = GetCardsCount();

            _deck = _maker.CreateNewDeck(cardsCountInNewDeck);
        }

        private int  GetCardsCount()
        {
            const string CardsCount52 = "1";
            const string CardsCount36 = "2";
            const string CardsCount32 = "3";

            int cardsCountInNewDeck;

            Console.Clear();
            Console.WriteLine($"Выберите количество карт в колоде:");
            Console.WriteLine($"{CardsCount52}. 52 карты");
            Console.WriteLine($"{CardsCount36}. 36 карт");
            Console.WriteLine($"{CardsCount32}. 32 карты");

            string userInput = Console.ReadLine();

            switch (userInput)
            {
                default:
                case CardsCount52:
                    cardsCountInNewDeck = 52;
                    break;
                case CardsCount36:
                    cardsCountInNewDeck = 36;
                    break;
                case CardsCount32:
                    cardsCountInNewDeck = 32;
                    break;
            }

            return cardsCountInNewDeck;
        }

        private void HandOverCard()
        {
            if (_deck.Cards.Count == 0)
            {
                Console.WriteLine($"В колоде нет карт");
                return;
            }
            
            Card card = _deck.GiveCard();

            _player.TakeCard(card);
        }
    }
}

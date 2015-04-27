using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//TODO: 

//FIX THE STRAIGHT ALGORITHM
namespace PokerPlayer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ReadKey();
        }
    }
    class PokerPlayer
    {
        public List<Card> Hand { get; set; }




        public void DrawHand(List<Card> cards)
        {
            Hand = cards;

        }
        // Enum of different hand types
        public enum HandType
        {
            RoyalFlush,
            StraightFlush,
            FourOfAKind,
            FullHouse,
            Flush,
            Straight,
            ThreeOfAKind,
            TwoPair,
            OnePair,
            HighCard,

        }
        // Rank of hand that player holds
        public HandType HandRank
        {
            get
            {

                if (HasRoyalFlush())
                {
                    return HandType.RoyalFlush;
                }
                else if (HasStraightFlush())
                {
                    return HandType.StraightFlush;
                }
                else if (HasFourOfAKind())
                {
                    return HandType.FourOfAKind;
                }
                else if (HasFullHouse())
                {
                    return HandType.FullHouse;
                }
                else if (HasFlush())
                {
                    return HandType.Flush;
                }
                else if (HasStraight())
                {
                    return HandType.Straight;
                }
                else if (HasThreeOfAKind())
                {
                    return HandType.ThreeOfAKind;
                }
                else if (HasTwoPair())
                {
                    return HandType.TwoPair;
                }
                else if (HasPair())
                {
                    return HandType.OnePair;
                }
                return HandType.HighCard;
            }
        }
        // Constructor that isn't used
        public PokerPlayer() { }
        //does player have one pair
        public bool HasPair()
        {
            //group by rank
            //order lists by count
            //if the first group has 2 cards in it
            if (Hand.GroupBy(x => x.CardRank).OrderByDescending(x => x.Count()).First().Count() == 2)
            {
                //one pair
                return true;
            }
            return false;
        }
        public bool HasTwoPair()
        {
            //group by rank
            //orer lists by count
            //if the first group has 2 cards
            //and if second group has 2 cards
            if (Hand.GroupBy(x => x.CardRank).OrderByDescending(x => x.Count()).First().Count() == 2 && Hand.GroupBy(x => x.CardRank).OrderByDescending(x => x.Count()).Skip(1).First().Count() == 2)
            {
                //2 pairs
                return true;
            }
            return false;
        }
        public bool HasThreeOfAKind()
        {
            //group by rank
            //order by count
            //if the first group has 3 cards in it
            if (Hand.GroupBy(x => x.CardRank).OrderByDescending(x => x.Count()).First().Count() == 3)
            {
                //3 of a kind
                return true;
            }
            return false;
        }
        public bool HasStraight()
        {
            Hand = Hand.OrderByDescending(x => x.CardRank).ToList();
            List<Card> tempList = new List<Card>();
            for (int i = 0; i < Hand.Count() - 1; i++)
            {


                //if ace is high card
                //if second card in descending order is 5
                //AND if all cards are distint
                //5 4 3 2 are only combinationn of cards that leaves 5 after ace
                //when counting downwards, if they are not distinct, not a run
                if (Hand.Any(x => x.CardRank == Rank.Ace) && Hand.OrderByDescending(x => x.CardRank).Skip(1).First().CardRank == Rank.Five && Hand.Distinct().Count() == 5)
                {
                    return true;
                }
                // if there is no ace
                //see if cards are in order
                else if (Hand[i].CardRank - 1 == Hand[i+1].CardRank)
                {
                    //if 2 adjacent cards are in order add new card to list
                    tempList.Add(Hand.ElementAt(i));
                    //if 4 cards have been successfully added AND
                    //to avoid comparing the last card to nothing
                    if (i == 3 && tempList.Count() == 4)
                    {
                        return true;
                    }
                    
                }
            }
            return false;
        }
        public bool HasFlush()
        {
            //if the suit count is 1
            if (Hand.GroupBy(x => x.CardSuit).Distinct().Count() == 1)
            {
                return true;
            }
            return false;
        }
        public bool HasFullHouse()
        {
            //group by rank
            //order by descending
            //if the first list has 3 objects
            //if second list has 2 objects
            if (Hand.GroupBy(x => x.CardRank).OrderByDescending(x => x.Count()).First().Count() == 3 && Hand.GroupBy(x => x.CardRank).OrderByDescending(x => x.Count()).Skip(1).First().Count() == 2)
            {
                return true;
            }
            return false;
        }
        public bool HasFourOfAKind()
        {
            //group by rank
            //order by descending
            //if first list has 4 objects in it
            if (Hand.GroupBy(x => x.CardRank).OrderByDescending(x => x.Count()).First().Count() == 4 && Hand.GroupBy(x=>x.CardSuit).Distinct().Count() == 4)
            {
                return true;
            }
            return false;
        }
        public bool HasStraightFlush()
        {
            //check to see if suit count is 1
            //if all cards are distinct

            for (int i = 0; i < Hand.Count() - 2; i++)
            {
                
                Hand = Hand.OrderByDescending(x => x.CardRank).ToList();
                List<Card> tempList = new List<Card>();
                //this is a royal flush
                if (Hand.Any(x=>x.CardRank == Rank.Ace))
                {

                    return false;
                }
                else if (Hand[i].CardRank - 1 == Hand[i + 1].CardRank  && Hand.GroupBy(x => x.CardSuit).Distinct().Count() == 1 && Hand.GroupBy(x => x.CardRank).Distinct().Count() == 5)
                {
                    tempList.Add(Hand.ElementAt(i));
                    //if 4 cards have been successfully added AND
                    //to avoid comparing the last card to nothing
                    if (i == 3 && tempList.Count() == 4)
                    {
                        return true;
                    }
                    return true;
                }
            }

            //check to see if suit count is 1

            return false;
        }
        public bool HasRoyalFlush()
        {
            //make sure suit count is 1
            //if all cards are distinct
            //if first card is an ace
            //if last card is a 10
            if (Hand.GroupBy(x => x.CardSuit).Distinct().Count() == 1 && Hand.OrderBy(x => x.CardRank).Distinct().Count() == 5 && Hand.OrderByDescending(x => x.CardRank).First().CardRank == Rank.Ace && Hand.OrderByDescending(x => x.CardRank).Skip(4).First().CardRank == Rank.Ten)
            {
                return true;
            }
            return false;
        }
    }
    //Guides to pasting your Deck and Card class

    //  *****Deck Class Start*****


    // When a new deck is created, you’ll create a card of each rank for each suit and add them to the deck of cards, 
    //      which in this case will be a List of Card objects.
    //
    // A deck can perform the following actions:
    //     void Shuffle() -- Merges the discarded pile with the deck and shuffles the cards
    //     List<card> Deal(int numberOfCards) - returns a number of cards from the top of the deck
    //     void Discard(Card card) / void Discard(List<Card> cards) - returns a card from a player to the 
    //         discard pile	
    // 
    // A deck knows the following information about itself:
    //     int CardsRemaining -- number of cards left in the deck
    //     List<Card> DeckOfCards -- card waiting to be dealt
    //     List<Card> DiscardedCards -- cards that have been played
    class Deck
    {
        Random rng = new Random();

        public List<Card> DeckOfCards { get; set; }
        public List<Card> DiscardedCards { get; set; }

        public int CardsRemaining
        {
            get
            {
                return DeckOfCards.Count();
            }
        }

        public Deck()
        {
            this.DeckOfCards = new List<Card>();
            this.DiscardedCards = new List<Card>();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 2; j < 15; j++)
                {
                    DeckOfCards.Add(new Card(i, j));
                }
            }
        }

        public List<Card> Deal(int numOfCards)
        {
            List<Card> dealtCards = new List<Card>();
            for (int i = 0; i < numOfCards; i++)
            {
                Card tempCard = DeckOfCards[0];
                dealtCards.Add(tempCard);
                DeckOfCards.Remove(tempCard);
            }
            return dealtCards;
        }
        public void Discard(Card card)
        {
            DiscardedCards.Add(card);
            DeckOfCards.Remove(card);
        }
        public void Discard(List<Card> cards)
        {
            foreach (Card card in cards)
            {

                DiscardedCards.Add(card);
                DeckOfCards.Remove(card);

            }
        }
        public void Shuffle()
        {
            List<Card> shuffleList = new List<Card>();

            shuffleList.Clear();
            int deckSize = DeckOfCards.Count();
            for (int i = 0; i < deckSize; i++)
            {

                Card tempCard = DeckOfCards.ElementAt(rng.Next(0, DeckOfCards.Count()));
                shuffleList.Add(tempCard);
                DeckOfCards.Remove(tempCard);
            }
            DeckOfCards = shuffleList;
        }

    }


    // What makes a card?
    //     A card is comprised of it’s suit and its rank.  Both of which are enumerations.
    //     These enumerations should be "Suit" and "Rank"


    //  *****Deck Class End*******

    //  *****Card Class Start*****



    //  *****Card Class End*******
    public enum Rank
    {
        Two = 2,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King,
        Ace
    }
    public enum Suit
    {
        Club,
        Diamond,
        Heart,
        Spade
    }
    class Card
    {


        public Rank CardRank { get; set; }
        public Suit CardSuit { get; set; }

        public Card(int rank, int suit)
        {
            this.CardRank = (Rank)rank;
            this.CardSuit = (Suit)suit;
        }

    }


}

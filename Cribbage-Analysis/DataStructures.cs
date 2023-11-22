using System;
using HandCalculations;

namespace DataStructures
{
    /* A class that represents a card in a standard 52 card deck.
    Holds a suit and value field, where suit is a char with a value
    of 'H' for Hearts, 'D' for diamonds, 'C' for Clubs, or 'S' for
    Spades. The value field is a string that can be from "1" to "10"
    or "J" for Jack, "Q" for Queen, or "K" for King.*/
    class Card
    {
        //Fields

        /* The suit of the card, as represented as a char.
        Possible values are H for Hearts, D for Diamonds,
        S for Spades, or C for Clubs.*/
        private char suit;
        public char Suit
        { get { return suit;}}
        
        /* The value of the card, as represented by a char.
        Can be a digit from 1-10 or J, Q, K.*/
        private string value;
        public string Value
        { get { return value;}}

        /* The integer value of a card, used when adding
        cards up to fifteen. Number cards have the same
        value as their number, face cards have a value of 10.*/
        private byte intValue;
        public byte IntValue
        { get { return intValue;}}

        /* The value of a card as represented as an it by it's placement
        in the sequence from 1 to K. Number cards have the same
        value as their number, J has a value of 11, Q of 12, K of 13.*/
        private byte orderValue;
        public byte OrderValue
        { get { return orderValue;}}


        //Constructors

        /* Basic constructor that takes the Card's suit and value
        as parameters. Checks that values are valid and throws a
        InvalidDataException if they aren't. Assigns intValue and
        orderValue based on the value of the card.*/
        public Card(char _suit, string _value)
        {
            //Console.WriteLine(_suit + "_" + _value);
            if(_suit != 'H' && _suit != 'D' && _suit != 'S' && _suit != 'C')
            {
                throw new InvalidDataException();
            }

            suit = _suit;
            value = _value;

            if(Char.IsNumber(_value[0]))
            {
                if(Convert.ToByte(_value) < 0 || Convert.ToByte(_value) > 10)
                {
                    throw new InvalidDataException();
                }
                intValue = Convert.ToByte(_value);
                orderValue = Convert.ToByte(_value);
            }
            else if(_value == "J")
            {
                intValue = 10;
                orderValue = 11;
            }
            else if(_value == "Q")
            {
                intValue = 10;
                orderValue = 12;
            }
            else if(_value == "K")
            {
                intValue = 10;
                orderValue = 13;
            }
            else{
                throw new InvalidDataException();
            }
        }

        /* A copy constructor that makes the new card
        identical to the one used as a parameter.*/
        public Card(Card original)
        {
            suit = original.Suit;
            value = original.Value;
            intValue = original.IntValue;
            orderValue = original.OrderValue;
        }


        /* Returns a string of the format (value)_(suit) */
        public override string ToString()
        {
            return Value + "_" + Suit;
        }

        
        //Copy Methods

        /* A method that returns a copy of this Card.*/
        public Card copy()
        {
            return new Card(this);
        }
        
        /* A method that returns a copy of the
        Card parameter.*/
        public static Card copy(Card card)
        {
            return new Card(card);
        }
        
        /* A method that returns an array with elements identical
        to the Cards in the parameter array.*/
        public static Card[] copy(Card [] cards)
        {
            int length = cards.Length;
            Card [] result = new Card [length];

            for(int i = 0; i < length; i++)
            {
                result[i] = new Card(cards[i]);
            }

            return result;
        }
        

        //Sorting Methods

        /* Takes an array of Cards as a parameter and sorts
        that array in descending order. Return a reference 
        to that array.*/
        public static Card[] sort(Card[] cards)
        {
            int j = 1;
            while(j < cards.Length)
            {
                int k = j;
                while(k > 0 && cards[k] > cards[k-1])
                {//Swap values until value is smaller or equal to the earlier index.
                    //Record value to be swapped.
                    Card card = cards[k-1];
                    //Move over other value.
                    cards[k-1] = cards[k];
                    //Put new value into other spot.
                    cards[k] = card;
                    k--;//Decrement inner index
                }
                j++;//Increment Outer index
            }

            return cards;
        }

        /* Returns a reference to a sorted copy of the
        Card array parameter. */
        public static Card [] getSortedCopy(Card [] cards)
        {
            int length = cards.Length;
            Card [] result = new Card [length];

            for(int i = 0; i < length; i++)
            {
                result[i] = new Card(cards[i]);
            }

            result = Card.sort(result);
            return result;
        }

        //Operator Override Methods - Based on OrderValue.

        /* Override method for the == operator that returns true
        if the value of both cards is the same based on orderValue.*/
        public static bool operator == (Card card1, Card card2)
        {
            return card1.OrderValue == card2.OrderValue;
        }
        
        /* Override method for the != operator that returns true
        if the value of both cards is not the same based on orderValue.*/
        public static bool operator != (Card card1, Card card2)
        {
            return card1.OrderValue != card2.OrderValue;
        }

        /* Override method for the > operator that returns true
        if the value of the first card is greater than the second
        based on orderValue.*/
        public static bool operator > (Card card1, Card card2)
        {
            return card1.OrderValue > card2.OrderValue;
        }

        /* Override method for the < operator that returns true
        if the value of the first card is less than the second
        based on orderValue.*/
        public static bool operator < (Card card1, Card card2)
        {
            return card1.OrderValue < card2.OrderValue;
        }

        /* Override method for the >= operator that returns true
        if the value of the first card is greater or equal to the second
        based on orderValue.*/
        public static bool operator >= (Card card1, Card card2)
        {
            return card1.OrderValue >= card2.OrderValue;
        }

        /* Override method for the <= operator that returns true
        if the value of the first card is greater or equal to the second
        based on orderValue.*/
        public static bool operator <= (Card card1, Card card2)
        {
            return card1.OrderValue <= card2.OrderValue;
        }

        /* Returns true if arguement is identical to this object,
        false otherwise.*/
        public bool Equals(Card card)
        {
            if(value == card.Value && suit == card.Suit)
            {
                return true;
            }
            return false;
        }
    }

    /* A class that represents a hand in a game of crib. Contains
    a Card object for the deck card, as well as an array of Cards
    representing the main cards in hand.*/
    class Hand
    {
        //Fields

        /* A reference to the deckCard in this hand. */
        private Card deckCard;

        /* A bool that is true if deckCard has been set,
        and is otherwise false.*/
        private bool hasDeckCard;
        
        /* An array with references to all of the main
        cards in the hand. Kept sorted in descending order
        based on the OrderValue of the cards.*/
        private Card [] mainCards;

        /* Stores the number of Cards in the mainCards array.*/
        private byte mainCardNum;


        //Constructors

        /* The default constructor that takes the deckCard and
        the array of mainCards as arguements.*/
        public Hand(Card _deckCard, Card[] _mainCards)
        {
            deckCard = _deckCard.copy();
            hasDeckCard = true;
            mainCards = Card.getSortedCopy(_mainCards);
            mainCardNum = (byte)_mainCards.Length;
        }

        /* A constructor that only takes an array of
        mainCards as an arguement, and changes 
        hasDeckCard to false.*/
        public Hand(Card [] _mainCards)
        {
            hasDeckCard = false;
            mainCards = Card.getSortedCopy(_mainCards);
            mainCardNum = (byte)_mainCards.Length;
        }


        /* Method that returns a string that represents
        the hand.*/
        public override string ToString()
        {
            String result = "(";
            if(hasDeckCard)
            {
                result += deckCard.ToString() + ": ";
            }
            foreach(Card c in mainCards)
            {
                result += c.ToString() + ", ";
            }
            result += ")";
            return result;
        }


        //Getters

        /* A method that returns an array of cards that includes
        both the mainCards array and the deckCard sorted into 
        descending order.*/
        public Card [] getAllCards()
        {
            Card [] allCards = new Card[mainCardNum + Convert.ToByte(hasDeckCard)];
            bool insertedDeckCard = false;
            for(int i = 0; i < mainCardNum; i++)
            { 
                if(!insertedDeckCard && hasDeckCard && (deckCard > mainCards[i]))
                { //If deckCard hasn't been inserted and is greater than this mainCard, insert deckCard
                    allCards[i] = deckCard.copy();
                    insertedDeckCard = true;
                    i--;
                }
                else if(insertedDeckCard)
                {
                    allCards[i + 1] = mainCards[i].copy();
                }
                else{
                    allCards[i] = mainCards[i].copy();
                }
            }

            if(!insertedDeckCard && hasDeckCard)
            { //If still haven't put deck card in, put it in the last element.
                allCards[allCards.Length - 1] = deckCard.copy();
            }

            return allCards;
        }

        /* Returns total number of cards in hand, including the deckCard.*/
        public byte getNumberOfCards()
        {
            return (byte)(Convert.ToByte(hasDeckCard) + mainCardNum);
        }
        
        /* A method that returns a non-referenced copy of the
        deckCard in the object. Returns null if there is no
        deckCard.*/
        public Card ? getDeckCard()
        {
            if(hasDeckCard)
            {
               return deckCard.copy(); 
            }
            return null;
        }

        /* Returns hasDeckCard field. */
        public bool getHasDeckCard()
        {
            return hasDeckCard;
        }

        /* Returns a non-referenced copy to the mainCards
        array in the object.*/
        public Card [] getMainCards()
        {
            return Card.copy(mainCards);
        }

        /* Returns number of cards in mainCards as a byte.*/
        public byte getMainCardNum()
        {
            return mainCardNum;
        }

        
        //Setters

        /* Method to set deckCard. */
        public void setDeckCard(Card newCard)
        {
            hasDeckCard = true;
            deckCard = newCard.copy();
        }

        /* Method that searches through mainCards for a particular
        Card and replaces it with another. Returns 1 if replacement
        was successful, 0 otherwise.*/
        public int changeMainCard(Card original, Card replacement)
        {
            for(int i = 0; i < mainCardNum; i++)
            {
                if(mainCards[i].Equals(original))
                {
                    mainCards[i] = replacement.copy();
                    return 1;
                }
            }
            return 0;
        }

        /* Method that searches through all cards for a particular
        Card and replaces it with another. Returns 1 if replacement
        was successful, 0 otherwise.*/
        public int changeCard(Card original, Card replacement)
        {
            if(deckCard.Equals(original))
            {
                deckCard = replacement.copy();
                return 1;
            }
            for(int i = 0; i < mainCardNum; i++)
            {
                if(mainCards[i].Equals(original))
                {
                    mainCards[i] = replacement.copy();
                    return 1;
                }
            }
            return 0;
        }


        public bool hasCard(Card card)
        {
            if(hasDeckCard && deckCard.Equals(card))
            {
                return true;
            }
            foreach(Card c in mainCards)
            {
                if (c.Equals(card))
                {
                    return true;
                }
            }
            return false;
        }


        //Copy Methods

        public Hand copy()
        {
            if(hasDeckCard)
            {
                return new Hand(this.getDeckCard(), this.getMainCards());
            }
            else{
                return new Hand(this.getMainCards());
            }
        }
    }
}
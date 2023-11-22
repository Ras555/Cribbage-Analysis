// See https://aka.ms/new-console-template for more information

using System;
using HandCalculations;
using Statistics;
using DataStructures;

namespace Runners
{
    class CribHandStatistics
    {

        public static Card[] deck;

        public static void Main(string[] args)
        {
            Console.WriteLine("Beginning of Program!");

            string[] num = {"1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K"};
            char [] suit = {'H', 'D', 'S', 'C'};
            
            deck = new Card [52];

            //Console.WriteLine("Initializing complete.");

            int i = 0;
            foreach(char s in suit)
            {
                foreach(string n in num)
                {
                    Card card = new Card(s, n);
                    deck[i] = card;
                    i++;
                }
            }

            //Console.WriteLine("Deck array initialized. Deck length is: " + deck.Length);

            //printDeck();

            /* Tests for Card, Hand, and methods.
            Card [] array1 = new Card[] {deck[3], deck[47], deck[37], deck[5], deck[2]};
            foreach(Card c in array1)
            {
                Console.Write(c.ToString() + " ");
            }
            Console.WriteLine();
            Card.sort(array1);
            foreach(Card c in array1)
            {
                Console.Write(c.ToString() + " ");
            }
            Console.WriteLine();
            Card[] array2 = Card.copy(array1);
            array2[2] = deck[25];
            array2[4] = deck[6];
            foreach(Card c in array1)
            {
                Console.Write(c.ToString() + " ");
            }
            Console.WriteLine();
            foreach(Card c in array2)
            {
                Console.Write(c.ToString() + " ");
            }
            Console.WriteLine();
            Card[] array3 = Card.getSortedCopy(array2);
            foreach(Card c in array2)
            {
                Console.Write(c.ToString() + " ");
            }
            Console.WriteLine();
            foreach(Card c in array3)
            {
                Console.Write(c.ToString() + " ");
            }
            Console.WriteLine();

            Hand hand1 = new Hand(deck[23], new Card[] {deck[18], deck[17], deck[5], deck[50]});
            Console.WriteLine(hand1.ToString());
            */

            /* Tests for calculateHandValue()
            Hand hand = new Hand(deck[10], new Card[]{deck[40], deck[38], deck[28], deck[15]});
            Console.WriteLine(hand.ToString());

            Console.WriteLine("Checking calculateHandValue()");
            int points = HandCalculator.calculateHandValue(hand);
            Console.WriteLine("Points found in hand are: " + points);

            hand = new Hand(deck[4], new Card[]{deck[17], deck[30], deck[43], deck[1]});
            Console.WriteLine(hand.ToString());
            points = HandCalculator.calculateHandValue(hand);
            Console.WriteLine("Points found in hand are: " + points);

            hand = new Hand(deck[35], new Card[]{deck[7], deck[9], deck[3], deck[24]});
            Console.WriteLine(hand.ToString());
            points = HandCalculator.calculateHandValue(hand);
            Console.WriteLine("Points found in hand are: " + points);

            hand = new Hand(deck[7], new Card[]{deck[1], deck[4], deck[20], deck[33]});
            Console.WriteLine(hand.ToString());
            points = HandCalculator.calculateHandValue(hand);
            Console.WriteLine("Points found in hand are: " + points);

            hand = new Hand(deck[0], new Card[]{deck[13], deck[1], deck[14], deck[2]});
            Console.WriteLine(hand.ToString());
            points = HandCalculator.calculateHandValue(hand);
            Console.WriteLine("Points found in hand are: " + points);

            hand = new Hand(deck[6], new Card[]{deck[7], deck[20], deck[33], deck[34]});
            Console.WriteLine(hand.ToString());
            points = HandCalculator.calculateHandValue(hand);
            Console.WriteLine("Points found in hand are: " + points);

            hand = new Hand(deck[5], new Card[]{deck[6], deck[7], deck[8], deck[22]});
            Console.WriteLine(hand.ToString());
            points = HandCalculator.calculateHandValue(hand);
            Console.WriteLine("Points found in hand are: " + points);

            hand = new Hand(deck[18], new Card[]{deck[19], deck[20], deck[21], deck[22]});
            Console.WriteLine(hand.ToString());
            points = HandCalculator.calculateHandValue(hand);
            Console.WriteLine("Points found in hand are: " + points);

            hand = new Hand(deck[12], new Card[]{deck[11], deck[48], deck[25], deck[5]});
            Console.WriteLine(hand.ToString());
            points = HandCalculator.calculateHandValue(hand);
            Console.WriteLine("Points found in hand are: " + points);

            hand = new Hand(deck[25], new Card[]{deck[15], deck[16], deck[17], deck[20]});
            Console.WriteLine(hand.ToString());
            points = HandCalculator.calculateHandValue(hand);
            Console.WriteLine("Points found in hand are: " + points);*/

            /*Tests for statistic classes and file printing.
            CribStats stats = new CribStats();

            stats.printHandStatistics();
            stats.printHandValueStats();

            stats.foundValue(8);
            stats.foundValue(12);
            stats.foundValue(8);
            stats.foundValue(2);

            stats.newHand(new Hand( new Card [] {deck[6], deck[7], deck[20], deck[33], deck[34], deck[50]}), 
                new Hand( new Card[] {deck[6], deck[7], deck[20], deck[33]}), 12.5F);
            stats.newHand(new Hand(new Card[]{deck[10], deck[40], deck[38], deck[28], deck[15], deck[40]}), 
                new Hand(new Card[]{deck[10], deck[40], deck[38], deck[28]}), 7.25F);

            stats.printHandStatistics();
            stats.printHandValueStats();*/
            
            CribStats stats = new CribStats();
            stats.createHandStatisticFile();
            stats = CribbageHandRunner.twoPlayerHandAnalysis(stats);

            

            Console.WriteLine("Completed Analysis. Now Printing Stats in Files.");

            stats.printHandValueStats();

            Console.WriteLine("Printing to Files Complete. Closing Program.");
        }

        public static void printDeck()
        {
            Console.WriteLine("Entered printDeck()...");
            foreach(Card card in deck)
            {
                Console.Write(card.ToString() + ", ");
            }
            Console.WriteLine("Completed printDeck()...");
        }
    }
}

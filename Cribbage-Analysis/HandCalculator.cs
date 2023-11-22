using System;
using System.Windows.Markup;
using Runners;
using Statistics;
using DataStructures;
using System.Timers;
using Microsoft.Win32.SafeHandles;

namespace HandCalculations
{
    class HandCalculator
    {

        /* A method to calculate the value of a hand in 
        crib. The last string in the string array parameter
        must be the card on top of the deck.
        */
        public static int calculateHandValue(Hand hand)
        {
            if(!hand.getHasDeckCard() || hand.getMainCardNum() != 4)
            {
                throw new InvalidDataException();
            }

            int points = 0;
            int found = 0;
            
            found = findFifteens(hand); //Calculate fifteens in hand
            points += found;
            //Console.WriteLine("Number of points from fifteens: " + found);

            found = findPairs(hand); //Calculate pairs in hand
            points += found;
            //Console.WriteLine("Number of points from pairs: " + found);

            found = findRuns(hand); //Calculate runs in hand
            points += found;
            //Console.WriteLine("Number of points from runs: " + found);

            found = sameSuit(hand); //Determines whether there is a point from the jack.
            points += found;
            //Console.WriteLine("Points from cards being the same suit: " + found);

            found = jackPoint(hand); //Determines whether there is a point from the jack.
            points += found;
            //Console.WriteLine("Points from jack matching: " + found);
            
            //Console.WriteLine("Total points in hand: " + points);
            return points;
        }
    
        /* A method to calculate the amount of points
        from fifteens in a hand. Input is the hand as an
        array of strings and the output is an integer.*/
        private static int findFifteens(Hand hand)
        {
            Card[] allCards = hand.getAllCards();

            //Loop to calculate the amount of fifteens in the hand.
            int points = 0; //Amount of points from fifteens.
            bool [] included = {true, false, false, false, false}; //Array to signify what numbers are included in the total.
            int i = 0; //Index of current card to be looked at (included/excluded)

            int total = 0; //Total sum of all included cards.
            while (true)
            {
                //Include initial card
                included[i] = true; 
                total += allCards[i].IntValue;
                //Console.WriteLine("Included card: " + i);

                //Console.WriteLine("Total is: " + total);

                //If sum is 15, increase points
                if(total == 15)
                {
                    points += 2;
                    //Console.WriteLine("Found fifteen!");
                }

                i++; //Go to next card

                //If sum is less than 15, include following cards until sum is 15 or greater.
                while(i < allCards.Length && total < 15)
                {
                    //Include next card.
                    included[i] = true;
                    total += allCards[i].IntValue;
                    //Console.WriteLine("Included card: " + i);

                    //Console.WriteLine("Total is: " + total);

                    //If sum equals fifteen, increase points.
                    if(total == 15)
                    {
                        points += 2;
                        //Console.WriteLine("Found fifteen!");
                    }
                    //If sum is greater or equal to fifteen, uninclude last included number.
                    if(total >= 15)
                    {
                        included[i] = false;
                        total -= allCards[i].IntValue;
                        //Console.WriteLine("Unincluded card: " + i);
                    }

                    i++;
                }

                //Console.WriteLine("Finished loop.");

                //If last card has not been unincluded (because the total was less than fifteen),
                //uninclude it.
                i--;
                if(included[i] == true)
                {
                    included[i] = false;
                    total -= allCards[i].IntValue;
                    //Console.WriteLine("Unincluded card: " + i);
                }

                /*//If sum is less than fifteen, all fifteens have been found.
                if(total < 15)
                {
                    break;
                }*/

                //Find next smallest included card, uninclude it, and set i to the next index.
                while(i > 0 && included[i-1] == false)
                {
                    i--;
                }
                if(i == 0)
                {
                    break;
                }
                total -= allCards[i-1].IntValue;
                included[i-1] = false;
                //Console.WriteLine("Unincluded card: " + (i - 1));
            }

            return points;
        }

        /* A method to calculate the amount of points
        from pairs in a hand. Takes the hand as an
        array of strings as it's input and outputs
        an integer.*/
        private static int findPairs(Hand hand)
        {
            Card[] allCards = hand.getAllCards();

            int points = 0;

            int i = 0; //First card to compare
            while(i < allCards.Length - 1)
            {
                int j = i+1;
                while (j < allCards.Length)
                {
                    //If card types are the same, gain points.
                    if(allCards[i].Value == allCards[j].Value)
                    {
                        //Console.WriteLine("Found " + hand[i] + " and " + hand[j]);
                        points += 2;
                    }
                    else{
                        break;
                    }
                    j++; //Increment second card
                }
                i++;
            }
            return points;
        }

        /* A method to calculate the amount of points
        from runs in the hand. Take the hand as an array
        of strings as an input and outputs an integer.*/
        private static int findRuns(Hand hand)
        {
            Card[] allCards = hand.getAllCards();

            int[] values = new int[13];

            //Create int array containing number of card values represented in the hand, such as 3 5's and 2 6's
            //  Considers J: 11, Q: 12, K: 13 - Note: Values represented in index one less: K in 12, 10 in 9
            for(int i = 0; i < allCards.Length; i++)
            {
                values[allCards[i].OrderValue - 1] ++;            
            }

            //Console.WriteLine("Finished initializing value array...");
            int points = 0;
            int j = 12; //Initial point in array
            int runLength = 0; //Number of cards in a row

            while(j >= 0) //Loop from K until you've reached the last number (1).
            {
                //Console.WriteLine("Current value is: " + j);
                if(values[j] > 0) //If there is one of this number, increase runLength.
                {
                    runLength++;
                    //Console.WriteLine("Run Length is: " + runLength);
                }
                if(values[j] == 0 || j == 0) //If there is none of this number:
                {
                    if(runLength >= 3){ //Check whether runLength is equal to or greater than 3

                        if(values[j] == 0)
                        {
                            j++;
                        }
                        int runs = 1;
                        for(int k = 0; k < runLength; k++)
                        {
                            runs *= values[j + k];
                        }
                        points += runs * runLength; //Increase points by product of all values involved in run.
                        break; //Exit loop - run found
                    }
                    runLength = 0; //If runLength is less than 3, set to 0 and continue loop
                }
                j--; //Decrement index.
            }
            return points;
        }

        /* A method to determine whether points are obtained
        from cards being the same suit.*/
        private static int sameSuit(Hand hand)
        {

            Card[] mainCards = hand.getMainCards();

            bool sameSuit = true;
            int points = 0;
            char suit = mainCards[0].Suit;
            //Console.WriteLine("The suit is: " + suit);

            foreach(Card card in mainCards)
            {
                if(card.Suit!= suit)
                {
                    sameSuit = false;
                    break;
                }
            }

            if(sameSuit)
            {
                points += 4;
                if(hand.getHasDeckCard() && hand.getDeckCard().Suit == suit)
                {
                    points += 1;
                }
            }
            return points;
        }

        /* A method to determine whether the hand gains a point
        from a jack matching the deck card in suit.*/
        private static int jackPoint(Hand hand)
        {
            if(!hand.getHasDeckCard())
            {
                throw new InvalidDataException();
            }

            Card[] mainCards = hand.getMainCards();
            foreach(Card card in mainCards)
            {
                if(card.Value == "J" && hand.getDeckCard().Suit == card.Suit)
                {
                    return 1;
                }
            }
            return 0;
        }
    }

    class CribbageHandRunner
    {
        public static CribStats twoPlayerHandAnalysis(CribStats stats)
        {
            int loops= 0;
            const int total = 20358520;

            // Create a timer with a five second interval.
            System.Timers.Timer timer = new System.Timers.Timer(60000);
            // Hook up the Elapsed event for the timer. 
            timer.Elapsed += (s,e) => Console.WriteLine("Analysis is {0}% complete...", 100D * (double)loops/(double)total);
            timer.AutoReset = true;
            timer.Enabled = true;

            int [] cards = new int[6];
            //For each combination of hands: 6 - (4 - index) + 1: 52 - (6 - index - 1) 
            for(cards[0] = 0; cards[0] < 47; cards[0]++)
            {
                //cards[0] = 0; - For when for loop is disabled
                for(cards[1] = cards[0] + 1; cards[1] < 48 ; cards[1]++)
                {
                    //cards[1] = cards[0] + 1; - for when for loop is disabled.
                    for(cards[2] = cards[1] + 1; cards[2] < 49; cards[2]++)
                    {
                        for(cards[3] = cards[2] + 1; cards[3] < 50; cards[3]++)
                        {
                            for(cards[4] = cards[3] + 1; cards[4] < 51; cards[4]++)
                            {
                                for(cards[5] = cards[4] + 1; cards[5] < 52; cards[5]++)
                                {
                                    Hand hand = new Hand (new Card[]{CribHandStatistics.deck[cards[0]],
                                        CribHandStatistics.deck[cards[1]], 
                                        CribHandStatistics.deck [cards[2]], CribHandStatistics.deck[cards[3]],
                                        CribHandStatistics.deck [cards[4]], CribHandStatistics.deck[cards[5]]});
                                    
                                    stats = twoPlayerHandOptimizer(hand, stats);
                                    loops++;
                                }
                            }                            
                        }
                    }
                }
                stats.printHandStatistics();
            }           

            return stats;
        }

        private static CribStats twoPlayerHandOptimizer(Hand hand, CribStats stats)
        {
            //Check to make sure parameters are valid:
            if(hand.getHasDeckCard() || hand.getMainCardNum() != 6)
            {
                throw new InvalidDataException();
            }

            Card [] newDeck = new Card[46];
            //Console.WriteLine("Total deck: " + CribHandStatistics.deck.Length
            //    + " Incoming hand: " + hand.getNumberOfCards() + " New Deck: " + newDeck.Length);

            int i = 0;
            for(int j = 0; j < CribHandStatistics.deck.Length; j++)
            {
                if(!hand.hasCard(CribHandStatistics.deck[j]))
                {
                    newDeck[i] = CribHandStatistics.deck[j];
                    i++;
                }
            }

            //Console.WriteLine(hand.ToString());

            Card[] cardArr = hand.getMainCards();

            int [] keeps = new int [4];

            float greatestAverage = 0;
            int [] greatestValues = new int[0];
            Hand greatestHand = new Hand(new Card[]{});

            //Loops through all combinations of four cards from the
            //original six. 
            for(keeps[0] = 0; keeps[0] < 3; keeps[0]++)
            {
                for(keeps[1] = keeps[0] + 1; keeps[1] < 4; keeps[1]++)
                {
                    for(keeps[2] = keeps[1] + 1; keeps[2] < 5; keeps[2]++)
                    {
                        for(keeps[3] = keeps[2] + 1; keeps[3] < 6; keeps[3]++)
                        {
                            Hand keepCards = new Hand (new Card[]{cardArr[keeps[0]],
                                cardArr[keeps[1]], cardArr [keeps[2]], cardArr[keeps[3]]});
                            //Console.WriteLine(keepCards.ToString());
                            int [] values = findPossibleValues(keepCards, newDeck);
                            float average = 0;
                            foreach(int value in values)
                            {
                                average += value;
                            }

                            average /= 46;

                            //Console.WriteLine("Hand average is: " + average);

                            if(average > greatestAverage)
                            {
                                greatestAverage = average;
                                greatestValues = values;
                                greatestHand = keepCards;
                            }
                        }
                    }
                }
            }

            foreach(int value in greatestValues)
            {
                stats.foundValue(value);
            }

            stats.newHand(hand, greatestHand, greatestAverage);
            return stats;
        }

        private static int [] findPossibleValues(Hand original, Card[] deck)
        {
            //Check if parameters are valid
            if(original.getHasDeckCard() || original.getMainCardNum() != 4 || deck.Length != 46)
            {
                throw new InvalidDataException();
            }

            Hand hand = original.copy();

            int [] scores = new int [46];
                        
            for(int i = 0; i < 46; i++)
            {
                hand.setDeckCard(deck[i]);
                //Console.WriteLine(hand.ToString());
                scores[i] = HandCalculator.calculateHandValue(hand);
            }

            return scores;
        }
    }
}
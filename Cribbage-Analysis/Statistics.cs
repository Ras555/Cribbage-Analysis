

using Runners;
using System.IO;
using DataStructures;

namespace Statistics
{
    /* Class to record and print the statistics of certain
    hands and hand values in cribbage.*/
    class CribStats
    {
        Stack <HandStats> hands; //Recorded hands.
        int [] values; //Records number of each value found.

        /* A class that records information about a particular cribbage hand.
        In particular, the starting hand given, the optimal hand to keep, and
        the statistically likely (average) value if optimal cards are kept.*/
        private class HandStats
        {
            public Hand startingHand;
            public Hand optimalHand;
            public float statisticalValue;

            /* Basic constructor that takes starting hand and optimal hand
            (both as an array of strings) plus an integer value for the 
            average value of the hand.*/
            public HandStats(Hand start, Hand optimal, float value)
            {
                startingHand = start;
                optimalHand = optimal;
                statisticalValue = value;
            }

            /* Method to create string with hand stats.*/
            public string getHandSummary()
            {
                string result = startingHand.ToString();
                result += " |  ";
                result += optimalHand.ToString();
                result += "  | ";
                result += "    " + Math.Round(statisticalValue, 3) + "  ";
                return result;
            }
        }
    
        /* Basic constructor. */
        public CribStats()
        {
            values = new int[30];
            hands = new Stack<HandStats>();
        }

        /* Method that notates that a particular value has been found. */
        public void foundValue(int value)
        {
            values[value]++;
        }

        /* Method that records a new hand that has been found. Takes
        original hand, optimal hand, and the statistacally likely value
        of the hand as parameters.*/
        public void newHand(Hand original, Hand optimal, float value)
        {
            HandStats hand = new HandStats(original, optimal, value);
            hands.Push(hand);
        }

        /* Method to create or override a file that contains a record
        of hands that have been found and information about those hands.*/
        public void createHandStatisticFile()
        {
            string filename = "Crib Hand Optimization.txt";

            float sum = 0;
            foreach(HandStats hand in hands)
            {
                sum += hand.statisticalValue;
            }

            using(StreamWriter sw = File.CreateText(filename))
            {
                sw.WriteLine("This file records the optimal cards to keep from\n"
                    + " particular hands in two player crib as well as the\n"
                    + " statistically average value to obtain from that hand (when optimized.)\n");
                sw.WriteLine("Average value of all hands is: {0:0.###}", ((double)sum/ (double)hands.Count));
                sw.WriteLine("         Starting Hand         |      Optimal Hand      |    Average Value");
                sw.WriteLine("--------------------------------------------------------------------------------");
            }
        }

        public void printHandStatistics()
        {
            string filename = "Crib Hand Optimization.txt";

            using(StreamWriter sw = File.AppendText(filename))
            {
                while(hands.Count > 0)
                {
                    HandStats hand = hands.Pop();
                    sw.WriteLine(hand.getHandSummary() + "\n");
                }
            }
        }
    
        /* Method to create or override a file that contains the statistics
        regarding how often particular score values are found.*/
        public void printHandValueStats()
        {
            string filename = "Crib Hand Value Stats.txt";

            int total = 0;
            foreach(int value in values)
            {
                total += value;
            }

            using(StreamWriter sw = File.CreateText(filename))
            {
                sw.WriteLine("This file records the number of each score from hands has been recorded\n"
                    + " as well as the percentage of hands fall under that score.\n");
                sw.WriteLine("  Score  |  # Found  |  Percentage");
                sw.WriteLine("----------------------------------");

                if(total != 0)
                {
                    for(int i = 0; i < values.Length; i++)
                    {
                        sw.WriteLine("    {0:}    |     {1:}     |    {2:0.###}%",
                             i, values[i], 100 * (double)values[i]/(double)total);
                    }
                }
            }
        }
    }
}
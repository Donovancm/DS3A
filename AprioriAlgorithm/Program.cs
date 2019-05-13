using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AprioriAlgorithm
{
    class Program
    {
        public static double countXuY = 0;
        public static double countX = 0;
        public static double countY = 0;
        public static double n = 0;
        static void Main(string[] args)
        {
            Console.WriteLine("Apriori Algorithm!");

            Dictionary<int, string[]> transactions = new Dictionary<int, string[]>();
            transactions.Add(1, new string[] { "Milk", "Bread" });
            transactions.Add(2, new string[] { "Butter" });
            transactions.Add(3, new string[] { "Beer" });
            transactions.Add(4, new string[] { "Milk", "Bread", "Butter" });
            transactions.Add(5, new string[] { "Bread" });
            TableMatrix(transactions);

            string Y = "Milk";
            string[] X = { "Butter", "Bread" };

            n = transactions.Count;
            double support = CalculateSupport(transactions, Y, X);
            double confidence = CalculateConfidence(transactions, Y, X);
            double lift = CalculateLift(transactions, Y, support);
            
           
            Console.ReadLine();

            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }
        public static void TableMatrix(Dictionary<int, string[]> transactions)
        {
            string[] headers = new string[] { "Transactions", "Milk", "Bread", "Butter", "Beer" };
            double[,] array = new double[headers.Length, transactions.Count];
            //magic
            int row = array.GetLength(0);
            int col = array.GetLength(1);
            for (int i = 0; i < row; i++)
            {
                int key = i + 1;
                string[] value = transactions[key];
                // check and fill matrix
                for (int j = 0; j <= col - 1; j++)
                {
                    if (j == 0 && array[i, j] == 0)
                    {
                        array[i, j] = key;
                    }
                    for (int k = 0; k < value.Length; k++)
                    {
                        if (j > 0 && headers[j] == value[k])
                        {
                            array[i, j] = 1;
                        }
                    }

                }
            }
            PrintTableMatrix(array);
        }

        public static void PrintTableMatrix(double[,] array)
        {
            string[] headers = new string[] { "Transactions", "Milk", "Bread", "Butter", "Beer" };
            int row = array.GetLength(0);
            int col = array.GetLength(1);

            for (int k = 0; k < headers.Length; k++)
            {
                Console.Write(headers[k]+"\t");
            }
            Console.WriteLine();
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j <= col - 1; j++)
                {
                    if (j == 0)
                    {
                        Console.Write(array[i, j] + "\t" + "\t");
                    }
                    else
                    {
                        Console.Write(array[i, j] + "\t");
                    }
                }
                Console.WriteLine();
            }
        }

        public static double CalculateSupport(Dictionary<int, string[]> transactions, string Y, string[] X)
        {
            string[] combo = new string[X.Length + 1];
            for (int a = 0; a < X.Length + 1; a++)
            {
                if (a < X.Length)
                {
                    combo[a] = X[a];
                }
                else
                {
                    combo[a] = Y;
                }
            }

            foreach (var item in transactions)
            {
                string[] array = item.Value;
                Boolean foundAll = false;
                int findCombination = combo.Length;
                int countFoundComb = 0;
                for (int i = 0; i < array.Length; i++)
                {

                    // check for combination in array X items with Y
                    for (int j = 0; j < combo.Length; j++)
                    {
                        if (array[i] == combo[j])
                        {
                            countFoundComb++;
                        }
                        if (countFoundComb == findCombination) { foundAll = true; }
                    }
                    if (foundAll) { countXuY++; }
                }
            }

            double support = countXuY / n;
            Console.WriteLine("Support: " + support);
            return support;
        }
        public static double CalculateConfidence(Dictionary<int, string[]> transactions, string Y, string[] X)
        {
            foreach (var item in transactions)
            {
                string[] array = item.Value;
                Boolean foundAll = false;
                int findCombination = X.Length;
                int countFoundComb = 0;
                for (int i = 0; i < array.Length; i++)
                {

                    // check for combination in array X items with Y
                    for (int j = 0; j < X.Length; j++)
                    {
                        if (array[i] == X[j])
                        {
                            countFoundComb++;
                        }
                        if (countFoundComb == findCombination) { foundAll = true; }
                    }
                    if (foundAll) { countX++; }
                }
            }

            double confidence = countXuY / countX;
            Console.WriteLine("Confidence: " + confidence);
            return confidence;
        }
        public static double CalculateLift(Dictionary<int, string[]> transactions, string Y, double support)
        {

            foreach (var item in transactions)
            {
                string[] array = item.Value;
                for (int i = 0; i < array.Length; i++)
                {
                    // check for combination in array X items with Y
                    if (array[i] == Y)
                    {
                        countY++;
                    }
                }
            }
            double lift = support / ((countX / n) * (countY / n));
            Console.WriteLine("Lift: " + lift);
            return lift;
        }
    }
}

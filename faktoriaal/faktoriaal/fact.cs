using System.Collections.Generic;
using System.Linq;

namespace faktoriaal
{
    class fact
    {
        private static long[] readInput(string fileName)
        {
            var file = new System.IO.StreamReader(fileName);
            var text = file.ReadLine().Split(' ');
            file.Close();
            return text.Select(long.Parse).ToArray();
        }

        private static long calcFact(long input)
        {
            if (input == 1) return 1;
            var factors = primeFactorisation(input);
            var temp = new List<long>();

            for (var i = 0; i < factors.Count; i++)
            {
                if (factors.ElementAt(i).Value < factors.ElementAt(i).Key)
                    temp.Add(factors.ElementAt(i).Value * factors.ElementAt(i).Key);
                else
                {
                    var startNum = factors.ElementAt(i).Value / factors.ElementAt(i).Key;
                    if (startNum < 1) startNum = 1;
                    var found = false;
                    while (!found)
                    {
                        var factorSum = startNum;
                        var tempsum = startNum / factors.ElementAt(i).Key;
                        while (tempsum > 0)
                        {
                            factorSum = factorSum + tempsum;
                            tempsum = tempsum / factors.ElementAt(i).Key;
                        }

                        if (factorSum >= factors.ElementAt(i).Value)
                            found = true;
                        else
                            startNum++;
                    }
                    temp.Add(startNum * factors.ElementAt(i).Key);
                }
            }

            long max = 0;
            for (var i = 0; i < factors.Count; i++)
                if (temp[i] > max) max = temp[i];
            return max;
        }

        private static SortedList<long, long> primeFactorisation(long input)
        {
            var factors = new List<long>();
            while (input % 2 == 0)
            {
                factors.Add(2);
                input /= 2;
            }
            var factor = 3;
            while (factor * factor <= input)
            {
                if (input % factor == 0)
                {
                    factors.Add(factor);
                    input /= factor;
                }
                else factor += 2;
            }
            if (input > 1) factors.Add(input);

            var dict = new SortedList<long, long>();
            foreach (var item in factors)
            {
                if (dict.ContainsKey(item)) dict[item]++;
                if (!dict.ContainsKey(item)) dict.Add(item, 1);
            }
            return dict;
        }

        private static void writeOutput(long result, string fileName)
        {
            var file = new System.IO.StreamWriter(fileName);
            file.WriteLine(result);
            file.Close();
        }

        static void Main(string[] args)
        {
            var numbers = readInput("factsis.txt");
            long result = 0;
            for (var i = 0; i <= (numbers[1] - numbers[0]); i++)
            {
                result += calcFact(numbers[0] + i);
            }
            writeOutput(result, "factval.txt");
        }
    }
}

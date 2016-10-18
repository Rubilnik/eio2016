using System.Collections.Generic;
using System.Linq;

namespace medalid
{
    class Medalid
    {
        private const int Mi = 1000;
        private static long sum;

        private static List<long> readInput(string fileName)
        {
            var medals = new long[Mi + 1];
            var file = new System.IO.StreamReader(fileName);
            sum = long.Parse(file.ReadLine());
            var numbers = file.ReadLine().Split(' ');
            file.Close();

            for (long i = 0; i < sum; i++)
            {
                medals[long.Parse(numbers[i])]++;
            }
            return medals.ToList();
        }

        private static List<List<long>> findSeries(List<long> medals)
        {
            var series = new List<long>();
            var collection = new List<List<long>>();
            var foundMedals = 0;
            while (foundMedals < sum)
            {
                for (var i = 1; i <= Mi; i++)
                {
                    if (medals[i] > 0)
                    {
                        series.Add(i);
                        medals[i]--;
                        foundMedals++;
                    }
                    else if (medals[i] == 0 && series.Any()) //Avastasime nulli ning seeria on lõppenud
                    {
                        collection.Add(new List<long>(series)); //Lisa seeria listi
                        series.Clear(); //tühjenda seeria collector
                    }
                }
                if (series.Count == 0) continue;
                collection.Add(new List<long>(series));
                series.Clear();
            }
            return collection;
        }

        private static void writeOutput(string fileName, List<List<long>> collection)
        {
            var file = new System.IO.StreamWriter(fileName);
            file.WriteLine(collection.Count);
            foreach (var series in collection)
            {
                file.WriteLine(series.Count + " " + string.Join(" ", series.ToArray()));
            }
            file.Close();
        }

        static void Main(string[] args)
        {
            writeOutput("medalidval.txt", findSeries(readInput("medalidsis.txt")));
        }
    }
}

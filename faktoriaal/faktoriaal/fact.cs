using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace faktoriaal
{
    class fact
    {

        private static int[] readInput(string fileName)
        {
            var file = new System.IO.StreamReader(fileName);
            var text = file.ReadLine().Split(' ');
            file.Close();

            var numbers = new int[2];
            for (var i = 0; i < text.Length; i++)
            {
                numbers[i] = int.Parse(text[i]);
            }
            
            return numbers;
        }

        private static int[] fillInputGap(int[] numbers)
        {
            var delta = numbers[1] - numbers[0];
            var filledNumbers = new int[delta + 1];
            filledNumbers[0] = numbers[0];
            for (var i = 1; i <= delta; i++)
            {
                filledNumbers[i] = filledNumbers[0] + i;
            }
            return filledNumbers;
        }

        static void Main(string[] args)
        {
            var numbers = fillInputGap(readInput("factsis.txt"));
        }
    }
}

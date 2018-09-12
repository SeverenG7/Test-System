using System;
using System.Collections.Generic;
using System.Linq;

namespace TestSystem.Logic.Infrastructure
{
    public static class StringExtension
    {
        public static List<int> StringStirrer(this String sequence)
        {
            char delimetr = ',';
            List<int> numbers = new List<int>();
            string[] sequenceArray = sequence.Split(delimetr).ToArray();

            for (int i = 0; i < sequenceArray.Length; i++)
            {
                if (!String.IsNullOrWhiteSpace(sequenceArray[i]))
                {
                    numbers.Add(Int32.Parse(sequenceArray[i]));
                }
            }
            return numbers;
        }

        public static string StringStirrer(this String sequence , int remove)
        {
            string newSequence = "";
            List<int> numbers = sequence.StringStirrer();
            numbers.Remove(remove);

            foreach (int number in numbers)
            {
                newSequence += number.ToString() + ",";
            }

            return newSequence;
        }
    }
} 

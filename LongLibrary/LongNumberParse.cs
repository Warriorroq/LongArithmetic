using System.Collections.Generic;
using System.Text;

namespace LongLibrary
{
    internal static class LongNumberParse
    {
        public static LongNumberSign GetNumberSign(string digits)
        {
            var numberSign = LongNumberSign.plus;
            if (digits.StartsWith('-'))
                numberSign = LongNumberSign.minus;
            return numberSign;
        }
        public static List<long> ConvertStringNumToListDigits(string bigNum, int basisLength)
        {
            List<long> digits = new();
            int index = bigNum.Length - 1;
            StringBuilder builder = new StringBuilder();
            while (index >= 0)
            {
                if (char.IsDigit(bigNum[index]))
                    builder.Append(bigNum[index]);
                if (builder.Length >= basisLength)
                    AddStringBuilderToDigitsList(builder, digits);
                index--;
            }
            if (builder.Length != 0)
                AddStringBuilderToDigitsList(builder, digits);
            RemoveZerosFromEndOfList(digits);
            return digits;
        }
        public static void RemoveZerosFromEndOfList(List<long> list)
        {
            if (list.Count == 1)
                return;
            var number = 0;
            var count = list.Count - 1;
            for (int i = count; i > 0; i--)
            {
                if (list[i] == 0)
                    number++;
                else
                    break;
            }
            if (number != 0)
                list.RemoveRange(list.Count - number, number);
        }
        private static void AddStringBuilderToDigitsList(StringBuilder builder, List<long> nums)
        {
            builder.Reverse();
            var digit = long.Parse(builder.ToString());
            builder.Clear();
            nums.Add(digit);
        }
    }
}

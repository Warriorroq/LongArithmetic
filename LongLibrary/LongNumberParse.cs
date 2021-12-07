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
                    digits.Add(ParseBuilderToNum(builder));
                index--;
            }
            if (builder.Length != 0)
                digits.Add(ParseBuilderToNum(builder));
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
        private static long ParseBuilderToNum(StringBuilder builder)
        {
            builder.Reverse();
            var digit = long.Parse(builder.ToString());
            builder.Clear();
            return digit;
        }
    }
}

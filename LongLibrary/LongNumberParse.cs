using System.Collections.Generic;
using System.Text;

namespace LongLibrary
{
    public static class LongNumberParse
    {
        public static LongNumberSign GetNumberSign(string digits)
        {
            var numberSign = LongNumberSign.plus;
            if (digits.StartsWith('-'))
                numberSign = LongNumberSign.minus;
            return numberSign;
        }
        public static List<int> ConvertStringNumToListDigits(string bigNum, int basisLength)
        {
            List<int> digits = new();
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
        private static void AddStringBuilderToDigitsList(StringBuilder builder, List<int> nums)
        {
            builder.Reverse();
            var digit = ConvertStringBuilderNumToDigits(builder);
            builder.Clear();
            nums.Add(digit);
        }
        private static int ConvertStringBuilderNumToDigits(StringBuilder builder)
           => int.Parse(builder.ToString());
        private static void RemoveZerosFromEndOfList(List<int> list)
        {
            for(int i = list.Count - 1; i >= 0; i--)
            {
                if (list[i] == 0)
                    list.RemoveAt(i);
                else
                    return;
            }
        }
    }
}

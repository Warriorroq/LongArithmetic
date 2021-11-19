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
            return digits;
        }
        private static void AddStringBuilderToDigitsList(StringBuilder builder, List<int> nums)
        {
            builder.Reverse();
            nums.Add(ConvertStringBuilderNumToDigits(builder));
            builder.Clear();
        }
        private static int ConvertStringBuilderNumToDigits(StringBuilder builder)
           => int.Parse(builder.ToString());
    }
}

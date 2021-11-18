using System;
using System.Collections.Generic;
using System.Text;

namespace LongLibrary
{
    public class LongNumber
    {
        public int Length => digits.Count;
        public LongNumber(string digits)
        {
            this.digits = new();
            SetNumberSign(digits);
            ConvertStringNumToDigits(digits);
            if (this.digits.Count is 0)
            {
                this.digits.Add(0);
                numberSign = LongNumberSign.plus;
            }
        }
        protected List<int> digits;
        protected LongNumberSign numberSign;
        protected const int basis = 1_000_000;
        protected const int basisLength = 6;
        public static LongNumber operator +(LongNumber firstNum, LongNumber secondNum)
        {
            if (secondNum.Length > firstNum.Length)
                (firstNum, secondNum) = (secondNum, firstNum);
            if (firstNum.numberSign == secondNum.numberSign)
            {
                for(int i = 0; i < secondNum.Length; i++)
                    firstNum.AddValueToDigit(secondNum.digits[i], i);
            }
            return firstNum;
        }
        private void AddValueToDigit(int value, int index)
        {
            var sum = value + digits[index];
            if(sum >= basis)
            {
                if (digits.Count - 1 == index)
                {
                    digits.Add(1);
                    digits[index] = sum - basis;
                    return;
                }
                digits[index] = sum - basis;
                AddValueToDigit(1, index + 1);
                return;
            }
            digits[index] += value;
        }
        private void SetNumberSign(string digits)
        {
            numberSign = LongNumberSign.plus;
            if (digits.StartsWith('-'))
            {
                numberSign = LongNumberSign.minus;
                if (digits.Length == 1)
                    throw new Exception($"ERROR: incorrect enter {nameof(digits)} it contains only - ");
            }
        }
        private void ConvertStringNumToDigits(string bigNum)
        {
            int index = bigNum.Length - 1;
            StringBuilder builder = new StringBuilder();
            while(index >= 0)
            {
                if(char.IsDigit(bigNum[index]))
                    builder.Append(bigNum[index]);
                if (builder.Length >= basisLength)
                    AddStringBuilderNumToDigits(builder);
                index--;
            }
            if(builder.Length != 0)
                AddStringBuilderNumToDigits(builder);
        }
        private void AddStringBuilderNumToDigits(StringBuilder builder)
        {
            builder.Reverse();
            ConvertStringBuilderNumToDigits(builder);
            builder.Clear();
        }
        private void ConvertStringBuilderNumToDigits(StringBuilder builder)
            =>digits.Add(int.Parse(builder.ToString()));
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            if (numberSign is LongNumberSign.minus)
                builder.Append('-');
            var i = digits.Count - 1;
            builder.Append(digits[i]);
            i--;
            while (i >= 0)
            {
                if (digits[i] < 10)
                    builder.Append("00000");
                else if (digits[i] < 100)
                    builder.Append("0000");
                else if (digits[i] < 1000)
                    builder.Append("000");
                else if (digits[i] < 10000)
                    builder.Append("00");
                else if (digits[i] < 100000)
                    builder.Append("0");
                builder.Append(digits[i]);
                i--;
            }
            return builder.ToString();
        }
    }
    public enum LongNumberSign
    {
        plus = 0,
        minus = 1,
    }
}

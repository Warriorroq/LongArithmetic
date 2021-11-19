using System;
using System.Collections.Generic;
using System.Text;

namespace LongLibrary
{
    public class LongNumber : ICloneable
    {
        public LongNumber(string digits)
        {
            numberSign = LongNumberParse.GetNumberSign(digits);
            this.digits = LongNumberParse.ConvertStringNumToListDigits(digits, basisLength);
            if (this.digits.Count is 0)
            {
                this.digits.Add(0);
                numberSign = LongNumberSign.plus;
            }
        }
        private LongNumber(List<int> digits, LongNumberSign numberSign)
        {
            this.digits = new List<int>();
            this.digits.AddRange(digits);
            this.numberSign = numberSign;
        }
        public int Length => digits.Count;
        protected List<int> digits;
        protected LongNumberSign numberSign;
        protected const int basis = 1_000_000;
        protected const int basisLength = 6;
        public static LongNumber operator +(LongNumber firstNum, LongNumber secondNum)
        {
            if (firstNum.numberSign != secondNum.numberSign)
                return null;
            if (secondNum.Length > firstNum.Length)
                (firstNum, secondNum) = (secondNum, firstNum);
            for (int i = 0; i < secondNum.Length; i++)
                firstNum.AddValueToDigits(secondNum.digits[i], i);
            return firstNum;
        }
        public static LongNumber operator -(LongNumber firstNum, LongNumber secondNum)
        {
            if (firstNum.numberSign != secondNum.numberSign)
            {
                secondNum.numberSign = (LongNumberSign)((int)secondNum.numberSign * -1);
                return firstNum + secondNum;
            }
            return null;
        }
        private void AddValueToDigits(int value, int index)
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
                AddValueToDigits(1, index + 1);
                return;
            }
            digits[index] += value;
        }
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
                builder.Append(GetZerosInNum(digits[i]));
                builder.Append(digits[i]);
                i--;
            }
            return builder.ToString();
        }
        private string GetZerosInNum(int number)
        {
            StringBuilder builder = new();
            for(int i = 0;i < basisLength - number.Length();i++)
                builder.Append("0");
            return builder.ToString();
        }
        public object Clone()
            => new LongNumber(digits,numberSign);
    }
    public enum LongNumberSign
    {
        plus = 1,
        minus = -1,
    }
}

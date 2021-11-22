using System;
using System.Collections.Generic;
using System.Text;

namespace LongLibrary
{
    public class LongNumber : ICloneable
    {
        public int Length => digits.Count;
        protected List<long> digits;
        protected LongNumberSign numberSign;
        protected const long basis = 1_000_000_000;
        internal const int basisLength = 9;
        private LongNumber(){}
        internal LongNumber(List<long> digits, LongNumberSign numberSign)
        {
            this.digits = digits;
            this.numberSign = numberSign;
        }
        #region Operators
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
        public static bool operator ==(LongNumber firstNum, LongNumber secondNum)
        {
            if (firstNum.numberSign != secondNum.numberSign)
                return false;
            if (firstNum.Length != secondNum.Length)
                return false;
            for (int i = 0; i < firstNum.Length; i++)
                if (firstNum.digits[i] != secondNum.digits[i])
                    return false;
            return true;
        }
        public static bool operator !=(LongNumber firstNum, LongNumber secondNum)
            => !(firstNum == secondNum);
        private void AddValueToDigits(long value, int index)
        {
            var sum = value + digits[index];
            if (sum >= basis)
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
        #endregion Operators
        #region ToString
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
                builder.Append(GetZerosInDigit(digits[i]));
                builder.Append(digits[i]);
                i--;
            }
            return builder.ToString();
        }
        private string GetZerosInDigit(long number)
        {
            StringBuilder builder = new();
            for(int i = 0;i < basisLength - number.Length();i++)
                builder.Append("0");
            return builder.ToString();
        }
        #endregion ToString
        #region Clone
        public object Clone()
        {
            var clone = new LongNumber();
            clone.digits = new List<long>(digits);
            clone.numberSign = numberSign;
            return clone;
        }
        #endregion Clone
    }
    public enum LongNumberSign
    {
        plus = 1,
        minus = -1,
    }
}

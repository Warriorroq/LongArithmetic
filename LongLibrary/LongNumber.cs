using System;
using System.Collections.Generic;
using System.Text;

//public long this[int index] => digits[index];
//public int Length => (digits.Count - 1) * basisLength + digits[digits.Count - 1].Length();
namespace LongLibrary
{
    public class LongNumber : ICloneable
    {
        public int Count => _digits.Count;
        internal const int basisLength = 9;
        protected List<long> _digits;
        protected LongNumberSign _numberSign;
        protected const long _basis = 1_000_000_000;
        public LongNumber(LongNumber longNumber)
        {
            _digits = new List<long>(longNumber._digits);
            _numberSign = longNumber._numberSign;
        }
        internal LongNumber(List<long> digits, LongNumberSign numberSign)
        {
            _digits = digits;
            _numberSign = numberSign;
        }
        #region Operators
        public static LongNumber operator +(LongNumber firstNum, LongNumber secondNum)
        {
            List<long> list = new List<long>();
            SumUpAllDigits(firstNum, secondNum, list);
            LongNumberSign sign = list[list.Count - 1] < 0 ? LongNumberSign.minus : LongNumberSign.plus;
            CorrectInput(list);
            LongNumberParse.RemoveZerosFromEndOfList(list);
            return new LongNumber(list, sign);
        }
        private static void CorrectInput(List<long> digits)
        {
            for(int i =0;i < digits.Count - 1;i++)
            {
                if(digits[i] >= _basis)
                {
                    digits[i + 1]++;
                    digits[i] -= _basis;
                }
                else if (digits[i] <= -_basis)
                {
                    digits[i + 1]--;
                    digits[i] += _basis;// n - x fix
                }
            }
            if(digits[digits.Count - 1] >= _basis)
            {
                digits.Add(1);
                digits[digits.Count - 2] -= _basis;
            }
            else if(digits[digits.Count - 1] <= -_basis)
            {
                digits.Add(1);
                digits[digits.Count - 2] += _basis;
            }

        }
        private static void SumUpAllDigits(LongNumber firstNum, LongNumber secondNum, List<long> result)
        {
            var length = firstNum.Count > secondNum.Count ? firstNum.Count : secondNum.Count;
            for (var i = 0; i < length; i++)
            {
                long num1 = 0;
                long num2 = 0;
                if (i < firstNum.Count)
                    num1 = firstNum._digits[i] * (int)firstNum._numberSign;
                else
                    num1 = 0;

                if (i < secondNum.Count)
                    num2 = secondNum._digits[i] * (int)secondNum._numberSign;
                else
                    num2 = 0;
                result.Add(num1 + num2);
            }
        }
        public static LongNumber operator -(LongNumber firstNum, LongNumber secondNum)
        {
            if (firstNum._numberSign != secondNum._numberSign)
            {
                secondNum._numberSign = (LongNumberSign)((int)secondNum._numberSign * -1);
                return firstNum + secondNum;
            }
            return null;
        }
        public static bool operator ==(LongNumber firstNum, LongNumber secondNum)
        {
            if (firstNum._numberSign != secondNum._numberSign)
                return false;
            if (firstNum.Count != secondNum.Count)
                return false;
            for (int i = 0; i < firstNum.Count; i++)
                if (firstNum._digits[i] != secondNum._digits[i])
                    return false;
            return true;
        }
        public static bool operator !=(LongNumber firstNum, LongNumber secondNum)
            => !(firstNum == secondNum);
        private void AddValueToDigits(long value, int index)
        {
            var sum = value + _digits[index];
            if (sum >= _basis)
            {
                if (_digits.Count - 1 == index)
                {
                    _digits.Add(1);
                    _digits[index] = sum - _basis;
                    return;
                }
                _digits[index] = sum - _basis;
                AddValueToDigits(1, index + 1);
                return;
            }
            _digits[index] += value;
        }
        #endregion Operators
        #region ToString
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            if (_numberSign is LongNumberSign.minus)
                builder.Append('-');
            var i = _digits.Count - 1;
            builder.Append(_digits[i]);
            i--;
            while (i >= 0)
            {
                builder.Append(GetZerosInDigit(_digits[i]));
                builder.Append(_digits[i]);
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
            var digits = new List<long>(_digits);
            var numberSign = _numberSign;
            return new LongNumber(digits, numberSign);
        }
        #endregion Clone
    }
    public enum LongNumberSign
    {
        plus = 1,
        minus = -1,
    }
}

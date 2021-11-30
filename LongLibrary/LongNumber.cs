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
        private LongNumber()
        {
            _digits = new List<long>();
            _numberSign = LongNumberSign.plus;
        }
        #region Operators
        public static LongNumber operator +(LongNumber firstNum, LongNumber secondNum)
        {
            List<long> list = new List<long>();
            OperateAllDigits(firstNum, secondNum, list, SumUp);
            LongNumberParse.RemoveZerosFromEndOfList(list);
            LongNumberSign sign = list[list.Count - 1] < 0 ? LongNumberSign.minus : LongNumberSign.plus;
            CorrectData(list, CorrectDigits);
            return new LongNumber(list, sign);
        }
        public static LongNumber operator -(LongNumber firstNum, LongNumber secondNum)
        {
            List<long> list = new List<long>();
            OperateAllDigits(firstNum, secondNum, list, Reduce);
            LongNumberParse.RemoveZerosFromEndOfList(list);
            LongNumberSign sign = list[list.Count - 1] < 0 ? LongNumberSign.minus : LongNumberSign.plus;
            CorrectData(list, CorrectDigits);
            return new LongNumber(list, sign);
        }
        // TODO: this
        public static LongNumber operator *(LongNumber firstNum, LongNumber secondNum)
        {
            LongNumberSign sign = (LongNumberSign)((int)firstNum._numberSign * (int)secondNum._numberSign);

            if (firstNum.Count < secondNum.Count)
                (secondNum, firstNum) = (firstNum, secondNum);

            var number = new LongNumber();
            var list = new List<long>();
            for (var i = 0; i < secondNum.Count; i++)
            {
                list.Clear();
                for (var j = 0; j < firstNum.Count; j++)
                    list.Add(firstNum._digits[j] * secondNum._digits[i]);
                number += new LongNumber(list, LongNumberSign.plus);
            }
            number._numberSign = sign;
            return number;
        }
        public static bool operator >=(LongNumber firstNum, LongNumber secondNum)
        {
            if (firstNum._numberSign != secondNum._numberSign)
                return firstNum._numberSign == LongNumberSign.minus ? false : true;
            var list = new List<long>();
            OperateAllDigits(firstNum, secondNum, list, Reduce);
            LongNumberParse.RemoveZerosFromEndOfList(list);
            LongNumberSign sign = list[list.Count - 1] < 0 ? LongNumberSign.minus : LongNumberSign.plus;
            return sign == firstNum._numberSign;
        }
        public static bool operator <=(LongNumber firstNum, LongNumber secondNum)
            =>secondNum >= firstNum;
        public static bool operator <(LongNumber firstNum, LongNumber secondNum)
            => firstNum <= secondNum && secondNum != firstNum;
        public static bool operator >(LongNumber firstNum, LongNumber secondNum)
            => firstNum >= secondNum && secondNum != firstNum;
        private static void CorrectData(List<long> list, Action<List<long>> CorrectDigits)
        {
            CorrectDigits(list);
            LongNumberParse.RemoveZerosFromEndOfList(list);
            if (list.Count is 0)
                list.Add(0);
        }
        private static void CorrectDigits(List<long> digits)
        {
            for(int i =0;i < digits.Count - 1;i++)
            {
                var additionDigits = digits[i] / _basis;
                if (digits[i] >= _basis)
                {
                    digits[i + 1] += additionDigits;
                    digits[i] -= _basis * additionDigits;
                }
                else if (digits[i] < 0)
                {
                    digits[i + 1] -= additionDigits;
                    digits[i] += _basis * additionDigits;
                }
                digits[i] = Math.Abs(digits[i]);
            }
            CorrectLastDigit(digits);
        }
        private static void CorrectLastDigit(List<long> digits)
        {
            var last = digits[digits.Count - 1];
            var lastAdditionDigits = Math.Abs(digits[digits.Count - 1] / _basis);
            if (last >= _basis)
            {
                last -= _basis * lastAdditionDigits;
                digits[digits.Count - 1] = last;
                digits.Add(lastAdditionDigits);
            }
            else if (last <= -_basis)
            {
                last += _basis * lastAdditionDigits;
                digits[digits.Count - 1] = Math.Abs(last);
                digits.Add(lastAdditionDigits);
            }
            else if (last < 0)
                digits[digits.Count - 1] = Math.Abs(last);
        }
        private static void OperateAllDigits(LongNumber firstNum, LongNumber secondNum, List<long> result, Func<long,long,long> func)
        {
            var length = firstNum.Count > secondNum.Count ? firstNum.Count : secondNum.Count;
            for (var i = 0; i < length; i++)
            {
                long num1;
                long num2;
                if (i < firstNum.Count)
                    num1 = firstNum._digits[i] * (int)firstNum._numberSign;
                else
                    num1 = 0;

                if (i < secondNum.Count)
                    num2 = secondNum._digits[i] * (int)secondNum._numberSign;
                else
                    num2 = 0;
                result.Add(func(num1, num2));
            }
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
        public static long SumUp(long a, long b)
            => a + b;
        public static long Reduce(long a, long b)
            => a - b;
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

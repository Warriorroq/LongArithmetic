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
        public LongNumber(LongNumberSign numberSign, List<long> digits)
        {
            _digits = new List<long>(digits);
            _numberSign = numberSign;
        }
        private LongNumber()
        {
            _digits = new List<long>();
            _digits.Add(0);
            _numberSign = LongNumberSign.plus;
        }
        private LongNumber(LongNumberSign numberSign = LongNumberSign.plus, params long[] nums)
        {
            _digits = new List<long>(nums);            
            _numberSign = LongNumberSign.plus;
        }
        #region Operators
        public static LongNumber operator +(LongNumber firstNum, LongNumber secondNum)
        {
            List<long> list = OperateAllDigits(firstNum, secondNum, firstNum.SumUp);
            LongNumberParse.RemoveZerosFromEndOfList(list);
            LongNumberSign sign = list[list.Count - 1] < 0 ? LongNumberSign.minus : LongNumberSign.plus;
            CorrectData(list, CorrectDigits);
            return new LongNumber(sign, list);
        }
        public static LongNumber operator -(LongNumber firstNum)
            =>new LongNumber((LongNumberSign)((int)firstNum._numberSign * -1), firstNum._digits);
        public static LongNumber operator -(LongNumber firstNum, LongNumber secondNum)
        {
            List<long> list = OperateAllDigits(firstNum, secondNum, firstNum.Reduce);
            LongNumberParse.RemoveZerosFromEndOfList(list);
            LongNumberSign sign = list[list.Count - 1] < 0 ? LongNumberSign.minus : LongNumberSign.plus;
            CorrectData(list, CorrectDigits);
            return new LongNumber(sign, list);
        }
        public static LongNumber operator *(LongNumber firstNum, LongNumber secondNum)
        {
            LongNumberSign sign = (LongNumberSign)((int)firstNum._numberSign * (int)secondNum._numberSign);

            if (firstNum.Count < secondNum.Count)
                (secondNum, firstNum) = (firstNum, secondNum);

            var number = new LongNumber();
            var zeros = new List<long>();
            var list = new List<long>();
            for (var i = 0; i < secondNum.Count; i++)
            {
                list.Clear();
                list.AddRange(zeros);
                for (var j = 0; j < firstNum.Count; j++)
                    list.Add(firstNum._digits[j] * secondNum._digits[i]);
                number.SumUp(list);
                number.CorrectPlusDigits();
                zeros.Add(0);
            }
            number._numberSign = sign;
            return number;
        }
        public static LongNumber operator *(LongNumber firstNum, long secondNum)
        {
            var list = new List<long>();
            for (var i = 0; i < firstNum.Count; i++)                
                list.Add(firstNum._digits[i] * secondNum);
            var number = new LongNumber(LongNumberSign.plus, list);
            number.CorrectPlusDigits();
            return number;
        }
        public static LongNumber operator /(LongNumber firstNum, LongNumber secondNum)
        {
            var signs = (firstNum._numberSign, secondNum._numberSign);
            (firstNum._numberSign, secondNum._numberSign) = (LongNumberSign.plus, LongNumberSign.plus);

            var answer = firstNum.Devide(secondNum);
            answer._numberSign = (LongNumberSign)((int)signs.Item1 * (int)signs.Item2);

            (firstNum._numberSign, secondNum._numberSign) = (signs.Item1, signs.Item2);
            return answer;
        }
        public static LongNumber operator %(LongNumber firstNum, LongNumber secondNum)
        {
            var signs = (firstNum._numberSign, secondNum._numberSign);
            (firstNum._numberSign, secondNum._numberSign) = (LongNumberSign.plus, LongNumberSign.plus);

            var answer = firstNum - secondNum * firstNum.Devide(secondNum);
            answer._numberSign = (LongNumberSign)((int)signs.Item1 * (int)signs.Item2);

            (firstNum._numberSign, secondNum._numberSign) = (signs.Item1, signs.Item2);
            return answer;
        }
        public static bool operator >=(LongNumber firstNum, LongNumber secondNum)
        {
            if (firstNum._numberSign != secondNum._numberSign)
                return firstNum._numberSign == LongNumberSign.minus ? false : true;
            var list = OperateAllDigits(firstNum, secondNum, firstNum.Reduce);
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
                if (digits[i] >= _basis)
                {
                    digits[i + 1]++;
                    digits[i] -= _basis;
                }
                else if (digits[i] < 0)
                {
                    digits[i + 1]--;
                    digits[i] += _basis;
                }
                digits[i] = Math.Abs(digits[i]);
            }
            CorrectLastDigit(digits);
        }
        private static void CorrectLastDigit(List<long> digits)
        {
            var last = digits[digits.Count - 1];
            if (last >= _basis)
            {
                last -= _basis;
                digits[digits.Count - 1] = last;
                digits.Add(1);
            }
            else if (last <= -_basis)
            {
                last += _basis;
                digits[digits.Count - 1] = Math.Abs(last);
                digits.Add(1);
            }
            else if (last < 0)
                digits[digits.Count - 1] = Math.Abs(last);
        }
        private static List<long> OperateAllDigits(LongNumber firstNum, LongNumber secondNum, Func<long,long,long> func)
        {
            List<long> result = new();
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
            return result;
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
        public void Push(List<long> list)
        {
            var temp = _digits;
            _digits = list;
            _digits.AddRange(temp);
        }
        public void Push(long num)
            =>_digits.Insert(0, num);
        private LongNumber Devide(LongNumber devideNumber)
        {
            if (this < devideNumber)
                return new LongNumber(this);

            LongNumberSign sign = (LongNumberSign)((int)_numberSign * (int)devideNumber._numberSign);

            LongNumber number = new LongNumber(this);
            LongNumber answer = new LongNumber();            
            var bigDigit = number.DequequeDigitsAsLongNum(devideNumber.Count);
            while (true)
            {
                var a = GetDivideLong(bigDigit, devideNumber);
                answer.Push(a);
                bigDigit -= devideNumber * a;

                if (number.Count == 0)
                    break;
                else
                    bigDigit.Push(number.DequequeDigits(1));
            }
            LongNumberParse.RemoveZerosFromEndOfList(answer._digits);
            return answer;
        }
        private long GetDivideLong(LongNumber main, LongNumber divider)
        {
            if(main < divider)
                return 0;
            long left = 1;
            long middle = _basis/2;
            long right = _basis;
            var num = divider * middle;
            while (true)
            {
                if (left == middle || middle == right)
                    return middle;
                if(main > num)
                {
                    left = middle;
                    middle = (middle + right) / 2;
                }
                if (main < num)
                {
                    right = middle;
                    middle = (middle + left) / 2;
                }
                num = divider * middle;
            }
        }
        private LongNumber DequequeDigitsAsLongNum(int lastCount)
        {
            if (lastCount >= Count)
                return this;
            List<long> nums = new();

            for (var i = lastCount; i > 0; i--)
                nums.Add(_digits[Count - i]);
            
            _digits.RemoveRange(Count - lastCount, lastCount);
            return new LongNumber(_numberSign, nums);
        }
        private List<long> DequequeDigits(int lastCount)
        {
            if (lastCount > Count)
                return new List<long>();
            List<long> nums = new();

            for (var i = lastCount; i > 0; i--)
                nums.Add(_digits[Count - i]);

            _digits.RemoveRange(Count - lastCount, lastCount);
            return nums;
        }
        private long SumUp(long a, long b)
            => a + b;
        private long Reduce(long a, long b)
            => a - b;
        private void CorrectPlusDigits()
        {
            long additionDigits = 0;
            for (int i = 0; i < _digits.Count - 1; i++)
            {
                additionDigits = _digits[i] / _basis;
                if (_digits[i] >= _basis)
                {
                    _digits[i + 1]+=additionDigits;
                    _digits[i] -= _basis * additionDigits;
                }
            }
            var last = _digits[_digits.Count - 1];
            additionDigits = last / _basis;
            if (last >= _basis)
            {
                _digits[_digits.Count - 1] -= _basis * additionDigits;
                _digits.Add(0);
                _digits[_digits.Count - 1] = additionDigits;
            }
            LongNumberParse.RemoveZerosFromEndOfList(_digits);
        }
        private void SumUp(List<long> list)
        {
            List<long> newList = new();
            var length = Count > list.Count ? Count : list.Count;
            for (var i = 0; i < length; i++)
            {
                long num1;
                long num2;
                if (i < Count)
                    num1 = _digits[i];
                else
                    num1 = 0;

                if (i < list.Count)
                    num2 = list[i];
                else
                    num2 = 0;
                newList.Add(num1 + num2);
            }
            _digits = newList;
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
            => new LongNumber(_numberSign, new List<long>(_digits));
        #endregion Clone
    }
    public enum LongNumberSign
    {
        plus = 1,
        minus = -1,
    }
}

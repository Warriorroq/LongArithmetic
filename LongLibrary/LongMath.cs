namespace LongLibrary
{
    public static class LongMath
    {
        public static LongNumber GetLongNumber(string number)
        {
            var numberSign = LongNumberParse.GetNumberSign(number);
            var digits = LongNumberParse.ConvertStringNumToListDigits(number, LongNumber.basisLength);
            if (digits.Count is 0)
            {
                digits.Add(0);
                numberSign = LongNumberSign.plus;
            }
            return new LongNumber(numberSign, digits);
        }
    }
}

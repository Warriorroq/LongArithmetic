using System;

namespace LongLibrary
{
    public static class IntExtensions
    {
        public static int Length(this int number)
        {
            if (number == 0)
                return 1;
            return GetLength(number);
        }
        public static int Length(this long number)
        {
            if (number == 0)
                return 1;
            return GetLength(number);
        }
        private static int GetLength(long number)
        {
            var result = 0;
            while (number > 0)
            {
                number = number / 10;
                result++;
            }
            return result;
        }
    }
}

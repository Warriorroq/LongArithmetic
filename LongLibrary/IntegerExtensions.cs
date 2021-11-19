using System;

namespace LongLibrary
{
    public static class IntegerExtensions
    {
        public static int Length(this int number)
        {
            var result = 0;
            if (result == 1)
                return 1;
            while (number > 0)
            {
                number = number / 10;
                result++;
            }
            return result;
        }
}
}

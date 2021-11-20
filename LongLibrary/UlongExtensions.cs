namespace LongLibrary
{
    public static class UlongExtensions
    {
        public static int Length(this ulong number)
        {
            var result = 0;
            if (number == 0)
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

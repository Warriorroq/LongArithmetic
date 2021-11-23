using System.Collections.Generic;
using System.Text;

namespace LongLibrary
{
    public static class StringBuilderExtension
    {
        public static void Reverse(this StringBuilder builder)
        {
            int end = builder.Length - 1;
            int start = 0;
            while (end - start > 0)
            {
                (builder[end], builder[start]) = (builder[start], builder[end]);
                start++;
                end--;
            }
        }
    }
}

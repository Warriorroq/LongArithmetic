using System;
using LongLibrary;
namespace LongNumbers
{
    public class Program
    {
        private Program() { }
        private static void Main(string[] args)
            => new Program().Execute();
        private void Execute()
        {
            var breakNum = LongMath.GetLongNumber("10");

            while (true)
            {
                LongNumber longNumber1 = LongMath.GetLongNumber(Console.ReadLine());
                LongNumber longNumber2 = LongMath.GetLongNumber(Console.ReadLine());
                var a = longNumber2 + longNumber1;
                Console.WriteLine(a);
            }

        }
    }

}

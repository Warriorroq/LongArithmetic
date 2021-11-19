using System;
using System.Threading;
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
            var breakNum = new LongNumber("10");

            while(true)
            {
                LongNumber longNumber1 = new LongNumber(Console.ReadLine());
                LongNumber longNumber2 = new LongNumber(Console.ReadLine());
                Console.WriteLine(longNumber1 == longNumber2);
                Console.WriteLine(longNumber1 + longNumber2);
                if (longNumber1 == breakNum || longNumber2 == breakNum)
                    break;

            }

        }
    }

}

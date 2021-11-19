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
            var breakNum = new LongNumber("10");

            while(true)
            {
                LongNumber longNumber1 = new LongNumber(Console.ReadLine());
                LongNumber longNumber2 = new LongNumber(Console.ReadLine());
                var temp1 = longNumber1.Clone() as LongNumber;
                var temp2 = longNumber2.Clone() as LongNumber;
                Console.WriteLine($" - {longNumber1 - longNumber2}");
                Console.WriteLine($" + {temp1 + temp2}");
                if (longNumber1 == breakNum || longNumber2 == breakNum)
                    break;

            }

        }
    }

}

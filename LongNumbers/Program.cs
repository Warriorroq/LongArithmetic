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
                string strlnum1 = longNumber1.ToString();
                string strlnum2 = longNumber2.ToString();
                var temp1 = longNumber1.Clone() as LongNumber;
                var temp2 = longNumber2.Clone() as LongNumber;
                Console.WriteLine($" {strlnum1} == {strlnum2} is {longNumber1 == longNumber2}");
                Console.WriteLine($" {strlnum1} != {strlnum2} is {longNumber1 != longNumber2}");
                Console.WriteLine($" {strlnum1} - {strlnum2} is {longNumber1 - longNumber2}");
                Console.WriteLine($" {strlnum1} + {strlnum2} is {temp1 + temp2}");
                if (longNumber1 == breakNum || longNumber2 == breakNum)
                    break;

            }

        }
    }

}

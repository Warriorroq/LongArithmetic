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
            while (true)
            {
                LongNumber longNumber1 = LongMath.GetLongNumber(ConsoleAsk("num1:"));
                LongNumber longNumber2 = LongMath.GetLongNumber(ConsoleAsk("num2:"));
                Console.WriteLine($"{longNumber1} + {longNumber2} = {longNumber1 + longNumber2}");
                Console.WriteLine($"{longNumber1} - {longNumber2} = {longNumber1 - longNumber2}");
                Console.WriteLine($"{longNumber1} == {longNumber2} = {longNumber1 == longNumber2}");
                Console.WriteLine($"{longNumber1} != {longNumber2} = {longNumber1 != longNumber2}");
            }
        }
        private string ConsoleAsk(string question)
        {
            Console.WriteLine(question);
            return Console.ReadLine();
        }
    }

}

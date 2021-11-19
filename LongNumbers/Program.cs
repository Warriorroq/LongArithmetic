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
            LongNumber longNumber2 = new LongNumber(Console.ReadLine());
            LongNumber longNumber1 = new LongNumber(Console.ReadLine());
            longNumber1 += longNumber2;
            Console.WriteLine(longNumber1);
        }
    }

}

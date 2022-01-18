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
            var num1 = LongMath.GetLongNumber("217783628520943495");
            var num2 = LongMath.GetLongNumber("232342340000002355");
            var answer = LongMath.GetLongNumber("-14 558 711 479 058 860");
            var a = num1 - num2;
        }
        private string ConsoleAsk(string question)
        {
            Console.WriteLine(question);
            return Console.ReadLine();
        }
    }

}

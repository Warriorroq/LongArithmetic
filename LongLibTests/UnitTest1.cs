using NUnit.Framework;
using LongLibrary;
namespace LongLibTests
{
    public class Tests
    {
<<<<<<< Updated upstream
        private LongNumber num1;
        private LongNumber num2;
        private LongNumber num3;
        private LongNumber num4;
        private LongNumber num5;
        private LongNumber num6;
        private LongNumber num7;
        [SetUp]
        public void Setup()
        {
            num1 = LongMath.GetLongNumber("932758236583249327948621932562341");
            num2 = LongMath.GetLongNumber("65783442658212443786544326523467");
            num3 = LongMath.GetLongNumber("-23578465723590238409823590982738");
            num4 = LongMath.GetLongNumber("-99999999999999999999999999999999");
            num5 = LongMath.GetLongNumber("100000000000000000000000000000000");
            num6 = LongMath.GetLongNumber("9998");
            num7 = LongMath.GetLongNumber("932758236583249327948621932562341");
        }

        [Test]
        public void EqualTest()
            =>Assert.IsTrue(num1 == num7);
=======

        [Test]
        public void EqualTest()
        {
            var num1 = LongMath.GetLongNumber("932758236583249327948621932562341");
            var num2 = LongMath.GetLongNumber(" 93 275 82 a36sadasg58 agfa3249327948621932562341");
            Assert.IsTrue(num1 == num2);
        }

        [Test]
        public void EqualTes2()
        {
            var num1 = LongMath.GetLongNumber("932758236583249327948621932562341");
            var num2 = LongMath.GetLongNumber("261389512");
            Assert.IsTrue(num1 != num2);
        }

        [Test]
        public void ModTest()
        {
            var num1 = LongMath.GetLongNumber("932758236583249327948621932562341");
            var num2 = LongMath.GetLongNumber("23432908593287523758923755982375");
            var answer = LongMath.GetLongNumber("18 874 801 445 035 901 350 595 449 249 716");
            Assert.IsTrue(num1 % num2 == answer);
        }

        [Test]
        public void ModTest2()
        {
            var num1 = LongMath.GetLongNumber("28736542938765982369832658923235325");
            var num2 = LongMath.GetLongNumber("235");
            var answer = LongMath.GetLongNumber("40");
            Assert.IsTrue(num1 % num2 == answer);
        }
        [Test]
        public void ModTest3()
        {
            var num1 = LongMath.GetLongNumber("28736542938765982369832658923235325");
            var num2 = LongMath.GetLongNumber("-235");
            var answer = LongMath.GetLongNumber("-195");
            Assert.IsTrue(num1 % num2 == answer);
        }
        [Test]
        public void ModTest4()
        {
            var num1 = LongMath.GetLongNumber("28736542938765982369832658923235325");
            var num2 = LongMath.GetLongNumber("-232342340000002355");
            var answer = LongMath.GetLongNumber("-14 558 711 479 058 860");
            Assert.IsTrue(num1 % num2 == answer);
        }

        [Test]
        public void MinusTest()
        {
            var num1 = LongMath.GetLongNumber("28736542938765982369832658923235325");
            var num2 = LongMath.GetLongNumber("25235325325235235325325235325235325");
            var answer = LongMath.GetLongNumber("3 501 217 613 530 747 044 507 423 598 000 000");
            Assert.IsTrue(num1 - num2 == answer);
        }
        [Test]
        public void MinusTest2()
        {
            var num1 = LongMath.GetLongNumber("217783628520943495");
            var num2 = LongMath.GetLongNumber("232342340000002355");
            var answer = LongMath.GetLongNumber("-14 558 711 479 058 860");
            Assert.IsTrue(num1 - num2 == answer);
        }
        [Test]
        public void MinusTest3()
        {
            var num1 = LongMath.GetLongNumber("28736542938765982369832658923235325");
            var num2 = LongMath.GetLongNumber("-25235325325235235325325235325235325");
            var answer = LongMath.GetLongNumber("53 971 868 264 001 217 695 157 894 248 470 650");
            Assert.IsTrue(num1 - num2 == answer);
        }
>>>>>>> Stashed changes
    }
}
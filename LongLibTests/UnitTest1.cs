using NUnit.Framework;
using LongLibrary;
namespace LongLibTests
{
    public class Tests
    {
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
    }
}
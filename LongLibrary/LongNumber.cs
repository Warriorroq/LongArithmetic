using System;
namespace LongLibrary
{
    public struct LongNumber
    {
        public LongNumber(int value, int basis)
        {
            this.value = value;
            this.basis = basis;
        }
        public int value;
        public int basis;
    }
}

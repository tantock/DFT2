using Microsoft.VisualStudio.TestTools.UnitTesting;
using DFT2;
using System;
using System.Collections.Generic;
using System.Text;

namespace DFT2.Tests
{
    [TestClass()]
    public class FFT_UtilityTests
    {
        [TestMethod()]
        public void ShiftTest()
        {
            int[] freq = new int[] { 0, 1, 2, 3, 4, -3, -2, -1 };
            int[] freqShift = new int[] { -3, -2, -1, 0, 1, 2, 3, 4};

            var shifted = FFT_Utility<int>.Shift(freq, 5);
            
            for(int i = 0; i < freq.Length; i++)
            {
                Assert.AreEqual(freqShift[i], shifted[i]);
            }
        }
    }
}
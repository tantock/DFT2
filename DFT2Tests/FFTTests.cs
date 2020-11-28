using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Numerics;
using DFT2;
using System;
using System.Collections.Generic;
using System.Text;

namespace DFT2.Tests
{
    [TestClass()]
    public class FFTTests
    {
        Complex[] data = new Complex[4];
        [TestMethod()]
        public void ComputeTest_Impulse()
        {
            data[0] = new Complex(1, 0);
            data[1] = new Complex(0, 0);
            data[2] = new Complex(0, 0);
            data[3] = new Complex(0, 0);

            var output = FFT.Compute(data);
            for(int i = 0; i < data.Length; i++)
            {
                Assert.AreEqual(1.0/data.Length, output[i].Real);
                Assert.AreEqual(0, output[i].Imaginary);
            }
        }

        [TestMethod()]
        public void ComputeTest_Shifted_Impulse()
        {
            data[0] = new Complex(0, 0);
            data[1] = new Complex(1, 0);
            data[2] = new Complex(0, 0);
            data[3] = new Complex(0, 0);

            var output = FFT.Compute(data);
            for (int i = 0; i < data.Length; i++)
            {
                Assert.AreEqual(1.0 / data.Length, output[i].Magnitude);
            }
        }
    }
}

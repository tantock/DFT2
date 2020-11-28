using Microsoft.VisualStudio.TestTools.UnitTesting;
using DFT2;
using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace DFT2.Tests
{
    [TestClass()]
    public class IFFTTests
    {
        
        [TestMethod()]
        public void ComputeTest_Impulse()
        {
            Complex[] data = new Complex[4];
            data[0] = new Complex(0.25, 0);
            data[1] = new Complex(0.25, 0);
            data[2] = new Complex(0.25, 0);
            data[3] = new Complex(0.25, 0);

            var output = IFFT.Compute(data);

            Assert.AreEqual(1, output[0].Real);
            Assert.AreEqual(0, output[0].Imaginary);

            for (int i = 1; i < data.Length; i++)
            {
                Assert.AreEqual(0, output[i].Real);
                Assert.AreEqual(0, output[i].Imaginary);
            }
        }
        [TestMethod()]
        public void ComputeTest_Impulse_Nyq_Bounded()
        {
            Complex[] data = new Complex[3];
            data[0] = new Complex(0.25, 0);
            data[1] = new Complex(0.25, 0);
            data[2] = new Complex(0.25, 0);

            var output = IFFT.Compute(data, true, true);

            Assert.AreEqual(1, output[0].Real);
            Assert.AreEqual(0, output[0].Imaginary);

            for (int i = 1; i < output.Length; i++)
            {
                Assert.AreEqual(0, output[i].Real);
                Assert.AreEqual(0, output[i].Imaginary);
            }
            Assert.AreEqual(4, output.Length);

            //not scaled
            data[0] = new Complex(1, 0);
            data[1] = new Complex(1, 0);
            data[2] = new Complex(1, 0);

            output = IFFT.Compute(data, false, true);

            Assert.AreEqual(1, output[0].Real);
            Assert.AreEqual(0, output[0].Imaginary);

            for (int i = 1; i < output.Length; i++)
            {
                Assert.AreEqual(0, output[i].Real);
                Assert.AreEqual(0, output[i].Imaginary);
            }
            Assert.AreEqual(4, output.Length);
        }
    }
}
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DFT2;
using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;
using System.Linq;

namespace DFT2.Tests
{
    [TestClass()]
    public class SpectralDerivativeTests
    {
        [TestMethod()]
        public void ComputeTest()
        {
            double tollerance = Math.Pow(10, -5);

            var n = 128;
            var L = 30.0;
            var dx = L / n;

            Complex[] f = new Complex[n];
            double[] df = new double[n];
            double x;
            for(int i = 0; i < n; i++)
            {
                x = (-L / 2)  + (dx * i) ;
                f[i] = new Complex(Math.Cos(x) * Math.Exp(-Math.Pow(x, 2) / 25), 0);
                df[i] = -(Math.Sin(x) * Math.Exp(-Math.Pow(x, 2) / 25) + ((2 / 25.0) * x * f[i].Real)) ;
            }

            var dfFFT = SpectralDerivative.Compute(f, L);

            List<double> error = new List<double>();

            for(int i = 0; i < dfFFT.Length; i++)
            {
                error.Add(Math.Abs(df[i] - dfFFT[i].Real));
            }

            if (error.Average() > tollerance)
                Assert.Fail();
            
        }
    }
}
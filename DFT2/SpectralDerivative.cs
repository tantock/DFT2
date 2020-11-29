using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

namespace DFT2
{
    public static class SpectralDerivative
    {
        /// <summary>
        /// Computes the spectral derivative of a series of data points
        /// </summary>
        /// <param name="data">Time series</param>
        /// <param name="sampleDuration">Duration of the time series</param>
        /// <returns></returns>
        public static Complex[] Compute(Complex[] data, double sampleDuration)
        {
            //derivative calculation
            var fhat = FFT.Compute(data);
            Complex[] dfhat = new Complex[fhat.Length];
            double[] kappa = new double[fhat.Length];
            Complex imaj = new Complex(0, 1);
            for (int i = 0; i < kappa.Length; i++)
            {
                kappa[i] = (2 * Math.PI / sampleDuration) * ((-data.Length / 2.0) + i);
            }

            kappa = DFT2.Utility.Bins<double>.Shift(kappa, (kappa.Length / 2) + 1);
            for (int i = 0; i < dfhat.Length; i++)
            {
                dfhat[i] = imaj * kappa[i] * fhat[i];
            }

            return IFFT.Compute(dfhat);
        }
    }
}

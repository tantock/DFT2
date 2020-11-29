using System;
using System.Numerics;
namespace DFT2
{
    /// <summary>
    /// Based on the COMP 5703: ADVANCED ALGORITHMS, FALL 2016 by Amente Bekele
    /// http://people.scs.carleton.ca/~maheshwa/courses/5703COMP/16Fall/FFT_Report.pdf
    /// </summary>
    public static class FFT
    {
        /// <summary>
        /// Compute the FFT
        /// </summary>
        /// <param name="data">Samples</param>
        /// <param name="scale">Scaled by 1/N, N = sample size</param>
        /// <param name="nyquistBounded">Output from freq 0 inclusive to frequency N/2 inclusive. Scales values by 2.</param>
        /// <returns></returns>
        public static Complex[] Compute(Complex[] data, bool scale = true, bool nyquistBounded = false)
        {
            double log = Math.Log(data.Length) / Math.Log(2);
            if (Math.Ceiling(log) != Math.Floor(log))
                throw new ArgumentException("Data length not a power of 2");
            var computed = Cooley_Tukey(data);
            for(int i = 0; scale && i < computed.Length; i++)
            {
                if (nyquistBounded)
                    computed[i] /= data.Length * 2;
                else
                    computed[i] /= data.Length;
            }

            if (nyquistBounded)
            {
                var positiveComputed = new Complex[(data.Length / 2) + 1];
                Array.Copy(computed, positiveComputed, positiveComputed.Length);
                return positiveComputed;
            }
            return computed;
        }

        private static Complex[] Cooley_Tukey(Complex[] data)
        {
            int N = data.Length;
            int halfN = N / 2;
            if (N == 1)
                return data;
            else
            {
                double angle = 2 * Math.PI / N;
                Complex W_N = new Complex(Math.Cos(angle),-Math.Sin(angle));
                Complex W = 1;
                Complex[] A_even = new Complex[halfN];
                Complex[] A_odd = new Complex[halfN];

                for(int i = 0; i < halfN; i++)
                {
                    int doubleI = i * 2;
                    A_even[i] = data[doubleI];
                    A_odd[i] = data[doubleI + 1];
                }

                var Y_even = Cooley_Tukey(A_even);
                var Y_odd = Cooley_Tukey(A_odd);

                var Y = new Complex[N];

                for(int j = 0; j < halfN; j++)
                {
                    Y[j] = Y_even[j] + (W * Y_odd[j]);
                    Y[j + halfN] = Y_even[j] - W * Y_odd[j];
                    W = W * W_N;
                }
                return Y;
            }
        }
    }
}

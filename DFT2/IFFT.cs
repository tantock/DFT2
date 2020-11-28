using System;
using System.Numerics;
using System.Collections.Generic;
using System.Text;

namespace DFT2
{
    public static class IFFT
    {
        /// <summary>
        /// Compute the Inverse FFT
        /// </summary>
        /// <param name="data">Samples</param>
        /// <param name="scaled">FFT output was scaled by 1/N, N = sample size</param>
        /// <param name="nyquistBounded">Output from freq 0 inclusive to frequency N/2 inclusive. Scales values by 2.</param>
        /// <returns></returns>
        public static Complex[] Compute(Complex[] data, bool scaled = true, bool nyquistBounded = false)
        {

            Complex[] twosided;

            if (nyquistBounded)
            {
                
                twosided = new Complex[(data.Length-1) * 2];
                double scale = scaled ? 1 : 1.0 / twosided.Length;
                int halfSize = twosided.Length / 2;

                for(int i = 0; i < halfSize; i++)
                {
                    twosided[i] = data[i] * scale;
                }
                int reversed = halfSize;
                for(int i = halfSize; i < twosided.Length; i++)
                {
                    twosided[i] = data[reversed] * scale;
                    reversed--;
                }

            }
            else
            {
                twosided = data;
            }


            var computed = Cooley_Tukey(twosided);


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
                Complex W_N = new Complex(Math.Cos(angle), Math.Sin(angle));
                Complex W = 1;
                Complex[] A_even = new Complex[halfN];
                Complex[] A_odd = new Complex[halfN];

                for (int i = 0; i < halfN; i++)
                {
                    int doubleI = i * 2;
                    A_even[i] = data[doubleI];
                    A_odd[i] = data[doubleI + 1];
                }

                var Y_even = Cooley_Tukey(A_even);
                var Y_odd = Cooley_Tukey(A_odd);

                var Y = new Complex[N];

                for (int j = 0; j < halfN; j++)
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

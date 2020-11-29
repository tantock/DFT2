using System;
using System.Collections.Generic;
using System.Text;

namespace DFT2.Utility
{
    public static class Bins<T>
    {
        /// <summary>
        /// Shifts array elements such that negative frequencies preceed the zero-th frequency
        /// </summary>
        /// <param name="array"></param>
        /// <param name="negIndex">Index start of negative frequency</param>
        /// <returns></returns>
        public static T[] Shift(T[] array, int negIndex)
        {
            T[] shiftedArr = new T[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                int index = (i + negIndex) % array.Length;
                shiftedArr[i] = array[(i + negIndex) % array.Length];
            }

            return shiftedArr;
        }
    }

    public static class Window
    {
        /// <summary>
        /// Outputs the Hann window constants
        /// </summary>
        /// <param name="N">N-point DFT size</param>
        /// <returns></returns>
        public static double[] Hann(int N)
        {
            double[] window = new double[N];
            for (int i = 0; i < N; i++)
            {
                window[i] = 0.5 * (1 - Math.Cos(2 * Math.PI * i / N));
            }

            return window;
        }
    }
}

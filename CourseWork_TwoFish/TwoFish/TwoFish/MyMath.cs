using System;

namespace TwoFish
{
    class MyMath
    {
        public static int[,] MatrixMultiplication(int[,] a, int[,] b)
        {
            int[,] r = new int[a.GetLength(0), b.GetLength(1)];
            for (int i = 0; i < a.GetLength(0); i++)
            {
                for (int j = 0; j < b.GetLength(1); j++)
                {
                    for (int k = 0; k < b.GetLength(0); k++)
                    {
                        r[i, j] += a[i, k] * b[k, j];
                    }
                }
            }
            return r;
        }

        public static string RightCycleShift(string duoSSNumber)
        {
            string shiftedNumber = "";
            shiftedNumber = duoSSNumber;
            var numLength = shiftedNumber.Length;
            var tmp = shiftedNumber[shiftedNumber.Length - 1];

            shiftedNumber = shiftedNumber.Insert(0, Convert.ToString(tmp));
            shiftedNumber = shiftedNumber.Remove(numLength);
            return shiftedNumber;
        }

        public static string LeftCycleShift(string duoSSNumber)
        {
            string shiftedNumber = "";
            shiftedNumber = duoSSNumber;
            var numLength = shiftedNumber.Length;
            var tmp = shiftedNumber[0];

            shiftedNumber = shiftedNumber.Remove(0, 1);
            shiftedNumber += tmp;
            return shiftedNumber;
        }


    }
}



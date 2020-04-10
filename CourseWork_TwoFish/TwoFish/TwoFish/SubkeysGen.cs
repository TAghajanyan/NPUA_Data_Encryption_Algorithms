using System;
using System.Collections.Generic;

namespace TwoFish
{
    class SubkeysGen
    {
        public const int XBlockLength = 32;
        public const int byteLength = 8;
        public const int NibbleLength = 4;
        public static int[,] M1Matrix = new int[4, 4] { { 1, 239, 91, 91 }, { 91, 239, 239, 1 }, { 239, 91, 1, 239 }, { 239, 1, 239, 91 } };
        public static int[,] M2Matrix = new int[4, 8] { { 1, 164, 85, 135, 90, 88, 219, 158 }, { 164, 86, 130, 243, 30, 198, 104, 229 }, { 2, 161, 252, 193, 71, 174, 61, 25 }, { 164, 85, 135, 90, 88, 219, 158, 3 } };
        public static double p = Math.Pow(2, 24) + Math.Pow(2, 16) + Math.Pow(2, 8) + 1;
        public static string[,] tItoeForQ0 = new string[4, 16] { { "8", "1", "7", "D", "6", "F", "3", "2", "0", "B", "5", "9", "E", "C", "A", "4" }, { "E", "C", "B", "8", "1", "2", "3", "5", "F", "4", "A", "6", "7", "0", "9", "D" }, { "B", "A", "5", "E", "6", "D", "9", "0", "C", "8", "F", "3", "2", "4", "7", "1" }, { "D", "7", "F", "4", "1", "2", "6", "E", "9", "B", "3", "0", "8", "5", "C", "A" } };
        public static string[,] tItoeForQ1 = new string[4, 16] { { "2", "8", "B", "D", "F", "7", "6", "E", "3", "1", "9", "4", "0", "A", "C", "5" }, { "1", "E", "2", "B", "4", "C", "3", "7", "6", "D", "A", "5", "F", "9", "0", "8" }, { "4", "C", "7", "5", "1", "6", "9", "A", "0", "E", "D", "8", "2", "B", "3", "F" }, { "B", "9", "5", "1", "C", "3", "D", "E", "6", "4", "7", "F", "2", "0", "8", "A" } };
        public static int[,] qOperationMatrix = new int[5, 4] { { 1, 0, 0, 1 }, { 0, 0, 1, 1 }, { 1, 0, 1, 0 }, { 1, 1, 0, 0 }, { 0, 1, 0, 1 } };

        public static int[,] sBlocksMatrix => qOperationMatrix;
        public static List<string> SubKeysGeneration(string key)
        {
            if (key.Length < 128)
            {
                while (key.Length != 128)
                {
                    key = key.Insert(0, "0");
                }
            }
            else if (key.Length < 192)
            {
                while (key.Length != 192)
                {
                    key = key.Insert(0, "0");
                }
            }
            else if (key.Length < 256)
            {
                while (key.Length != 256)
                {
                    key = key.Insert(0, "0");
                }
            }

            var forCalculations = key.Length / 64;
            var bigMKeyBlocks = StringMethods.StringToList(key, XBlockLength);
            var lilMKeyBlocksBin = StringMethods.StringToList(key, byteLength);
            int[] arrayV = new int[forCalculations];
            List<string> arrayMe = new List<string>();
            List<string> arrayMo = new List<string>();
            int[,] lilMKeyBlocksMatrix = new int[8, 1];
            List<string> subkeys = new List<string>();

            for (int i = 0; i < forCalculations; i++)
            {
                for (int k = 0; k < 8; k++)
                {
                    lilMKeyBlocksMatrix[k, 0] = Convert.ToInt32(lilMKeyBlocksBin[i * 8 + k], 2);
                }

                var promezhutoch = MyMath.MatrixMultiplication(M2Matrix, lilMKeyBlocksMatrix);
                int vItoe = 0;
                for (int j = 0; j < 4; j++)
                {
                    vItoe += (int)(promezhutoch[j, 0]);
                }
                arrayV[forCalculations - i - 1] = vItoe;
                arrayMe.Add(bigMKeyBlocks[2 * i]);
                arrayMo.Add(bigMKeyBlocks[2 * i + 1]);
            }

            for (int i = 0; i < 40; i++)
            {
                var AItoe = HFunction(StringMethods.MyConvertToString((int)(2 * i * p), 2, 32), arrayMe, forCalculations);
                var BItoe = StringMethods.MyConvertToString(HFunction(StringMethods.MyConvertToString((int)((2 * i + 1) * p), 2, 32), arrayMo, forCalculations), 2, 32);
                for (int j = 0; j < 8; j++)
                {
                    BItoe = MyMath.LeftCycleShift(BItoe);
                }

                if ((i % 2) == 0)
                {
                    var subkeyI = StringMethods.MyConvertToString((int)(AItoe + Convert.ToInt32(BItoe, 2) % Math.Pow(2, 32)), 2, 32);
                    subkeys.Add(subkeyI);
                }
                else
                {
                    var subkeyI = StringMethods.MyConvertToString((int)(AItoe + Convert.ToInt32(BItoe, 2) * 2 % Math.Pow(2, 32)), 2, 32);
                    for (int j = 0; j < 9; j++)
                    {
                        subkeyI = MyMath.LeftCycleShift(subkeyI);
                    }
                    subkeys.Add(subkeyI);
                }
            }

            return subkeys;
        }

        public static int HFunction(string word32BitDuoSS, List<string> arrayForCalculations, int kNumber)
        {
            int[,] matrixStep5 = new int[1, 4];
            //int[,] resultArrayStep5 = new int[1, 4];

            while (word32BitDuoSS.Length != 32)
            {
                word32BitDuoSS = word32BitDuoSS.Insert(0, "0");
            }
            string stepResult = word32BitDuoSS;

            int stepNum = 2;
            if (kNumber >= 2)
            {
                if (kNumber > 2)
                {
                    stepNum -= 1;
                    if (kNumber > 3)
                    {
                        stepNum -= 1;

                    }
                }
            }
            for (int i = stepNum; i < 5; i++)
            {
                string block32BitAfterQ = "";
                var subWords8Bit = StringMethods.StringToList(stepResult, byteLength);
                int flag = 0;
                foreach (string subWord in subWords8Bit)
                {
                    int y = 0;
                    var a0 = Convert.ToInt32(subWord, 2) / 16;
                    var b0 = Convert.ToInt32(subWord, 2) % 16;
                    var a1 = a0 ^ b0;
                    var test = StringMethods.MyConvertToString(b0, 2, 8);
                    var b0Nibbles = StringMethods.StringToList(StringMethods.MyConvertToString(b0, 2, 8), NibbleLength);
                    foreach (string nibble in b0Nibbles)
                    {
                        MyMath.RightCycleShift(nibble);
                    }
                    var b1 = (a0 ^ Convert.ToInt32(StringMethods.ListToString(b0Nibbles), 2) ^ (8 * a0)) % 16;
                    if (qOperationMatrix[stepNum, flag] == 0)
                    {
                        var a2 = Convert.ToInt32(tItoeForQ0[0, a1], 16);
                        var b2 = Convert.ToInt32(tItoeForQ0[1, b1], 16);
                        var a3 = a2 ^ b2;
                        var b2Nibbles = StringMethods.StringToList(StringMethods.MyConvertToString(b2, 2, 8), NibbleLength);
                        foreach (string nibble in b2Nibbles)
                        {
                            MyMath.RightCycleShift(nibble);
                        }
                        var b3 = (a2 ^ Convert.ToInt32(StringMethods.ListToString(b2Nibbles), 2) ^ (8 * a2)) % 16;
                        var a4 = Convert.ToInt32(tItoeForQ0[2, a3], 16);
                        var b4 = Convert.ToInt32(tItoeForQ0[3, b3], 16);
                        y = 16 * a4 + b4;
                    }
                    else
                    {
                        var a2 = Convert.ToInt32(tItoeForQ1[0, a1], 16);
                        var b2 = Convert.ToInt32(tItoeForQ1[1, b1], 16);
                        var a3 = a2 ^ b2;
                        var b2Nibbles = StringMethods.StringToList(StringMethods.MyConvertToString(b2, 2, 8), NibbleLength);
                        foreach (string nibble in b2Nibbles)
                        {
                            MyMath.RightCycleShift(nibble);
                        }
                        var b3 = (a2 ^ Convert.ToInt32(StringMethods.ListToString(b2Nibbles), 2) ^ (8 * a2)) % 16;
                        var a4 = Convert.ToInt32(tItoeForQ1[2, a3], 16);
                        var b4 = Convert.ToInt32(tItoeForQ1[3, b3], 16);
                        y = 16 * a4 + b4;
                    }
                    block32BitAfterQ += StringMethods.MyConvertToString(y, 2, 8);
                    flag++;
                }

                if (i != 4)
                {
                    stepResult = StringMethods.MyConvertToString(Convert.ToInt32(block32BitAfterQ, 2) ^ Convert.ToInt32(arrayForCalculations[3 - i], 2), 2, 32);
                }
                else
                {
                    var arrayAfterQ = StringMethods.StringToList(block32BitAfterQ, 8);
                    for (int k = 0; k < 4; k++)
                    {
                        matrixStep5[0, k] = Convert.ToInt32(arrayAfterQ[k], 2);
                    }
                    //resultArrayStep5 = MyMath.MatrixMultiplication(M1Matrix, matrixStep5);
                }
            }
            int hResult = 0;
            for (int j = 0; j < 3; j++)
            {
                hResult += (int)(matrixStep5[0, j]);
            }

            return hResult;

        }

    }
}



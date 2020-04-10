using System;
using System.Collections.Generic;
using System.Linq;

namespace TwoFish
{
    class TwofishAlgo
    {
        public const int XBlockLength = 32;
        public static string testKey = "00000101110101010101011011010101010101010101111111100101110111101010101010110101110100111000011010100101001100000010111010101010101101101010101010101010111111110010111011110101010101011010111010011100001101010010100110";

        public static string Cyphering(string openMessageBlockDuoSS, string key)
        {
            int[] blocksAfterInputWhiting = new int[4];
            int[] blocksAfterOutputWhiting = new int[4];
            List<string> closedMessage = new List<string>();
            var subkeysArr = SubkeysGen.SubKeysGeneration(key);
            var XDataBlocks = StringMethods.StringToList(openMessageBlockDuoSS, XBlockLength);
            for (int i = 0; i < 4; i++)
            {
                blocksAfterInputWhiting[i] = Convert.ToInt32(XDataBlocks[i], 2) ^ Convert.ToInt32(subkeysArr[i], 2);
            }

            for (int round = 0; round < 16; round++)
            {
                blocksAfterInputWhiting = OneRound(blocksAfterInputWhiting, subkeysArr, round);
            }

            for (int i = 0; i < 4; i++)
            {
                blocksAfterOutputWhiting[i] = blocksAfterInputWhiting[i] ^ Convert.ToInt32(subkeysArr[i + 36], 2);
                closedMessage.Add(StringMethods.MyConvertToString(blocksAfterOutputWhiting[i], 2, 32));
            }
            var resultStringDuoSS = StringMethods.ListToString(closedMessage);
            return resultStringDuoSS;
        }

        public static string Decyphering(string closedMessageBlockDuoSS, string key)
        {
            int[] blocksBeforeInputWhiting = new int[4];
            Int32[] blocksBeforeOutputWhiting = new Int32[4];
            List<string> openMessage = new List<string>();
            var subkeysArr = SubkeysGen.SubKeysGeneration(key);
            var XDataBlocks = StringMethods.StringToList(closedMessageBlockDuoSS, XBlockLength);
            for (int i = 0; i < 4; i++)
            {
                blocksBeforeOutputWhiting[i] = Convert.ToInt32(XDataBlocks[i], 2) ^ Convert.ToInt32(subkeysArr[i + 36], 2);
            }
            for (int round = 15; round > -1; round--)
            {
                blocksBeforeOutputWhiting = OneRoundReverse(blocksBeforeOutputWhiting, subkeysArr, round);
            }
            for (int i = 0; i < 4; i++)
            {
                blocksBeforeInputWhiting[i] = blocksBeforeOutputWhiting[i] ^ Convert.ToInt32(subkeysArr[i], 2);
                openMessage.Add(StringMethods.MyConvertToString(blocksBeforeInputWhiting[i], 2, 32));
            }
            var resultStringDuoSS = StringMethods.ListToString(openMessage);
            return resultStringDuoSS;
        }

        public static string GFunction(string dataBlock32)
        {
            int[,] matrixAfterQ = new int[1, 4];
            //int[,] resultArrayStep5 = new int[1, 4];
            string block32BitAfterQ = "";
            var eightBitBlocks = StringMethods.StringToList(dataBlock32, 8);
            int blockNum = 0;
            string gResult = "";

            foreach (string subWord in eightBitBlocks)
            {
                int y = 0;
                var a0 = Convert.ToInt32(subWord, 2) / 16;
                var b0 = Convert.ToInt32(subWord, 2) % 16;
                var a1 = a0 ^ b0;
                var b0Nibbles = StringMethods.StringToList(StringMethods.MyConvertToString(b0, 2, 8), SubkeysGen.NibbleLength);
                foreach (string nibble in b0Nibbles)
                {
                    MyMath.RightCycleShift(nibble);
                }
                var b1 = (a0 ^ Convert.ToInt32(StringMethods.ListToString(b0Nibbles), 2) ^ (8 * a0)) % 16;
                if (SubkeysGen.qOperationMatrix[4, blockNum] == 0)
                {
                    var a2 = Convert.ToInt32(SubkeysGen.tItoeForQ0[0, a1], 16);
                    var b2 = Convert.ToInt32(SubkeysGen.tItoeForQ0[1, b1], 16);
                    var a3 = a2 ^ b2;
                    var b2Nibbles = StringMethods.StringToList(StringMethods.MyConvertToString(b2, 2, 8), SubkeysGen.NibbleLength);
                    foreach (string nibble in b2Nibbles)
                    {
                        MyMath.RightCycleShift(nibble);
                    }
                    var b3 = (a2 ^ Convert.ToInt32(StringMethods.ListToString(b2Nibbles), 2) ^ (8 * a2)) % 16;
                    var a4 = Convert.ToInt32(SubkeysGen.tItoeForQ0[2, a3], 16);
                    var b4 = Convert.ToInt32(SubkeysGen.tItoeForQ0[3, b3], 16);
                    y = 16 * a4 + b4;
                }
                else
                {
                    var a2 = Convert.ToInt32(SubkeysGen.tItoeForQ1[0, a1], 16);
                    var b2 = Convert.ToInt32(SubkeysGen.tItoeForQ1[1, b1], 16);
                    var a3 = a2 ^ b2;
                    var b2Nibbles = StringMethods.StringToList(StringMethods.MyConvertToString(b2, 2, 8), SubkeysGen.NibbleLength);
                    foreach (string nibble in b2Nibbles)
                    {
                        MyMath.RightCycleShift(nibble);
                    }
                    var b3 = (a2 ^ Convert.ToInt32(StringMethods.ListToString(b2Nibbles), 2) ^ (8 * a2)) % 16;
                    var a4 = Convert.ToInt32(SubkeysGen.tItoeForQ1[2, a3], 16);
                    var b4 = Convert.ToInt32(SubkeysGen.tItoeForQ1[3, b3], 16);
                    y = 16 * a4 + b4;
                }
                block32BitAfterQ += StringMethods.MyConvertToString(y, 2, 32);
                blockNum++;
            }
            var arrayAfterQ = StringMethods.StringToList(block32BitAfterQ, 8);
            for (int k = 0; k < 4; k++)
            {
                matrixAfterQ[0, k] = Convert.ToInt32(arrayAfterQ.ElementAt(k), 2);
            }

            for (int j = 0; j < 4; j++)
            {
                gResult += StringMethods.MyConvertToString(matrixAfterQ[0, j], 2, 8);
            }

            return gResult;
        }
        public static int[] OneRound(int[] blocksArray, List<string> subKeys, int roundNum)
        {
            int[] roundResult = new int[4];
            var stringB = StringMethods.MyConvertToString(blocksArray[1], 2, 32);
            for (int i = 0; i < 8; i++)
            {
                stringB = MyMath.LeftCycleShift(stringB);
            }
            int shiftedB = Convert.ToInt32(stringB, 2);
            var blockAafterG = GFunction(StringMethods.MyConvertToString(blocksArray[0], 2, 32));
            var blockBafterG = GFunction(StringMethods.MyConvertToString(shiftedB, 2, 32));
            var blockAandB = (Convert.ToInt32(blockAafterG, 2) + Convert.ToInt32(blockBafterG, 2)) % Math.Pow(2, 32);
            var APlusKey = blockAandB + Convert.ToInt32(subKeys.ElementAt(2 * roundNum + 8), 2);
            var BPlusKey = blockAandB + Convert.ToInt32(subKeys.ElementAt(2 * roundNum + 9), 2);
            blocksArray[2] = blocksArray[2] ^ (int)APlusKey;
            blocksArray[3] = Convert.ToInt32(MyMath.LeftCycleShift(StringMethods.MyConvertToString(blocksArray[3], 2, 32)), 2);
            blocksArray[3] = blocksArray[3] ^ (int)BPlusKey;
            blocksArray[2] = Convert.ToInt32(MyMath.RightCycleShift(StringMethods.MyConvertToString(blocksArray[2], 2, 32)), 2);
            roundResult[0] = blocksArray[2];
            roundResult[1] = blocksArray[3];
            roundResult[2] = blocksArray[0];
            roundResult[3] = blocksArray[1];

            return roundResult;
        }
        public static int[] OneRoundReverse(int[] blocksArray, List<string> subKeys, int roundNum)
        {
            int[] roundResult = new int[4];
            int shiftedD = blocksArray[3];

            for (int i = 0; i < 8; i++)
            {
                shiftedD = Convert.ToInt32(MyMath.LeftCycleShift(StringMethods.MyConvertToString(shiftedD, 2, 32)), 2);
            }
            var blockCafterG = GFunction(StringMethods.MyConvertToString(blocksArray[2], 2, 32));
            var blockDafterG = GFunction(StringMethods.MyConvertToString(shiftedD, 2, 32));
            var blockCandD = (Convert.ToInt32(blockCafterG, 2) + Convert.ToInt32(blockDafterG, 2)) % Math.Pow(2, 32);
            var CPlusKey = blockCandD + Convert.ToInt32(subKeys.ElementAt(2 * roundNum + 8), 2);
            var DPlusKey = blockCandD + Convert.ToInt32(subKeys.ElementAt(2 * roundNum + 9), 2);
            blocksArray[0] = Convert.ToInt32(MyMath.LeftCycleShift(StringMethods.MyConvertToString(blocksArray[0], 2, 32)), 2);
            blocksArray[1] = blocksArray[1] ^ (int)DPlusKey;
            blocksArray[1] = Convert.ToInt32(MyMath.RightCycleShift(StringMethods.MyConvertToString(blocksArray[1], 2, 32)), 2);
            blocksArray[0] = blocksArray[0] ^ (int)CPlusKey;
            roundResult[0] = blocksArray[2];
            roundResult[1] = blocksArray[3];
            roundResult[2] = blocksArray[0];
            roundResult[3] = blocksArray[1];

            return roundResult;
        }
    }
}



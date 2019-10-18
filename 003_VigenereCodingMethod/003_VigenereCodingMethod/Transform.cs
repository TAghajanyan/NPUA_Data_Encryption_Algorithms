using System;

namespace _003_VigenereCodingMethod
{
    class Transform
    {
        private string _key;

        public Transform(string key)
        {
            _key = key;
        }

        public string Encrypting(string sourceData)
        {
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string result = "";
            int index1, index2;

            for (int i = 0; i < sourceData.Length; i++)
            {
                if (i >= _key.Length)
                    _key += _key;

                index1 = alphabet.IndexOf(sourceData[i]);
                index2 = alphabet.IndexOf(_key[i]);
                result += index1 + index2 < 26 ? alphabet[index1 + index2] : alphabet[index1 + index2 - 26];
            }

            return result;
        }

        public string Decrypting(string decryptingData)
        {
            decryptingData = decryptingData.ToUpper();
            string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            string result = "";
            int index1, index2;

            for (int i = 0; i < decryptingData.Length; i++)
            {
                if (i >= _key.Length)
                    _key += _key;

                index1 = alphabet.IndexOf(decryptingData[i]);
                index2 = alphabet.IndexOf(_key[i]);
                result += index1 - index2 < 0 ? alphabet[26 - Math.Abs(index1 - index2)] : alphabet[index1 - index2];
            }

            return result;
        }

    }
}

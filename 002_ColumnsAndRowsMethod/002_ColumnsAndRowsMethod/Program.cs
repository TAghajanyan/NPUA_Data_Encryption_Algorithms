using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _002_ColumnsAndRowsMethod
{
    class Transform
    {
        private int _size;
        private char[,] _table;

        public Transform(int size)
        {
            _size = size;
        }

        public string GetEncryptedText(string sourceData)
        {
            string encryptedString = "";

            for (int i = 0; i < _size; i++)
            {
                for (int j = i; j < sourceData.Length; j += _size)
                {
                    encryptedString += sourceData[j];
                }
            }

            //------------------------------Second Variant----------------------------

            //_table = new char[sourceData.Length / _size + 1, _size];
            //int index = 0;
            //for (int i = 0; i < sourceData.Length / _size + 1; i++)
            //{
            //    for (int j = 0; j < _size && index < sourceData.Length; j++)
            //    {
            //        _table[i, j] = sourceData[index++];
            //        Console.Write(_table[i, j]);
            //    }
            //    Console.WriteLine();
            //}

            //for (int i = 0; i < _size; i++)
            //{
            //    for (int j = 0; j < _table.GetLongLength(0); j++)
            //    {
            //        encryptedString += _table[j, i];
            //    }
            //}

            return encryptedString;
        }

        public string GetDecryptedText(string decryptingText) 
        {
            _table = new char[decryptingText.Length / _size + 1, _size];
            string decryptedText = "";

            int countSpaces = (int)Math.Ceiling(decryptingText.Length / (double)_size);

            for (int i = 0; i < countSpaces * _size - decryptingText.Length; i++)
            {
                decryptingText += " ";
            }

            char[,] keyMatrix = new char[_size, countSpaces];

            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < countSpaces; j++)
                {
                    keyMatrix[i, j] = decryptingText[i * countSpaces + j];
                }
            }

            for (int j = 0; j < countSpaces; j++)
            {
                for (int i = 0; i < _size; i++)
                {
                    decryptedText += keyMatrix[i, j];
                }
            }
            return decryptedText;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Transform transform = new Transform(3);

            string encriptingText = transform.GetEncryptedText("12345678");
            Console.WriteLine(encriptingText); 

            Console.WriteLine(transform.GetDecryptedText(encriptingText));
        }
    }
}

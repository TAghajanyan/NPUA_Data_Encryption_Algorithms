namespace _001_PatternBasedMethod
{
    class Transform
    {
        private string _inputPattern;
        private string _outputPattern;

        public Transform()
        {
            _inputPattern = "AaBbCc1234";
            _outputPattern = "cCAaBb2143";
        }

        public string GetEncryptedString(string _sourceData)
        {
            string result = string.Empty;
            int i;

            foreach (char item in _sourceData)
            {
                for (i = 0; i < _inputPattern.Length; i++)
                {
                    if (item == _inputPattern[i])
                    {
                        result += _outputPattern[i];
                        break;
                    }
                }

                if (i == _inputPattern.Length)
                    result += item;
            }
            return result;
        }

        public string GetDecryptedString(string _encryptedData)
        {
            string result = string.Empty;
            int i;

            foreach (char item in _encryptedData)
            {
                for (i = 0; i < _outputPattern.Length; i++)
                {
                    if (item == _outputPattern[i])
                    {
                        result += _inputPattern[i];
                        break;
                    }
                }

                if (i == _outputPattern.Length)
                    result += item;
            }

            return result;
        }

    }
}
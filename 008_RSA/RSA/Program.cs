using System;
using System.Numerics;

namespace RSA
{
    class Program
    {
        private static int GCD(int a, int b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }

            return a == 0 ? b : a;
        }
        public static bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= boundary; i += 2)
                if (number % i == 0)
                    return false;

            return true;
        }
        public static int DetermineLCM(int a, int b)
        {
            int num1, num2;
            if (a > b)
            {
                num1 = a; num2 = b;
            }
            else
            {
                num1 = b; num2 = a;
            }

            for (int i = 1; i < num2; i++)
            {
                if ((num1 * i) % num2 == 0)
                {
                    return i * num1;
                }
            }
            return num1 * num2;
        }
        class RSA
        {
            private int p = 11;
            private int q = 13;
            private int lambda;
            private int d;
            public int n;
            public int e;
            public RSA()
            {
                n = p * q;
                lambda = ComputeLambda(p, q);
                e = CoPrimeFinder(n);
                d = PrivateKeyFinder(e, n);
            }
            private int ComputeLambda(int p, int q)
            {
                return DetermineLCM(p - 1, q - 1);
            }
            private int CoPrimeFinder(int n)
            {
                for (int i = 2; i < lambda; i++)
                {
                    if (GCD(i, lambda) == 1)
                        return i;
                }
                return 0;
            }
            public int ComputeChipherText(int m)
            {
                return (int)(Math.Pow(m, e) % n);
            }

            public int ComputePlainText(int c)
            {
                return (int)(BigInteger.Pow(c, d) % n);
            }

            public int PrivateKeyFinder(int e, int n)
            {
                for (int i = 3; i < n; i++)
                {
                    if (IsPrime(i))
                    {
                        if ((i * e - 1) % DetermineLCM(p - 1, q - 1) == 0)
                            return i;
                    }
                }
                return 0;
            }

        }

        static void Main(string[] args)
        {
            RSA a = new RSA();

            Console.WriteLine("Type an integer message < 100");
            int message = int.Parse(Console.ReadLine());
            int chiphertext = a.ComputeChipherText(message);
            Console.WriteLine("Encoded -> " + chiphertext);
            int plaintext = a.ComputePlainText(chiphertext);
            Console.WriteLine("Decoded -> " + plaintext);
        }
    }
}

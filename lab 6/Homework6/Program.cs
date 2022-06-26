using System;
using System.Numerics;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.ComponentModel.Design;


namespace Homework_6
{
    class Program
    {
        class Alice
        {
            public BigInteger K;
            private BigInteger a, g, p, A;

            public Alice(BigInteger a, BigInteger g, BigInteger p)
            {
                this.a = a;
                this.g = g;
                this.p = p;

                this.A = BigInteger.ModPow(this.g, this.a, this.p);
            }

            public BigInteger CalculateA(BigInteger[] param)
            {
                BigInteger g = param[0], a = param[1], p = param[2];
                BigInteger A = this.A = BigInteger.ModPow(this.g, this.a, this.p);
                return A;
            }

            public BigInteger[] ReturnData()
            {
                return new BigInteger[] {this.g, this.p, this.A};
            }

            public void CalculateFinalB(BigInteger B)
            {
                this.K = BigInteger.ModPow(B, this.a, this.p);
            }
        }

        class Bob
        {
            public BigInteger K;
            private BigInteger b;

            public Bob(BigInteger b)
            {
                this.b = b;
            }

            public BigInteger CalculateB(BigInteger[] param)
            {
                BigInteger g = param[0], p = param[1], A = param[2];
                BigInteger B = BigInteger.ModPow(g, this.b, p);
                return B;
            }

            public void CalculateFinalA(BigInteger[] param)
            {
                BigInteger g = param[0], p = param[1], A = param[2];
                this.K = BigInteger.ModPow(A, this.b, p);
            }

        }

        static void Main(string[] args)
        {
           /* BigInteger a = 12102342, g = 65537, p = 201285687, b = 86897665;*/

            Random rand = new();
            BigInteger a = new(rand.Next(10000000, 100000000));
            BigInteger b = new(rand.Next(10000000, 100000000));
            BigInteger p = new(rand.Next(100000, 3000000));

            BigInteger g;

            if (IsPrime((p-1)/2) == true)
            {
                g = FindPrimitive(p);
            }
            else
            {
                p = GetNearestPrime(p);
                g = FindPrimitive(p);
            }

            Alice test_Alice = new Alice(a, g, p);
            Console.WriteLine($"Алиса сгенерировала число a = {a} и g = {g} с p = {p}.");
            
            Bob test_Bob = new Bob(b);
            Console.WriteLine($"Боб сгенерировал число b = {b}.");
            
            BigInteger[] param = test_Alice.ReturnData();
            Console.WriteLine($"Боб получил от Алисы g = {param[0]}, p = {param[1]} и ключ A = {param[2]}.");

            BigInteger A = test_Alice.CalculateA(param);
            Console.WriteLine($"Таким образом, Алиса вычислила ключ A = {A}.");

            BigInteger B = test_Bob.CalculateB(param);
            Console.WriteLine($"А Боб вычислил ключ В = {B}.");

            test_Alice.CalculateFinalB(B);
            test_Bob.CalculateFinalA(param);
            Console.WriteLine(String.Format("В итоге, у Алисы оказался ключ {0}, а у Боба {1}. Их ключи {2}", test_Alice.K, test_Bob.K, test_Alice.K == test_Bob.K ? "совпали, ура!" : "разные."));
        }

        static bool IsPrime(BigInteger number)
        {
            if (number <= 1)
            {
                return false;
            }
            if (number <= 3)
            {
                return true;
            }

            if (number % 2 == 0 || number % 3 == 0)
            {
                return false;
            }

            for (int i = 5; i * i <= number; i += 6)
            {
                if (number % i == 0 || number % (i + 2) == 0)
                {
                    return false;
                }
            }
            return true;
        }

        static BigInteger GetNearestPrime(BigInteger number)
        {
            while (IsPrime((number - 1) / 2) == false)
            {
                number++;
            }
            return number;
        }

        static BigInteger Power(BigInteger x, BigInteger y, BigInteger p)
        {
            BigInteger result = 1;
            x %= p;

            while (y > 0)
            {
                if (y % 2 == 1)
                {
                    result = (result * x) % p;
                }
                y >>= 1;
                x = (x * x) % p;
            }
            return result;
        }

        static void FindPrimeFactors(HashSet<BigInteger> s, BigInteger n)
        {
            while (n % 2 == 0)
            {
                s.Add(2);
                n /= 2;
            }

            for (int i = 3; i <= Math.Pow(Math.E, BigInteger.Log(n) / 2); i += 2)
            {
                while (n % i == 0)
                {
                    s.Add(i);
                    n /= i;
                }
            }

            if (n > 2)
            {
                s.Add(n);
            }
        }

        static BigInteger FindPrimitive(BigInteger number)
        {
            HashSet<BigInteger> s = new HashSet<BigInteger>();

            BigInteger phi = number - 1;

            FindPrimeFactors(s, phi);

            // Check for every number from 2 to phi
            for (int r = 2; r <= phi; r++)
            {
                bool flag = false;
                foreach (int a in s)
                {
                    if (Power(r, phi / (a), number) == 1)
                    {
                        flag = true;
                        break;
                    }
                }

                if (flag == false)
                {
                    return r;
                }
            }
            return -1;
        }
    }
}

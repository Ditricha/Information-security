using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Numerics;

namespace Homework_2
{
    class RSA
    {
        static void Main(string[] args)
        {
            Console.Write("Алгоритм RSA\n\n");

            BigInteger n;
            BigInteger p = randomGenerator();
            BigInteger q = randomGenerator();

            if (IsPrime(p, 10) == true && IsPrime(q, 10) == true)
            {
                n = p * q;
            }
            else
            {
                p = GetNearestPrime(p);
                q = GetNearestPrime(q);
                n = p * q;
            }

            Console.Write($"Сгенерированое простое число p = {p}");
            Console.Write($"\n\nСгенерированое простое число q = {q}");
            Console.Write($"\n\nПроизведение p и q = {n}\n");

            BigInteger fn = EulerPhiFunction(p, q);
            Console.WriteLine($"\nФункция Эйлера: f(n) = {fn}");

            Console.Write($"\nВыберите открытую экспоненту из 17, 257, 65537: ");
            BigInteger e = BigInteger.Parse(Console.ReadLine());

            BigInteger temp_d = ExtendedEuclideanAlgorithm(e, fn);
            /*Console.Write($"\nD = {t_d}");*/
            BigInteger d = temp_d + fn;
            Console.Write($"\nСекретная экспонента d = {d}");

            Console.Write($"\n\nОткрытй ключ (e, n):\ne = {e}\nn = {n}");
            Console.Write($"\n\nЗакрытый ключ (d, n):\nd = {d}\nn = {n}");

            BigInteger encryptedMessage;
            BigInteger decryptedMessage;

            Console.Write("\n\nВведите собщение (число) для шифрования: ");
            BigInteger message = BigInteger.Parse(Console.ReadLine());

            encryptedMessage = Encrypt(message, e, n);
            Console.Write("\nЗашифрованный текст: {0}", encryptedMessage);

            decryptedMessage = Decrypt(encryptedMessage, d, n);
            Console.Write("\n\nРасшифрованный текст: {0}", decryptedMessage);

            Console.ReadLine();
        }

        //генерация рандомного большого числа
        static BigInteger randomGenerator()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] randomNumber = new byte[1024 / 8];
            rng.GetBytes(randomNumber);
            BigInteger result = new BigInteger(randomNumber);
            result = BigInteger.Abs(result);
            return result;
        }

        //проверка на простое число, тест Миллера-Рабина
        static bool IsPrime(BigInteger number, int k)
        {

            if (number == 2 || number == 3)
                return true;
            if (number < 2 || number % 2 == 0)
                return false;

            BigInteger t = number - 1;
            int s = 0;

            while (t % 2 == 0)
            {
                t /= 2;
                s += 1;
            }

            for (int i = 0; i < k; i++)
            {
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
                byte[] z0 = new byte[number.ToByteArray().LongLength];
                BigInteger z1;

                do
                {
                    rng.GetBytes(z0);
                    z1 = new BigInteger(z0);
                }
                while (z1 < 2 || z1 >= number - 2);

                BigInteger x = BigInteger.ModPow(z1, t, number);

                if (x == 1 || x == number - 1)
                    continue;

                for (int r = 1; r < s; r++)
                {
                    x = BigInteger.ModPow(x, 2, number);

                    if (x == 1)
                        return false;
                    if (x == number - 1)
                        break;
                }

                if (x != number - 1)
                    return false;
            }
            return true;
        }

        //получение простого числа, если результат проверки на простоту оказался неудачным
        static BigInteger GetNearestPrime(BigInteger number)
        {
            while (IsPrime(number, 10) == false)
            {
                number++;
            }
            return number;
        }

        static BigInteger EulerPhiFunction(BigInteger a, BigInteger b)
        {
            BigInteger phi = (a - 1) * (b - 1);
            return phi;
        }

        //расширенный алгоритм Евклида для вычисления секретной экспоненты
        static BigInteger ExtendedEuclideanAlgorithm(BigInteger a, BigInteger b)
        {
            BigInteger d, q, r, x, y, x1, x2, y1, y2;

            if (b == 0)
            {
                d = a;
                x = 1;
                y = 0;
                return x;
            }

            x2 = 1;
            x1 = 0;
            y2 = 0;
            y1 = 1;

            while (b > 0)
            {
                q = a / b;
                r = a - q * b;
                x = x2 - q * x1;
                y = y2 - q * y1;
                a = b;
                b = r;
                x2 = x1;
                x1 = x;
                y2 = y1;
                y1 = y;
            }
            d = a;
            x = x2;
            y = y2;

            return x;
        }

        //шифрование сообщения
        static BigInteger Encrypt(BigInteger msg, BigInteger e, BigInteger n)
        {
            BigInteger c = BigInteger.ModPow(msg, e, n);
            return c;
        }

        //дешифровка сообщения
        static BigInteger Decrypt(BigInteger c, BigInteger d, BigInteger n)
        {
            BigInteger m = BigInteger.ModPow(c, d, n);
            return m;
        }
    }
}
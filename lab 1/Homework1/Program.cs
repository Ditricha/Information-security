using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("1. Шифр Цезаря\n");
            Console.Write("2. Шифр Виженера\n");
            Console.Write("Введите номер шифра: ");
            string value = Console.ReadLine();

            switch (value)
            {
                case "1":
                    var cipher1 = new CaesarCipher();
                    int key = 3;
                    Console.Write("\nВведите строчной текст без пробелов на русском языке:\n");
                    string inputText1 = Console.ReadLine();

                    string encryptedText1 = cipher1.Encrypt(inputText1, key);
                    Console.WriteLine("{0}", encryptedText1);
                    Console.WriteLine("{0}", cipher1.Decrypt(encryptedText1, key));
                    Console.ReadLine();
                    break;
                case "2":
                    var cipher2 = new VigenereCipher();
                    Console.Write("\nВведите строчной текст без пробелов на русском языке:\n");
                    string inputText2 = Console.ReadLine();
                    Console.Write("Введите ключевое слово строчными буквами:\n");
                    string keyword = Console.ReadLine();

                    char[] arr = new char[100];
                    ArrayFromString(arr, keyword);

                    Console.Write("\n");
                    Console.WriteLine(inputText2);
                    for (int i = 0; i < inputText2.Length; i++)
                    {
                        Console.Write("{0}", arr[i]);
                    }
                    Console.Write("\n");
                    Separator(inputText2);
                    string encryptedText2 = cipher2.Encrypt(inputText2, keyword);
                    Console.WriteLine("\n{0}", encryptedText2);
                    Separator(inputText2);
                    Console.WriteLine("\n{0}", cipher2.Decrypt(encryptedText2, keyword));
                    Console.ReadLine();
                    break;
                default:
                    Console.WriteLine("Ошибка при вводе номера шифра.");
                    break;
            }
        }

        //создание массива символов из строки
        static void ArrayFromString(char[] array, string str)
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = str[i % str.Length];
            }
        }

        static void Separator(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                Console.Write("|");
            }
        }
    }

    //шифр Цезаря
    public class CaesarCipher
    {
        const string defaultAlphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";

        private string Encode(string text, int k)
        {
            string letters = defaultAlphabet;
            int n = letters.Length;
            string result = "";
            for (int i = 0; i < text.Length; i++)
            {
                char e = text[i];
                int index = letters.IndexOf(e);
                if (index < 0)
                {
                    result += e.ToString();
                }
                else
                {
                    int indexCode = (n + index + k) % n;
                    result += letters[indexCode];
                }
            }
            return result;
        }

        public string Encrypt(string originalText, int key)
        { 
            return Encode(originalText, key); 
        }

        public string Decrypt(string encryptedText, int key)
        {
            return Encode(encryptedText, -key);
        }
    }

    //шифр Виженера
    public class VigenereCipher
    {
        const string defaultAlphabet = "абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        readonly string letters;

        private string Encode(string text, string keyword, bool encrypting = true)
        {
            string result = "";
            int n = letters.Length;
            string xor = GetRepeatingKey(keyword, text.Length);

            for (int i = 0; i < text.Length; i++)
            {
                int indexLetters = letters.IndexOf(text[i]);
                int indexCode = letters.IndexOf(xor[i]);
                if (indexLetters < 0)
                {
                    result += text[i].ToString();
                }
                else
                {
                    result += letters[(n + indexLetters + ((encrypting ? 1 : -1) * indexCode)) % n].ToString();
                }
            }
            return result;
        }

        public string Encrypt(string originalText, string keyword)
        {
            return Encode(originalText, keyword);
        }

        public string Decrypt(string encryptedText, string keyword)
        {
            return Encode(encryptedText, keyword, false);
        }

        //генерация повторяющегося ключа
        private string GetRepeatingKey(string str, int n)
        {
            string k = str;
            while (k.Length < n)
            {
                k += k;
            }
            return k.Substring(0, n);
        }

        public VigenereCipher(string alphabet = null)
        {
            letters = string.IsNullOrEmpty(alphabet) ? defaultAlphabet : alphabet;
        }

    }

}

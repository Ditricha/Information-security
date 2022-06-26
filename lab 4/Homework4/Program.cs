using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;


namespace Homework_4
{
    class Program
    {
        private static void Main(string[] args)
        {
            Console.Write("Введите строку для шифрования: ");
            string input = Console.ReadLine();

            HuffmanTree huffmanTree = new();
            huffmanTree.Build(input);

            GetSortedArray(input);

            BitArray encoded = huffmanTree.Encode(input);
            Console.Write("\nЗашифрованная строка: ");
            foreach (bool bit in encoded)
            {
                Console.Write((bit ? 1 : 0) + "");
            }

            string decoded = huffmanTree.Decode(encoded);
            Console.Write($"\n\nРасшифрованная строка: {decoded}");

            Console.WriteLine();
            Console.ReadLine();
        }

        public static void GetSortedArray(string input)
        {
            string message1 = input;
            message1 = new string(message1.Distinct().ToArray());
            int[] counter = new int[input.Length];

            // подсчет повторяющихся символов
            for (int i = 0; i < input.Length; i++)
                counter[i] = 0;

            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < message1.Length; j++)
                {
                    if (input[i] == message1[j])
                    {
                        counter[j] += 1;
                    }
                }
            }

            // сортировка
            int tmp;
            char[] message2 = message1.ToCharArray(); ; // строка для сортировки
            char[] extra = new char[message1.Length];   // строка для мусора
            for (int i = 0; i < message1.Length - 1; i++)
            {
                for (int j = 0; j < message1.Length - 1; j++)
                {
                    if (counter[j] < counter[j + 1])
                    {
                        tmp = counter[j];
                        extra[j] = message2[j];
                        counter[j] = counter[j + 1];
                        message2[j] = message2[j + 1];
                        counter[j + 1] = tmp;
                        message2[j + 1] = extra[j];
                    }
                }
            }

            Console.WriteLine("\nОтсортированый по частоте повторений символов массив: ");
            for (int i = 0; i < input.Length; i++)
            {
                if (counter[i] != 0)
                    Console.WriteLine(message2[i] + ": " + counter[i]);
            }
        }
    }

    public class HuffmanTree
    {
        private readonly List<Node> _nodes = new();
        public Node Root { get; set; }
        public Dictionary<char, int> Frequency = new();

        public void Build(string source)
        {
            for (int i = 0; i < source.Length; i++)
            {
                if (!Frequency.ContainsKey(source[i]))
                {
                    Frequency.Add(source[i], 0);
                }
                Frequency[source[i]]++;
            }

            foreach (KeyValuePair<char, int> symbol in Frequency)
            {
                _nodes.Add(new Node() { Symbol = symbol.Key, Frequence = symbol.Value });
            }

            while (_nodes.Count > 1)
            {
                List<Node> orderedNodes = _nodes.OrderBy(node => node.Frequence).ToList();
                if (orderedNodes.Count >= 2)
                {
                    List<Node> taken = orderedNodes.Take(2).ToList();
                    Node parent = new()
                    {
                        Symbol = '*',
                        Frequence = taken[0].Frequence + taken[1].Frequence,
                        Left = taken[0],
                        Right = taken[1]
                    };
                    _nodes.Remove(taken[0]);
                    _nodes.Remove(taken[1]);
                    _nodes.Add(parent);
                }
                this.Root = _nodes.FirstOrDefault();
            }

        }

        public BitArray Encode(string source)
        {
            List<bool> encodedSource = new();

            Console.WriteLine("\nТаблица получившихся кодов символов: ");
            for (int i = 0; i < source.Length; i++)
            {
                List<bool> encodedSymbol = this.Root.Traverse(source[i], new List<bool>());
                encodedSource.AddRange(encodedSymbol);
                Console.WriteLine(source[i] + ": " + new string(encodedSymbol.Select(x => x ? '1' : '0').ToArray()));
            }

            BitArray bits = new(encodedSource.ToArray());
            return bits;
        }

        public string Decode(BitArray bits)
        {
            Node current = this.Root;
            string decoded = "";

            foreach (bool bit in bits)
            {
                if (bit)
                {
                    if (current.Right != null)
                    {
                        current = current.Right;
                    }
                }
                else
                {
                    if (current.Left != null)
                    {
                        current = current.Left;
                    }
                }

                if (IsLeaf(current))
                {
                    decoded += current.Symbol;
                    current = this.Root;
                }
            }
            return decoded;
        }

        public static bool IsLeaf(Node node)
        {
            return (node.Left == null && node.Right == null);
        }
    }

    public class Node
    {
        public char Symbol { get; set; }
        public int Frequence { get; set; }
        public Node Right { get; set; }
        public Node Left { get; set; }

        public List<bool> Traverse(char symbol, List<bool> data)
        {
            if (Right == null && Left == null)
            {
                if (symbol.Equals(this.Symbol))
                {
                    return data;
                }
                return null;
            }
            else
            {
                List<bool> left = null;
                List<bool> right = null;

                if (Left != null)
                {
                    List<bool> leftPath = new();
                    leftPath.AddRange(data);
                    leftPath.Add(false);
                    left = Left.Traverse(symbol, leftPath);
                }

                if (Right != null)
                {
                    List<bool> rightPath = new();
                    rightPath.AddRange(data);
                    rightPath.Add(true);
                    right = Right.Traverse(symbol, rightPath);
                }

                if (left != null)
                {
                    return left;
                }
                return right;
            }
        }
    }
}

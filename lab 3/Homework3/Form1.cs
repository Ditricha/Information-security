using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace Homework_3
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        const int ENCRYPT_SIZE = 1;
        const int ENCRYPT_TEXT_SIZE = 3;
        const int ENCRYPT_TEXT_MAX_SIZE = 999;

        //выбор изображения, текста, шифровка и сохранение
        private void EncryptButton_Click(object sender, EventArgs e)
        {
            string picFile;
            OpenFileDialog picture = new OpenFileDialog();
            picture.Filter = "Файлы изображений (*.bmp)|*.bmp|Все файлы (*.*)|*.*";
            
            if (picture.ShowDialog() == DialogResult.OK)
            {
                picFile = picture.FileName;
            }
            else
            {
                picFile = " ";
                return;
            }

            FileStream readFile;
            try
            {
                readFile = new FileStream(picFile, FileMode.Open);
            }
            catch (IOException)
            {
                MessageBox.Show("Ошибка открытия файла.", "Ошибка");
                return;
            }
            Bitmap bitPic = new Bitmap(readFile);

            string textFile;
            OpenFileDialog text = new OpenFileDialog();
            text.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            
            if (text.ShowDialog() == DialogResult.OK)
            {
                textFile = text.FileName;
            }
            else
            {
                textFile = " ";
                return;
            }

            FileStream readText;
            try
            {
                readText = new FileStream(textFile, FileMode.Open);
            }
            catch (IOException)
            {
                MessageBox.Show("Ошибка открытия файла.", "Ошибка");
                return;
            }
            BinaryReader byteText = new BinaryReader(readText, Encoding.ASCII);

            //считывание текста для шифрования
            List<byte> byteList = new List<byte>();
            while (byteText.PeekChar() != -1)
            { 
                byteList.Add(byteText.ReadByte());
            }
            int countText = byteList.Count;
            byteText.Close();
            readFile.Close();

            if (countText > (ENCRYPT_TEXT_MAX_SIZE - ENCRYPT_SIZE - ENCRYPT_TEXT_SIZE))
            {
                MessageBox.Show("Необходимо уменьшить выбранный размер текста, он слишком большой для данного алгоритма.", "Справка");
                return;
            }

            if (countText > (bitPic.Width * bitPic.Height))
            {
                MessageBox.Show("Выбранное изображение слишком мало.", "Справка");
                return;
            }

            byte[] symbol = Encoding.GetEncoding(1251).GetBytes("/");
            BitArray arrayStartSymbol = ToBit(symbol[0]);
            Color currentColor = bitPic.GetPixel(0, 0);

            //RGB
            BitArray arrayTemporary = ToBit(currentColor.R);
            arrayTemporary[0] = arrayStartSymbol[0];
            arrayTemporary[1] = arrayStartSymbol[1];
            byte newR = ToByte(arrayTemporary);

            arrayTemporary = ToBit(currentColor.G);
            arrayTemporary[0] = arrayStartSymbol[2];
            arrayTemporary[1] = arrayStartSymbol[3];
            arrayTemporary[2] = arrayStartSymbol[4];
            byte newG = ToByte(arrayTemporary);

            arrayTemporary = ToBit(currentColor.B);
            arrayTemporary[0] = arrayStartSymbol[5];
            arrayTemporary[1] = arrayStartSymbol[6];
            arrayTemporary[2] = arrayStartSymbol[7];
            byte newB = ToByte(arrayTemporary);

            Color newColor = Color.FromArgb(newR, newG, newB);
            bitPic.SetPixel(0, 0, newColor);

            WriteCountSymbolText(countText, bitPic);
            int index = 0;
            bool st = false;
            
            for (int i = ENCRYPT_TEXT_SIZE + 1; i < bitPic.Width; i++)
            {
                for (int j = 0; j < bitPic.Height; j++)
                {
                    Color pixelColor = bitPic.GetPixel(i, j);
                    
                    if (index == byteList.Count)
                    {
                        st = true;
                        break;
                    }

                    BitArray arrayColor = ToBit(pixelColor.R);
                    BitArray arrayMessage = ToBit(byteList[index]);
                    arrayColor[0] = arrayMessage[0];
                    arrayColor[1] = arrayMessage[1];
                    byte new_R = ToByte(arrayColor);

                    arrayColor = ToBit(pixelColor.G);
                    arrayColor[0] = arrayMessage[2];
                    arrayColor[1] = arrayMessage[3];
                    arrayColor[2] = arrayMessage[4];
                    byte new_G = ToByte(arrayColor);

                    arrayColor = ToBit(pixelColor.B);
                    arrayColor[0] = arrayMessage[5];
                    arrayColor[1] = arrayMessage[6];
                    arrayColor[2] = arrayMessage[7];
                    byte new_B = ToByte(arrayColor);

                    Color new_Color = Color.FromArgb(new_R, new_G, new_B);
                    bitPic.SetPixel(i, j, new_Color);
                    index++;
                }

                if (st)
                {
                    break;
                }
            }
            pictureBox1.Image = bitPic;

            String savePicFile;
            SaveFileDialog savePic = new SaveFileDialog();
            savePic.Filter = "Файлы изображений (*.bmp)|*.bmp|Все файлы (*.*)|*.*";
            
            if (savePic.ShowDialog() == DialogResult.OK)
            {
                savePicFile = savePic.FileName;
            }
            else
            {
                savePicFile = " ";
                return;
            };

            FileStream writeFile;
            try
            {
                writeFile = new FileStream(savePicFile, FileMode.Create);
            }
            catch (IOException)
            {
                MessageBox.Show("Ошибка открытия файла на запись", "Ошибка");
                return;
            }

            bitPic.Save(writeFile, System.Drawing.Imaging.ImageFormat.Bmp);
            writeFile.Close();
        }

        //расшифровка сообщения в изображение и сохранение
        private void DecryptButton_Click(object sender, EventArgs e)
        {
            string picFile;
            OpenFileDialog picture = new OpenFileDialog();
            picture.Filter = "Файлы изображений (*.bmp)|*.bmp|Все файлы (*.*)|*.*";
            
            if (picture.ShowDialog() == DialogResult.OK)
            {
                picFile = picture.FileName;
            }
            else
            {
                picFile = " ";
                return;
            }

            FileStream readFile;
            try
            {
                readFile = new FileStream(picFile, FileMode.Open);
            }
            catch (IOException)
            {
                MessageBox.Show("Ошибка открытия файла.", "Ошибка");
                return;
            }
            Bitmap bitPic = new Bitmap(readFile);

            int countSymbol = ReadCountSymbolText(bitPic);
            byte[] message = new byte[countSymbol];
            int index = 0;
            bool st = false;
            
            for (int i = ENCRYPT_TEXT_SIZE + 1; i < bitPic.Width; i++)
            {
                for (int j = 0; j < bitPic.Height; j++)
                {
                    Color pixelColor = bitPic.GetPixel(i, j);
                    if (index == message.Length)
                    {
                        st = true;
                        break;
                    }
                    BitArray arrayColor = ToBit(pixelColor.R);
                    BitArray arrayMessage = ToBit(pixelColor.R); ;
                    arrayMessage[0] = arrayColor[0];
                    arrayMessage[1] = arrayColor[1];

                    arrayColor = ToBit(pixelColor.G);
                    arrayMessage[2] = arrayColor[0];
                    arrayMessage[3] = arrayColor[1];
                    arrayMessage[4] = arrayColor[2];

                    arrayColor = ToBit(pixelColor.B);
                    arrayMessage[5] = arrayColor[0];
                    arrayMessage[6] = arrayColor[1];
                    arrayMessage[7] = arrayColor[2];
                    message[index] = ToByte(arrayMessage);
                    index++;
                }

                if (st)
                {
                    break;
                }
            }
            string stringMessage = Encoding.GetEncoding(1251).GetString(message);

            string saveFileText;
            SaveFileDialog saveText = new SaveFileDialog();
            saveText.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            
            if (saveText.ShowDialog() == DialogResult.OK)
            {
                saveFileText = saveText.FileName;
            }
            else
            {
                saveFileText = " ";
                readFile.Close();
                return;
            };

            FileStream writeFile;
            try
            {
                writeFile = new FileStream(saveFileText, FileMode.Create);
            }
            catch (IOException)
            {
                MessageBox.Show("Ошибка открытия файла для записи.", "Ошибка");
                readFile.Close();
                return;
            }
            StreamWriter writeText = new StreamWriter(writeFile, Encoding.Default);
            writeText.Write(stringMessage);
            MessageBox.Show("Текст записан в файл.", "Справка");
            
            writeText.Close();
            writeFile.Close();
            readFile.Close();
        }

        private void оЗаданиеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            about ab = new about();
            ab.ShowDialog();
        }

        //перевод байтов в биты
        private BitArray ToBit(byte source)
        {
            BitArray arrayBit = new BitArray(8);
            bool st;

            for (int i = 0; i < 8; i++)
            {
                if ((source >> i & 1) == 1)
                {
                    st = true;
                }
                else st = false;
                arrayBit[i] = st;
            }
            return arrayBit;
        }

        //перевод битов в байты
        private byte ToByte(BitArray source)
        {
            byte number = 0;

            for (int i = 0; i < source.Count; i++)
            {
                if (source[i] == true)
                    number += (byte)Math.Pow(2, i);
            }
            return number;
        }

        //нормализация количества символов для шифрования
        private byte[] CountSymbolNormalization(byte[] countSymbols)
        {
            int padding = ENCRYPT_TEXT_SIZE - countSymbols.Length;
            byte[] countWriting = new byte[ENCRYPT_TEXT_SIZE];

            for (int j = 0; j < padding; j++)
            {
                countWriting[j] = 0x30;
            }

            for (int j = padding; j < ENCRYPT_TEXT_SIZE; j++)
            {
                countWriting[j] = countSymbols[j - padding];
            }
            return countWriting;
        }

        //запись количества символов для шифрования в первые биты изображения
        private void WriteCountSymbolText(int count, Bitmap source)
        {
            byte[] countSymbols = Encoding.GetEncoding(1251).GetBytes(count.ToString());

            if (countSymbols.Length < ENCRYPT_TEXT_SIZE)
            {
                countSymbols = CountSymbolNormalization(countSymbols);
            }

            for (int i = 0; i < ENCRYPT_TEXT_SIZE; i++)
            {
                BitArray bitCount = ToBit(countSymbols[i]);
                Color pixelColor = source.GetPixel(0, i + 1);

                //RGB, запись в биты цветов изначальных пикселей новых цветов пикселей
                BitArray bitCurrentPixelColor = ToBit(pixelColor.R); //бит цветов текущего пикселя
                bitCurrentPixelColor[0] = bitCount[0];
                bitCurrentPixelColor[1] = bitCount[1];
                byte newR = ToByte(bitCurrentPixelColor); //новый бит цветов пикселя

                bitCurrentPixelColor = ToBit(pixelColor.G);
                bitCurrentPixelColor[0] = bitCount[2];
                bitCurrentPixelColor[1] = bitCount[3];
                bitCurrentPixelColor[2] = bitCount[4];
                byte newG = ToByte(bitCurrentPixelColor);

                bitCurrentPixelColor = ToBit(pixelColor.B);
                bitCurrentPixelColor[0] = bitCount[5];
                bitCurrentPixelColor[1] = bitCount[6];
                bitCurrentPixelColor[2] = bitCount[7];
                byte newB = ToByte(bitCurrentPixelColor);

                Color newColor = Color.FromArgb(newR, newG, newB);
                source.SetPixel(0, i + 1, newColor); //запись полученного цвета в изображение
            }
        }

        //чтение количества символов из первых бит изображения для дешифрования 
        private int ReadCountSymbolText(Bitmap source)
        {
            byte[] result = new byte[ENCRYPT_TEXT_SIZE];

            for (int i = 0; i < ENCRYPT_TEXT_SIZE; i++)
            {
                Color color = source.GetPixel(0, i + 1);

                BitArray colorArray = ToBit(color.R);
                BitArray bitCount = ToBit(color.R);
                bitCount[0] = colorArray[0];
                bitCount[1] = colorArray[1];

                colorArray = ToBit(color.G);
                bitCount[2] = colorArray[0];
                bitCount[3] = colorArray[1];
                bitCount[4] = colorArray[2];

                colorArray = ToBit(color.B);
                bitCount[5] = colorArray[0];
                bitCount[6] = colorArray[1];
                bitCount[7] = colorArray[2];

                result[i] = ToByte(bitCount);
            }
            string message = Encoding.GetEncoding(1251).GetString(result);
            return Convert.ToInt32(message, 10);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeaserCipher
{
    class Program
    {
        static double[] table = {8.2, 1.5, 2.8, 4.3, 12.7, 2.2, 2.0, 6.1, 7.0, 0.2, 0.8, 4.0, 2.4, 6.7, 7.5, 1.9, 0.1, 6.0, 6.3, 9.1, 2.8, 1.0, 2.4, 0.2, 2.0, 0.1};
        static String list = "abcdefghijklmnopqrstuvwxyz";

        static int let2num (char c)
        {
            int num = (int)(c - 'a');
            if(num < 0 || num > 25)
            {
                return (int)c;
            }

            return num; 
        }

        static char num2let(int n)
        {
            if(n < 0 || n > 25)
            {
                return (char)n;
            }

            int letter = (n + 'a');

            return (char)letter;
        }
        static char shift(int shiftAmt, char c)
        {
            if(c >= 'a' && c <= 'z')
            {
                int cNum = (let2num(c) + shiftAmt) % 26;
                if(cNum < 0)
                {
                    cNum += 26;
                }
                return num2let(cNum);
            }
            else
            {
                return c;
            }
        }

        static String encode(int shiftAmt, String str)
        {
            System.Text.StringBuilder cipherText = new System.Text.StringBuilder();

            for(int i = 0; i < str.Length; i++)
            {
                cipherText.Append( shift(shiftAmt, str[i]));
            }

            return cipherText.ToString();
        }

        static String decode(int shiftAmt, String str)
        {
            return encode((0 - shiftAmt), str);

        }

        static int count(char c, String str)
        {
            int nChars = 0;

            for(int i = 0; i < str.Length; i++)
            {
                if(c == str[i])
                {
                    nChars++;
                }
            }

            return nChars;
        }

        static double percent(int num1, int num2)
        {
            double value = ((double)num1/num2) * 100;

            return value;

        }

        static double[] freqs(String str)
        {
            double[] lFreqs = new double[list.Length];

            for(int i = 0; i < lFreqs.Length; i++)
            {
                lFreqs[i] = percent(count(list[i], str), str.Length);
            }

            return lFreqs;

        }
        
        static double[] rotate(int n, double[] lFreqs)
        {
            int offset = (lFreqs.Length - n) % lFreqs.Length;
            int j;

            if(offset > 0)
            {
                double[] clone = (double[])lFreqs.Clone();

                for(int i = 0; i < lFreqs.Length - 1; i++)
                {
                    j = (i + (lFreqs.Length - offset)) % lFreqs.Length;
                    lFreqs[i] = clone[i];
                }
                
            }
            return lFreqs;
        }

        static double chiSqr(double[] of)
        {
            double sum = 0;
            for(int i = 0; i < of.Length; i++)
            {
                sum += Math.Pow((of[i] - table[i]), 2) / table[i];
            }

            return sum;
        }
        
        static int position(double a, double[] chiSqrList)
        {
            int index = 0;
            for(int i = 0; i < chiSqrList.Length; i++)
            {
                if (a == chiSqrList[i])
                {
                    index = i;
                }
            }

            return index;

        }

        static String crack(string str)
        {
            double[] encodedFreq = freqs(str);
            double[] encodedChiSqr = new double[26];

            String plainText;

            for(int i = 0; i < encodedChiSqr.Length; i++)
            {
                encodedChiSqr[i] = chiSqr(encodedFreq);
                encodedFreq = rotate(1, encodedFreq);
            }
            double lowest = encodedChiSqr[0];

            for(int j = 1; j < encodedChiSqr.Length; j++)
            {
                if(encodedChiSqr[j] < lowest)
                {
                    lowest = encodedChiSqr[j];
                }
            }

            plainText = decode(position(lowest, encodedChiSqr), str);

            return plainText;
        }

        
        static void Main(string[] args)
        {

            char test = 'e';
            String test2 = "myxqbkdevkdsyxc yx mywzvodsxq dro ohkw!";
            String test3 = "kdvnhoolvixq";
            double[] test4 = { 1, 3, 5, 7, 11 };

            Console.WriteLine(let2num(test));
            Console.WriteLine(num2let(25));
            Console.WriteLine(shift(3, test));
            Console.WriteLine(encode(3, "Haskellisfun!"));
           

            Console.WriteLine(num2let(let2num(test)));
			Console.WriteLine(shift(3, test));
			Console.WriteLine(encode(3, test2));
			Console.WriteLine(decode(3, test3));
			Console.WriteLine(count('l', test2));
			Console.WriteLine(percent(2, 12));
			Console.WriteLine(freqs(test2));
			Console.WriteLine(rotate(3, freqs(test2)));
			Console.WriteLine(chiSqr(freqs(test2)));
			Console.WriteLine(position(5, test4));	
			Console.WriteLine(crack(encode(3, test2)));

            Console.WriteLine(crack("myxqbkdevkdsyxc yx mywzvodsxq dro ohkw!"));



            Console.Read();
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Matrix
{
    internal class Program
    {
        static int stbPotok = 50;
        static int width = 150;
        static int heigth = 30;
        
        static Random random = new Random();
        static string[] bykvu = {"m", "s", "a", "v", "7", "r", "c"};






        static void PotokMatricu(object stb)
        {
            int stolbec = (int)stb * stbPotok;
            int time = 1;
            int minRazmer = heigth / 4;
            int maxRazmer = heigth / 2;
            int timeMin = 1;
            int timeMax = 5;
            int counter = 0;


            int[,] matrix = new int[stbPotok, 3];
            for (int i = 0; i < stbPotok; i++)
            {
                GenereratorFifuru(i);
            }


            Console.ForegroundColor = ConsoleColor.Green;
            while (true)
            {
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    if(counter % matrix[i, 2] == 0)
                    {
                        int head = matrix[i, 0];
                        if (head >= 0 && head < heigth)
                        {

                            int randomBykva = random.Next(0, bykvu.Length);

                            lock (bykvu)
                            {
                                Console.SetCursorPosition(stolbec + i, head);
                                Console.Write(bykvu[randomBykva]);
                            }
                        }

                        int start = matrix[i, 0] - 1;
                        if (start >= 0 && start < heigth)
                        {

                            int randomBykva = random.Next(0, bykvu.Length);

                            lock (bykvu)
                            {
                                
                                Console.SetCursorPosition(stolbec + i, start);
                                Console.Write(bykvu[randomBykva]);
                            }
                        }


                        int end = matrix[i, 0] - matrix[i, 1];
                        if (end >= 0 && end < heigth)
                        {
                            lock (bykvu)
                            {
                                Console.SetCursorPosition(stolbec + i, end);
                                Console.Write(" ");
                            }
                        }
                    }
                }
                counter++;
                for (int i = 0; i < matrix.GetLength(0); i++)
                {
                    if(counter % matrix[i, 2] == 0)
                    {
                        matrix[i, 0]++;

                        if (matrix[i, 0] > heigth + matrix[i, 1])
                        {
                            GenereratorFifuru(i);
                        }
                    }
                }
                Thread.Sleep(time);
            }


            void GenereratorFifuru(int stolb)
            {
                int figura = random.Next(minRazmer, maxRazmer);
                matrix[stolb, 0] = 0;

                if (counter == 0)
                {
                    matrix[stolb, 0] = random.Next(30, 120) * -1;
                }
                
                matrix[stolb, 1] = figura;
                matrix[stolb, 2] = random.Next(timeMin, timeMax);
            }
        }


        static void Main(string[] args)
        {
            Console.SetBufferSize(width + 1, heigth);
            Console.SetWindowSize(width + 1, heigth);
            Console.CursorVisible = false;

            int kolPotokov = width / stbPotok;
            for (int i = 0; i < kolPotokov; i++)
            {
                new Thread(PotokMatricu).Start(i);
            }

            Console.ReadKey();
        }
    }
}

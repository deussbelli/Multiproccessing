using System;
using System.Threading;

//1.Знайти суму елементів матриці, як суму елементів верхньої трикутної та нижньої трикутною підматриць.  
//  Матриця задається рандомно. Розмірність матриці вводиться з консолі.

namespace MatrixSumThreads
{
    class lab1
    {
        static int[,] matrix;
        static int size;
        static int upperTriangleSum = 0;
        static int lowerTriangleSum = 0;

        static void Main1(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.Write("Enter the dimension of the matrix: ");
            size = int.Parse(Console.ReadLine());

            matrix = new int[size, size];
            Random random = new Random();

            Console.WriteLine("Generated matrix:");
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    matrix[i, j] = random.Next(1, 11);
                    Console.Write(matrix[i, j] + "\t");
                }
                Console.WriteLine();
            }

            Thread upperThread = new Thread(CalculateUpperTriangleSum);
            Thread lowerThread = new Thread(CalculateLowerTriangleSum);

            upperThread.Name = "Upper Triangle Sum Thread";
            lowerThread.Name = "Lower Triangle Sum Thread";

            upperThread.Start();
            lowerThread.Start();

            upperThread.Join();
            lowerThread.Join();

            Console.WriteLine($"> The sum of the elements of the upper triangular matrix: {upperTriangleSum}");

            Console.WriteLine($"> The sum of the elements of the lower triangular matrix: {lowerTriangleSum}");

            Console.WriteLine($"> Total amount of items: {upperTriangleSum + lowerTriangleSum}\n");

            Console.ReadKey();
        }
        static void CalculateUpperTriangleSum()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = i; j < size; j++)
                {
                    upperTriangleSum += matrix[i, j];
                }
            }
            Console.WriteLine("\nThe calculation of the upper triangular sum is complete.\n");
        }
        static void CalculateLowerTriangleSum()
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j <= i; j++)
                {
                    lowerTriangleSum += matrix[i, j];
                }
            }
            Console.WriteLine("Computation of the lower triangular sum is complete.\n");
        }
    }
}

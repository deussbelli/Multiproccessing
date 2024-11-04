using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Завдання 1.
//Написати паралельну програму, яка обчислює значення виразу F за варіантом.
//Кожна арифметична операція має бути виконана в окремому потоці.
//Змінні ініціалізуються в тому ж потоці, в якому вони вперше використовуються.
//Ініціалізувати змінні наступними значеннями: x1 = 1, x2 = 2, x3 = 3, x4 = 4, x5 = 5, x6 = 6.

//Варіант: 1.
//Вираз F: x1*x2 + x3 + x4*x5 + x6


namespace ParallelExpression
{
    internal class lab3
    {
        public EventWaitHandle wh1 = new AutoResetEvent(false),
                               wh2 = new AutoResetEvent(false),
                               wh3 = new AutoResetEvent(false);

        public int x1, x2, x3, x4, x5, x6;

        public int part1, part2;

        static public void Main3(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            lab3 l = new lab3();

            l.x1 = 1;
            l.x2 = 2;
            l.x3 = 3;
            l.x4 = 4;
            l.x5 = 5;
            l.x6 = 6;

            var T0 = new Thread(() => Func0(l)) { Name = "T0 - Part1: multiply x1 by x2" }; // Потік для обчислення x1 * x2
            var T1 = new Thread(() => Func1(l)) { Name = "T1 - Part2: multiply x4 by x5" }; // Потік для обчислення x4 * x5
            var T2 = new Thread(() => Func2(l)) { Name = "T2 - Sum: part1 + x3; part2 + x6" }; // Потік для додавання x3 і x6
            var T3 = new Thread(() => Func3(l)) { Name = "T3 - Sum: part1 + part2" }; // Потік для об'єднання частин

            T0.Start();
            T1.Start();
            T2.Start();
            T3.Start();

            T0.Join();
            T1.Join();
            T2.Join();
            T3.Join();

            Console.ReadKey();
        }

        static void Func0(lab3 l)
        {
            l.part1 = l.x1 * l.x2; 
            l.wh1.Set(); 
        }

        static void Func1(lab3 l)
        {
            l.part2 = l.x4 * l.x5; 
            l.wh2.Set();
        }

        static void Func2(lab3 l)
        {
            l.wh1.WaitOne();
            l.wh2.WaitOne(); 
            l.part1 += l.x3; 
            l.part2 += l.x6;
            l.wh3.Set();
        }

        static void Func3(lab3 l)
        {
            l.wh3.WaitOne(); 
            int F = l.part1 + l.part2; 
            Console.WriteLine("F = {0}", F);
        }
    }
}


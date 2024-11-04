using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

//1. Варіант:1.
//   Задано граф роботи потоків. 
//   Кожна задача в графі виконується в окремому потоці. 
//   Використовуючи семафори, виконати синхронізацію потоків. 
//   Змінні x, y та z є глобальними для всіх потоків та проініціалізовані наступним чином:
//   x = номер варіанту = 1, y = 5, z = 4.

namespace SemaphoreSlimProgram
{
    internal class lab2
    {
        static int x = 1;
        static int y = 5;

        static SemaphoreSlim t1T3 = new SemaphoreSlim(0, 1); //  T1 перейти до T3
        static SemaphoreSlim t2T4 = new SemaphoreSlim(0, 1); //  T2 перейти до T4
        static SemaphoreSlim t3T5 = new SemaphoreSlim(0, 1); //  T3 перейти до T5
        static SemaphoreSlim t4T5 = new SemaphoreSlim(0, 1); //  T4 перейти до T5

        static void Main2()
        {
            Thread thread1 = new Thread(T1) { Name = "T1 - Multiply x by 5" };
            Thread thread2 = new Thread(T2) { Name = "T2 - Add 2 to y" };
            Thread thread3 = new Thread(T3) { Name = "T3 - Add 2 to x after T1" };
            Thread thread4 = new Thread(T4) { Name = "T4 - Subtract 3 from y after T2" };
            Thread thread5 = new Thread(T5) { Name = "T5 - Multiply x and y (final)" };


            thread1.Start();
            thread2.Start();
            thread3.Start();
            thread4.Start();
            thread5.Start();

            thread1.Join();
            thread2.Join();
            thread3.Join();
            thread4.Join();
            thread5.Join();

            Console.WriteLine($"Final values: x = {x}, y = {y}");
        }

        static void T1()
        {
            x = x * 5;
            Console.WriteLine($"{Thread.CurrentThread.Name}: x = {x}");

            t1T3.Release();
        }

        static void T2()
        {
            y = y + 2;
            Console.WriteLine($"{Thread.CurrentThread.Name}: y = {y}");

            t2T4.Release();
        }

        static void T3()
        {
            t1T3.Wait();

            x = x + 2;
            Console.WriteLine($"{Thread.CurrentThread.Name}: x = {x}");

            t3T5.Release();
        }

        static void T4()
        {
            t2T4.Wait();

            y = y - 3;
            Console.WriteLine($"{Thread.CurrentThread.Name}: y = {y}");

            t4T5.Release();
        }

        static void T5()
        {
            t3T5.Wait();
            t4T5.Wait();

            y = x * y;
            Console.WriteLine($"{Thread.CurrentThread.Name}: y = {y}");
        }
    }
}

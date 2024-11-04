using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//1.Розробити власну багатопоточну програму згідно наступних вимог:
//– у програмі має створюватися масив з 10 потоків;
//– до конструктора кожного потоку має передаватися функція із трьома аргументами:
//перший аргумент – ім’я, другий аргумент – прізвище, третій аргумент – випадкове ціле число у діапазоні від 0 до 9;

//– функція має виводити на консоль лише ім’я, якщо третій аргумент парний, і лише прізвище, якщо третій аргумент непарний.

namespace MultiThreadedProgram
{
    internal class lab5
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            string[] firstNames = { "John", "Alice", "Bob", "Mary", "Tom", "Anna", "James", "Laura", "Peter", "Linda" };
            string[] lastNames = { "Smith", "Brown", "Johnson", "Williams", "Jones", "Davis", "Wilson", "Miller", "Anderson", "Taylor" };

            Thread[] threads = new Thread[10];

            int[] randomNumbers = new int[10];

            Random random = new Random();

            for (int i = 0; i < 11; i++)
            {
                int index = i;
                randomNumbers[i] = random.Next(1, 11);

                Console.WriteLine($"\tTESTS: Потік {i + 1}: Ім'я: {firstNames[i]}, Прізвище: {lastNames[i]}, Число: {randomNumbers[i]}");

                // Створення потоку з передачею імені, прізвища та випадкового числа
                threads[i] = new Thread(() => PrintNameOrSurname(firstNames[i], lastNames[i], randomNumbers[i]));
                threads[i].Start();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            Console.WriteLine("\nВсі потоки завершено!\n");
        }

        // Функція, що виконується у кожному потоці
        static void PrintNameOrSurname(string firstName, string lastName, int number)
        {
            if (number % 2 == 0)
            {
                Console.WriteLine($"Ім'я: {firstName} (Число: {number})");
            }
            else
            {
                Console.WriteLine($"Прізвище: {lastName} (Число: {number})");
            }
        }
    }
}
import java.util.*;
import java.util.concurrent.locks.ReentrantLock;

// Варіант: 1
// 1.	Створити два нових класи потоків OddWriter і Non OddWriter (похідні від класу Thread),
// що забезпечують вивід впорядкованої послідовність цілих випадкових чисел від 0 до 100. 
// Числа у послідовності не повинні повторюватись і повинні бути впорядковані за зростанням.
// Потік OddWriter повинне виводити на екран непарні значення, а потік NonOddWriter - парні значення. 
// Для вирішення завдання в додаток до потоків OddReader і NonOddReader можна створювати довільну кількість потоків.

public class OrderedNumberWriter {
    public static final List<Integer> numbers = new ArrayList<>();
    public static final ReentrantLock lock = new ReentrantLock();

    public static void main(String[] args) throws InterruptedException {
        Scanner scanner = new Scanner(System.in);
        System.out.print("Enter ThreadCount for OddWriter: ");
        int oddThreadCount = scanner.nextInt();
        System.out.print("Enter ThreadCount for NonOddWriter: ");
        int nonOddThreadCount = scanner.nextInt();
        scanner.close();

        long startTime = System.currentTimeMillis(); 

        generateUniqueNumbers(1000);
        Collections.sort(numbers);

        List<Thread> threads = new ArrayList<>();

        // Створення та запуск потоків OddWriter
        for (int i = 0; i < oddThreadCount; i++) {
            Thread oddWriter = new OddWriter();
            threads.add(oddWriter);
            oddWriter.start();
        }

        // Створення та запуск потоків NonOddWriter
        for (int i = 0; i < nonOddThreadCount; i++) {
            Thread nonOddWriter = new NonOddWriter();
            threads.add(nonOddWriter);
            nonOddWriter.start();
        }

        for (Thread thread : threads) {
            thread.join();
        }

        long endTime = System.currentTimeMillis();
        System.out.println("Execution time: " + (endTime - startTime) + " ms");
    }

    // Метод для заповнення списку унікальними випадковими числами
    public static void generateUniqueNumbers(int range) {
        Random random = new Random();
        while (numbers.size() < range) {
            int number = random.nextInt(range);
            if (!numbers.contains(number)) {
                numbers.add(number);
            }
        }
    }

    static class OddWriter extends Thread {
        public void run() {
            lock.lock();
            try {
                for (Integer number : numbers) {
                    if (number % 2 != 0) { // Перевірка на непарність
                        System.out.println("OddWriter: " + number);
                    }
                }
            } finally {
                lock.unlock();
            }
        }
    }

    static class NonOddWriter extends Thread {
        public void run() {
            lock.lock();
            try {
                for (Integer number : numbers) {
                    if (number % 2 == 0) { // Перевірка на парність
                        System.out.println("NonOddWriter: " + number);
                    }
                }
            } finally {
                lock.unlock();
            }
        }
    }
}

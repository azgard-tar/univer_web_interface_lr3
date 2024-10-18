using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        // Демонстрація паралельного завантаження файлів
        Console.WriteLine("=== Паралельне завантаження файлів ===");
        Thread downloadThread1 = new Thread(DownloadFile1);
        Thread downloadThread2 = new Thread(DownloadFile2);
        downloadThread1.Start();
        downloadThread2.Start();
        downloadThread1.Join();
        downloadThread2.Join();

        // Демонстрація асинхронної обробки файлів
        Console.WriteLine("\n=== Асинхронна обробка файлів ===");
        ProcessFilesAsync().Wait();

        Console.WriteLine("Програма завершена.");
    }

    static void DownloadFile1()
    {
        Console.WriteLine("Завантаження файлу 1...");
        Thread.Sleep(2000);

        File.WriteAllText("file1.txt", "Це перший файл. Його потрібно обробити.");
        Console.WriteLine("Файл 1 завантажено.");
    }

    static void DownloadFile2()
    {
        Console.WriteLine("Завантаження файлу 2...");
        Thread.Sleep(3000);

        File.WriteAllText("file2.txt", "Це другий файл. Його також потрібно обробити.");
        Console.WriteLine("Файл 2 завантажено.");
    }

    static async Task ProcessFilesAsync()
    {
        Console.WriteLine("Починається обробка файлів...");

        await ProcessFileAsync("file1.txt");

        await ProcessFileAsync("file2.txt");

        Console.WriteLine("Обробка файлів завершена.");
    }

    static async Task ProcessFileAsync(string fileName)
    {
        Console.WriteLine($"Обробка {fileName}...");

        string content = await File.ReadAllTextAsync(fileName);
        Console.WriteLine($"Вміст файлу {fileName}: {content}");

        string processedContent = content.ToUpper();

        string newFileName = "processed_" + fileName;
        await File.WriteAllTextAsync(newFileName, processedContent);
        Console.WriteLine($"{fileName} оброблено. Результат збережено у {newFileName}.");

        content = await File.ReadAllTextAsync(newFileName);
        Console.WriteLine($"Вміст файлу {newFileName}: {content}");
    }
};
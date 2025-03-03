using System;
using System.IO;
using System.IO.Compression;
using System.Linq;

class Program
{
    static void Main()
    {
        Console.Write("Введите путь к директории: ");
        string directory = Console.ReadLine();
        
        Console.Write("Введите имя файла: ");
        string fileName = Console.ReadLine();
        
        if (Directory.Exists(directory) && !string.IsNullOrWhiteSpace(fileName))
        {
            string filePath = Directory.GetFiles(directory, fileName, SearchOption.AllDirectories).FirstOrDefault();
            
            if (!string.IsNullOrEmpty(filePath))
            {
                Console.WriteLine($"Файл найден: {filePath}\nСодержимое файла:");
                DisplayFileContent(filePath);
                
                Console.Write("Сжать файл? (y/n): ");
                if (Console.ReadLine().Trim().ToLower() == "y")
                {
                    CompressFile(filePath);
                }
            }
            else
            {
                Console.WriteLine("Файл не найден.");
            }
        }
        else
        {
            Console.WriteLine("Некорректный путь или имя файла.");
        }
    }

    static void DisplayFileContent(string filePath)
    {
        try
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (StreamReader reader = new StreamReader(fs))
            {
                Console.WriteLine(reader.ReadToEnd());
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка чтения файла: {ex.Message}");
        }
    }

    static void CompressFile(string filePath)
    {
        string compressedFile = filePath + ".gz";
        try
        {
            using (FileStream originalFileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (FileStream compressedFileStream = new FileStream(compressedFile, FileMode.Create))
            using (GZipStream compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
            {
                originalFileStream.CopyTo(compressionStream);
            }
            Console.WriteLine($"Файл сжат: {compressedFile}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка сжатия: {ex.Message}");
        }
    }
}

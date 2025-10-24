using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace ReadFileSV
{
    internal class Program
    {
        static List<string[]> ReadCsvFile(string filePath)
        {
            List<string[]> rows = new List<string[]>();

            try
            {
                // Read all lines from the CSV file
                string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);

                // Process each line and split by the comma to get individual values
                foreach (string line in lines)
                {
                    string[] values = line.Split(',');

                    // Add the values to the list of rows
                    rows.Add(values);
                }

                Console.WriteLine("CSV file read successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }


            return rows;
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            string csvFilePath = "Book1.csv";

            // Call the method to read and process the CSV data
            List<string[]> csvData = ReadCsvFile(csvFilePath);

            // Display the read data
            // ... code đọc file ở trên ...

            // Tạo một danh sách các dòng đã join
            List<string> joinedLines = new List<string>();
            foreach (string[] row in csvData)
            {
                joinedLines.Add(string.Join(", ", row));
            }

            // Ghi tất cả các dòng này ra một file mới
            File.WriteAllLines("output_test.txt", joinedLines, Encoding.UTF8);

            Console.WriteLine("Đã ghi kết quả ra file output_test.txt. Hãy mở file đó bằng Notepad hoặc VS Code.");
        }
    }
}

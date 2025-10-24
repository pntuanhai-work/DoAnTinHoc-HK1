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
                string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);


                foreach (string line in lines)
                {
                    string[] values = line.Split(',');

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

            List<string[]> csvData = ReadCsvFile(csvFilePath);


            List<string> joinedLines = new List<string>();
            foreach (string[] row in csvData)
            {
                joinedLines.Add(string.Join(", ", row));
            }

            // Ghi tất cả các dòng ra file mới
            File.WriteAllLines("output_test.txt", joinedLines, Encoding.UTF8);

            Console.WriteLine("Đã ghi kết quả ra file output_test.txt");
        }
    }
}

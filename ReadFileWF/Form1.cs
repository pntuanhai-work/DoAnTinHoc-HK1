using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ReadFileWF
{

    public partial class Form1 : Form
    {
        static DataTable ReadCsvFile(string filePath)
        {
            DataTable dataTB = new DataTable();

            try
            {
                string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);


                foreach (string line in lines)
                {
                    string[] values = line.Split(',');

                }
                string[] headers = lines[0].Split(',');
                foreach (string header in headers)
                {
                    dataTB.Columns.Add(header.Trim());
                }

                for (int i = 1; i < lines.Length; i++)
                {
                    string[] data = lines[i].Split(',');

                    ///them neu cot khop
                    if (data.Length == dataTB.Columns.Count)
                    {
                        // Tạo một DataRow mới và gán giá trị
                        dataTB.Rows.Add(data.Select(d => (object)d.Trim()).ToArray());
                    }
                }
                    Console.WriteLine("CSV file read successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }


            return dataTB;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                
                if(ofd.ShowDialog() == DialogResult.OK)
                {
                    DataTable csvData = ReadCsvFile(ofd.FileName);
                    dataGridView1.DataSource = csvData;
                }
            }
            lblKetQua.Text = "Xử lý thành công ^^";
        }
    }
}

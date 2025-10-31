using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReadFileWF
{

    public partial class Form1 : Form
    {
        private AVLTree treeAVL;
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

        private List<Course> DatatableToCourse (DataTable dt)
        {
            List<Course> listCourse = new List<Course>();

            foreach(DataRow row in dt.Rows)
            {
                 Course course = new Course(
                     Convert.ToInt32(row["id"]),
                    row["title"].ToString(),
                    row["url"].ToString(),
                    Convert.ToDouble(row["rating"]),
                    Convert.ToInt32(row["num_reviews"]),
                    Convert.ToInt32(row["num_published_lectures"]),
                    row["created"].ToString(),
                    row["last_update_date"].ToString(),
                    row["duration"].ToString(),
                    Convert.ToInt32(row["instructors_id"]),
                    row["image"].ToString(),
                    row["language"].ToString()
                     );
                listCourse.Add( course );
            }

            return listCourse;
        }

        private DataTable CourseToDatatable(List<Course> cs)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id", typeof(int));
            dt.Columns.Add("title", typeof(string));
            dt.Columns.Add("url", typeof(string));
            dt.Columns.Add("rating", typeof(double));
            dt.Columns.Add("num_reviews", typeof(int));
            dt.Columns.Add("num_published_lectures", typeof(int));
            dt.Columns.Add("created", typeof(string));
            dt.Columns.Add("last_update_date", typeof(string));
            dt.Columns.Add("duration", typeof(string));
            dt.Columns.Add("instructors_id", typeof(int));
            dt.Columns.Add("image", typeof(string));
            dt.Columns.Add("language", typeof(string));

            foreach (Course course in cs) { 
                dt.Rows.Add(course.ID,course.Title,course.URL,course.Rating,course.Num_Reviews,course.Num_Published,course.Create, course.LastUpdate,course.Duration,course.InstructionID,course.Image,course.Language);
            }
            return dt;
        }

        private void LoadDataIntoAVLTree(List<Course> courses)
        {
            treeAVL.Clear();

            foreach (Course course in courses)
            {
                treeAVL.Insert(course);
            }

            MessageBox.Show($"Đã load {courses.Count} khóa học vào cây AVL!",
                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public Form1()
        {
            InitializeComponent();
            treeAVL = new AVLTree();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                
                if(ofd.ShowDialog() == DialogResult.OK)
                {
                    DataTable csvData = ReadCsvFile(ofd.FileName);
                    dataGridView1.DataSource = csvData;

                    List<Course> courses = DatatableToCourse(csvData);
                    LoadDataIntoAVLTree(courses);
                }
            }
            lblKetQua.Text = "Xử lý thành công ^^";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }



        private void btnGetInfo_Click(object sender, EventArgs e)
        {
            string noiDung = treeAVL.Info_OfTree();

            MessageBox.Show(noiDung, "Ket qua");
        }

        private void btnEsc_Click(object sender, EventArgs e)
        {


            List<Course> sapXep = treeAVL.Asc();
            DataTable dt = CourseToDatatable(sapXep);
            dataGridView1.DataSource = dt;
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            List<Course> sapXep = treeAVL.Desc();
            DataTable dt = CourseToDatatable(sapXep);
            dataGridView1.DataSource = dt;
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<Course> listRating = new List<Course>();
            if (cboRating.SelectedIndex == 0) 
                 listRating = treeAVL.ListRating_Option(4.5);
            if(cboRating.SelectedIndex == 1)
                 listRating = treeAVL.ListRating_Option(4.0);
            if (cboRating.SelectedIndex == 2)
                listRating = treeAVL.ListRating_Option(3.0);

            DataTable dt = CourseToDatatable(listRating);
            dataGridView1.DataSource = dt;
        }

    }
}

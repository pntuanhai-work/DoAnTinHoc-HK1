using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadFileWF
{
    public class AVLNode
    {
        public double Rating {  get; set; }
        public List<Course> CoursesList { get; set; }
        public AVLNode Left { get; set; }
        public AVLNode Right { get; set; }
        public int Height { get; set; }

        public AVLNode(double rating, Course course)
        {
            Rating  = rating;
            CoursesList = new List<Course> { course };
            Left = null;
            Right = null;
            Height = 1;
        }

        public void addCourse(Course course)
        {
            CoursesList.Add(course);
        }

        public int getCourseCount()
        {
            return CoursesList.Count;
        }
    }
}

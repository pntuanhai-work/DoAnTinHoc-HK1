using System;
using System.Collections.Generic;
using System.Diagnostics.PerformanceData;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ReadFileWF
{
    public class AVLTree
    {
        private AVLNode root;

        public AVLTree()
        {
            root = null;
        }

        public void Insert(Course course)
        {
            root = insertNode(root, course);
        }

        public AVLNode insertNode(AVLNode node, Course course) {
            if (node == null)
            {
                return new AVLNode(course.Rating, course);
            }

            if (course.Rating < node.Rating) {
                node.Left = insertNode(node.Left, course);            
            }

            if (node.Rating < course.Rating)
            {
                node.Right = insertNode(node.Right, course);
            }

            if (node.Rating == course.Rating)
            {
                node.addCourse(course);
                return node;
            }
            updateHeight(node);
            int balance = getBalance(node);

            // Lech LL 
            if(balance > 1 && course.Rating < node.Left.Rating)
            {
                return rotateRight(node);
            }

            // lech RR 
            if (balance < -1 && course.Rating > node.Right.Rating) { 
                return rotateLeft(node);
            }

            // L-R
            if (balance > 1 && course.Rating > node.Left.Rating) {
                node.Left = rotateLeft(node.Left);
                return rotateRight(node);
            }

            // R-L
            if (balance < -1 && course.Rating < node.Right.Rating) { 
                node.Right = rotateRight(node.Right);
                return rotateLeft(node);
            }

            return node;
        }

        private int getHeight(AVLNode node) { 
            return node ==  null ? 0 : node.Height;
        }

        public int GetTreeHeight()
        {
            return getHeight(root);
        }

        private int getBalance(AVLNode node) { 
            return node == null ? 0 : getHeight(node.Left) - getHeight(node.Right);
        }

        private void updateHeight(AVLNode node) { 
            if(node != null)
            {
                node.Height = 1 + Math.Max(getHeight(node.Left), getHeight(node.Right));
            }
        }


        private AVLNode rotateRight(AVLNode y)
        {
            AVLNode x = y.Left;
            AVLNode T2 = x.Right;

            x.Right = y;
            y.Left = T2;

            updateHeight(y);
            updateHeight(x);
            return x;
        }

        private AVLNode rotateLeft(AVLNode x)
        {
            AVLNode y = x.Right;      // 1. Lưu node con phải
            AVLNode T2 = y.Left;      // 2. Lưu cây con trái của y

            // Thực hiện xoay
            y.Left = x;               // 3. y lên làm root, x thành con trái
            x.Right = T2;             // 4. T2 thành con phải của x

            // Cập nhật chiều cao
            updateHeight(x);          // 5. Update x trước
            updateHeight(y);          // 6. Update y sau

            return y;                 // 7. Return root mới
        }

        public int countNodes()
        {
            return CountNode_Tree(root);
        }

        private int CountNode_Tree(AVLNode node)
        {
            if(node == null )
                return 0;
            return 1 + CountNode_Tree(node.Left) + CountNode_Tree(node.Right);
        }

        private int CountLeaf_Tree(AVLNode node)
        {
            if(node == null ) return 0;
            if(node.Left == null && node.Right == null) return 1;
            return CountLeaf_Tree(node.Left) + CountLeaf_Tree(node.Right);
        }

        public int countLeft()
        {
            return CountLeaf_Tree(root);
        }

        public double? GetMinRating()
        {
            if(root == null) return null;
            AVLNode nodeCurrent = root;
            while(nodeCurrent.Left != null)
            {
                nodeCurrent= nodeCurrent.Left;
            }
            return nodeCurrent.Rating;
        }

        public double? GetMaxRating()
        {
            if(root == null) return null;
            AVLNode currentNode = root; 
            while (currentNode.Right != null)
            {
                currentNode = currentNode.Right;
            }

            return currentNode.Rating;
        }

        public string Info_OfTree()
        {
            if (root == null)
                return "Cay rong!";
            return "===THONG KE CAY AVL===\n" +
                $"Chieu cao cay: {GetTreeHeight()}\n" +
                $"Tong so node: {countNodes()}\n" +
                $"So nut la: {countLeft()}\n" +
                $"Rating thap nhat: {GetMinRating()}\n" + 
                $"Rating cao nhat: {GetMaxRating()}\n";
        }

        // xoá toàn bộ cây
        public void Clear()
        {
            root = null;
        }

        private void inOrderUp(AVLNode node, List<Course> result)
        {
            if(node == null) return;
            inOrderUp(node.Left, result);
            result.AddRange(node.CoursesList);
            inOrderUp(node.Right, result);
        }

        public List<Course> Asc()
        {
            List<Course> result = new List<Course>();
            inOrderUp(root, result);
            return result;
        }

        private void inOrderDown(AVLNode node, List<Course> result)
        {
            if(node == null ) return;
            inOrderDown(node.Right, result);
            result.AddRange(node.CoursesList);
            inOrderDown(node.Left, result);
        }

        public List<Course> Desc() { 
            List<Course> result = new List<Course>();
            inOrderDown(root, result);
            return result;
        }

        private void sapXepTrenRating(AVLNode node, List<Course> result, double minRating)
        {
            if (node == null) return;
            if (node.Rating >= minRating) {
                result.AddRange(node.CoursesList);
                sapXepTrenRating(node.Left, result, minRating);
                sapXepTrenRating(node.Right,result, minRating);
            }
            else //neu root <min rating thi chi search ben phai
            {
                sapXepTrenRating(node.Right,result,minRating);
            }
        }

        public List<Course> ListRating_Option(double minRating)
        {
            List<Course> result_Checked = new List<Course>();
            sapXepTrenRating(root, result_Checked, minRating);
            return result_Checked;
        }
    }
}

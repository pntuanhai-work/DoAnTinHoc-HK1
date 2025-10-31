using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace ReadFileWF
{
    public class Course
    {
        public int ID { get; set; }
        public string Title {  get; set; }
        public string URL { get; set; }
        public double Rating { get; set; }
        public int Num_Reviews { get; set; }
        public int Num_Published { get; set; }
        public string Create {  get; set; }
        public string LastUpdate { get; set; }
        public string Duration { get; set; }
        public int InstructionID { get; set; }
        public string Image {  get; set; }
        public string Language { get; set; }


        public Course(int iD, string title, string uRL, double rating, int num_Reviews, int num_Published, string create, string lastUpdate, string duration, int instructionID, string image, string language)
        {
            ID = iD;
            Title = title;
            URL = uRL;
            Rating = rating;
            Num_Reviews = num_Reviews;
            Num_Published = num_Published;
            Create = create;
            LastUpdate = lastUpdate;
            Duration = duration;
            InstructionID = instructionID;
            Image = image;
            Language = language;
        }

        public Course() {
            ID = 0;
            Title = string.Empty;
            URL = string.Empty;
            Rating = 0;
            Num_Reviews = 0;
            Num_Published = 0;
            Create = string.Empty;
            LastUpdate = string.Empty;
            Duration = string.Empty;
            InstructionID = 0;
            Image = string.Empty;
            Language = string.Empty;
        }
    }
}

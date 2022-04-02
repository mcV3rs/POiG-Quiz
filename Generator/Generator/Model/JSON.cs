using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Generator.Model
{
    internal class quizes
    {
        public List<Quiz> Quiz { get; set; }
    }

    public class Quiz
    {
        public Quiz()
        {
            Question = "";
            Answer_A = "";
            Answer_B = "";
            Answer_C = "";
            Correct_A = false;
            Correct_B = false;
            Correct_C = false;
            Correct_D = false;
        }

        public string Question { get; set; }

        public string Answer_A { get; set; }

        public string Answer_B { get; set; }

        public string Answer_C { get; set; }

        public string Answer_D { get; set; }

        public bool Correct_A { get; set; }

        public bool Correct_B { get; set; }

        public bool Correct_C { get; set; }

        public bool Correct_D { get; set; }
    }

}

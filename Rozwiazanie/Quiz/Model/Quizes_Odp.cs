using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Model
{
    internal class Quizes_Odp
    {
        public List<Quizes_Odp> Quiz { get; set; }
    }
    class Quize_Odp
    {
        public Quize_Odp()
        {
            Question = "";
            Answer_A = "";
            Answer_B = "";
            Answer_C = "";
            Answer_D = "";
            Correct_Answer = "";
            User_Answer = "";
        }

        public int ID { get; set; }

        public string Question { get; set; }

        public string Answer_A { get; set; }

        public string Answer_B { get; set; }

        public string Answer_C { get; set; }

        public string Answer_D { get; set; }

        public string Correct_Answer { get; set; }

        public string User_Answer { get; set; }

    }
    
}

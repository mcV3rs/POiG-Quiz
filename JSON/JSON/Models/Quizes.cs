using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quizes.Models
{
    internal class quizes
    {
        public List<Quiz> Quiz { get; set; }
    }

    public class Quiz
    {
        public int ID { get; set; }

        public string Question { get; set; }

        public string Answer_A { get; set; }

        public string Answer_B { get; set; }

        public string Answer_C { get; set; }

        public string Answer_D { get; set; }

        public int Correct_A { get; set; }

        public int Correct_B { get; set; }

        public int Correct_C { get; set; }

        public int Correct_D { get; set; }

        public int Count { get; set; }
    }
}

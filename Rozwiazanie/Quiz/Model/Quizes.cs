using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Model
{
    
         internal class quizes
        {
            public List<Quize> Quiz { get; set; }
        }

        public class Quize
        {
            public int ID { get; set; }

            public string Question { get; set; }

            public string AnswerA { get; set; }

            public string AnswerB { get; set; }

            public string AnswerC { get; set; }

            public string AnswerD { get; set; }

            public string Correct { get; set; }
        }
}

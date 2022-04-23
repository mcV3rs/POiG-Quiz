using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace Quiz.Model
{
    internal class quizes
    {
        public List<Quiz> Quiz { get; set; }
    }

    public class Quiz
    {
        public Quiz()
        {
            Random rnd = new Random();

            Question = "";
            Answer_A = "";
            Answer_B = "";
            Answer_C = "";
            Answer_D = "";
            Correct_A = rnd.Next(int.MaxValue / 2) * 2 + 1;
            Correct_B = rnd.Next(int.MaxValue / 2) * 2 + 1;
            Correct_C = rnd.Next(int.MaxValue / 2) * 2 + 1;
            Correct_D = rnd.Next(int.MaxValue / 2) * 2 + 1;
        }

        [JsonEncrypt]
        public string Question { get; set; }

        [JsonEncrypt]
        public string Answer_A { get; set; }

        [JsonEncrypt]
        public string Answer_B { get; set; }

        [JsonEncrypt]
        public string Answer_C { get; set; }

        [JsonEncrypt]
        public string Answer_D { get; set; }

        [JsonEncrypt]
        public int Correct_A { get; set; }

        [JsonEncrypt]
        public int Correct_B { get; set; }

        [JsonEncrypt]
        public int Correct_C { get; set; }

        [JsonEncrypt]
        public int Correct_D { get; set; }
    }

}

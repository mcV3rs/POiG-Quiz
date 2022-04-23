using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

using System.Security.Cryptography;

using System.IO;

namespace Quiz.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        /*public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((bool)value)
            {
                {
                    return new SolidColorBrush(Colors.Black);
                }
            }
            return new SolidColorBrush(Colors.LightGreen);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }*/
        public event PropertyChangedEventHandler PropertyChanged;
        private static bool isRun = false;
        private static bool isStop = false;
        private static bool isAnswer = false;
        private static bool isRunNext = false;
        private List<Model.Quiz> quizList = MainViewModel.deserialized();
        List<string> odp = new List<string>();
        private float right;
        private float all;
        private int i = 0;
        
        private DispatcherTimer dt;
        public MainViewModel()
        {
            dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromSeconds(1);
            dt.Tick += dtTicker;
        }

        private void dtTicker(object sender, EventArgs e)
        {
            increment++;
            Increment = increment;

        }
        private int increment = 0;
        public int Increment
        {
            get => increment;
            set
            {
                increment = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Increment)));
            }
        }
        public List<int> QuestionsCount
        {
            get => Enumerable.Range(1, quizList.Count).ToList();
        }
        private int l
        {
            get => quizList.Count;
            set
            {
                l = value;
            }
        }
        
        private int questionId = -1;
        public int QuestionId
        {
            get => questionId;
            set
            {
                questionId = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(QuestionId)));

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AnswerId)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Question)));

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AnswerA)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AnswerB)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AnswerC)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AnswerD)));

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CorrectA)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CorrectB)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CorrectC)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CorrectD)));
            }
        }

        public int AnswerId
        {
            get => questionId + 1;
        }
       
        public string Question
        {

            get
            {
                if (questionId == -1)
                {
                    return "Kliknij rozpocznij";
                }
                else if (questionId == l + 1)
                {
                    return "Kliknij zakończ";
                }
                else
                {
                    return quizList[questionId].Question;
                }

            }
            set
            {
                quizList[questionId].Question = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Question)));

            }
        }

        private string answerA
        {
            get
            {
                if (questionId == -1)
                {
                    return "";
                }
                else if (questionId == l + 1)
                {
                    return "";
                }
                else
                {
                    return quizList[questionId].Answer_A;
                }
            }

            set
            {
                answerA = value;
            }
        }
        
        public string AnswerA
        {
            get => answerA;
            set
            {
                answerA = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AnswerA)));
            }
        }

        public bool CorrectA
        {
            get => quizList[questionId].Correct_A % 2 == 0 ? true : false;
            set
            {
                Random rnd = new Random();

                quizList[questionId].Correct_A = value ? rnd.Next(int.MaxValue / 2) * 2 : rnd.Next(int.MaxValue / 2) * 2 + 1;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CorrectA)));
            }
        }
        private string answerB
        {
            get
            {
                if (questionId == -1)
                {
                    return "";
                }
                else if (questionId == l + 1)
                {
                    return "";
                }
                else
                {
                    return quizList[questionId].Answer_B;
                }
            }
            set
            {
                answerB = value;
            }
        }
        public string AnswerB
        {
            get => answerB;
            set
            {
                answerB = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AnswerB)));

            }
        }
        public bool CorrectB
        {
            get => quizList[questionId].Correct_B % 2 == 0 ? true : false;
            set
            {
                Random rnd = new Random();

                quizList[questionId].Correct_B = value ? rnd.Next(int.MaxValue / 2) * 2 : rnd.Next(int.MaxValue / 2) * 2 + 1;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CorrectB)));
            }
        }
        private string answerC
        {
            get
            {
                if (questionId == -1)
                {
                    return "";
                }
                else if (questionId == l + 1)
                {
                    return "";
                }
                else
                {
                    return quizList[questionId].Answer_C;
                }
            }
            set
            {
                answerC = value;
            }

        }
        public string AnswerC
        {
            get => answerC;

            set
            {
                answerC = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AnswerC)));

            }
        }
        public bool CorrectC
        {
            get => quizList[questionId].Correct_C % 2 == 0 ? true : false;
            set
            {
                Random rnd = new Random();

                quizList[questionId].Correct_C = value ? rnd.Next(int.MaxValue / 2) * 2 : rnd.Next(int.MaxValue / 2) * 2 + 1;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CorrectC)));
            }
        }
        private string answerD
        {
            get
            {
                if (questionId == -1)
                {
                    return "";
                }
                else if (questionId == l + 1)
                {
                    return "";
                }
                else
                {
                    return quizList[questionId].Answer_D;
                }
            }
            set
            {
                answerD = value;
            }

        }

        public string AnswerD
        {
            get => answerD;
            set
            {
                answerD = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AnswerD)));
            }
        }

        public bool CorrectD
        {
            get => quizList[questionId].Correct_D % 2 == 0 ? true : false;
            set
            {
                Random rnd = new Random();

                quizList[questionId].Correct_C = value ? rnd.Next(int.MaxValue / 2) * 2 : rnd.Next(int.MaxValue / 2) * 2 + 1;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CorrectD)));
            }
        }

        public static List<Model.Quiz> deserialized()
        {
            return JsonConvert.DeserializeObject<List<Model.Quiz>>(System.IO.File.ReadAllText(@"pytania.json"), new JsonSerializerSettings
            {
                ContractResolver = new EncryptedStringPropertyResolver("#my*S3cr3t")
            });
        }

        // Serializacja danych do pliku JSON
        private void serialized()
        {
            var jsonOutput = JsonConvert.SerializeObject(quizList, Formatting.Indented, new JsonSerializerSettings
            {
                ContractResolver = new EncryptedStringPropertyResolver("#my*S3cr3t")
            });

            System.IO.File.WriteAllText(@"out.json", jsonOutput);
        }

        private void czyTrueA()
        {
            if (CorrectA == true)
            {
                right += 1;
            }

        }
        private void czyTrueB()
        {
            if (CorrectB == true)
            {
                right += 1;

            }

        }
        private void czyTrueC()
        {
            if (CorrectC == true)
            {
                right += 1;

            }
        }
        private void czyTrueD()
        {
            if (CorrectD == true)
            {
                right += 1;
            }

        }

        private ICommand answerAA;
        public ICommand AnswerAA
        {
            get
            {
                if (answerAA == null)
                    answerAA = new RelayCommand(
                        (o) =>
                        {

                            isRun = true;
                            isStop = false;
                            isRunNext = true;
                            isAnswer = false;
                            czyTrueA();
                            if( i == 0)
                                odp.Add(answerA);
                        },
                        (o) => isAnswer
                        );
                return answerAA;
            }
        }

        private ICommand answerBB;
        public ICommand AnswerBB
        {
            get
            {
                if (answerBB == null)
                    answerBB = new RelayCommand(
                        (o) =>
                        {
                            isRun = true;
                            isStop = false;
                            isRunNext = true;
                            isAnswer = false;
                            czyTrueB();
                            if (i == 0)
                                odp.Add(answerB);
                        },
                        (o) => isAnswer
                        );
                return answerBB;
            }
        }
        private ICommand answerCC;
        public ICommand AnswerCC
        {
            get
            {
                if (answerCC == null)
                    answerCC = new RelayCommand(
                        (o) =>
                        {
                            isRun = true;
                            isStop = false;
                            isRunNext = true;
                            isAnswer = false;
                            czyTrueC();
                            if (i == 0)
                                odp.Add(answerC);

                        },
                        (o) => isAnswer
                        );
                return answerCC;
            }
        }
        private ICommand answerDD;
        public ICommand AnswerDD
        {
            get
            {
                if (answerDD == null)
                    answerDD = new RelayCommand(
                        (o) =>
                        {
                            isRun = true;
                            isStop = false;
                            isRunNext = true;
                            isAnswer = false;
                            czyTrueD();
                            if (i == 0)
                                odp.Add(answerD);
                          
                        },
                        (o) => isAnswer
                        );
                return answerDD;
            }
        }
   
        private void repeat()
        {

            QuestionId = 0;
            
            for (int i = 0; i < l; i++)
            {
                QuestionId = i;
                isRunNext = true;
                if(odp[i] == answerA)
                {
                    if (quizList[i].Correct_A.Equals(true))
                    {
                        MessageBox.Show("Odpowiedź A to dobra odpowiedź", "Poprawna odpowiedź");
                    }
                    else
                    {
                        if (quizList[i].Correct_B.Equals(true))
                        {
                            MessageBox.Show("Odpowiedz B to dobra odpowiedź ", "Odpowiedź A to zła odpowiedź!");
                        }
                        else if (quizList[i].Correct_B.Equals(true) & quizList[i].Correct_C.Equals(true))
                        {
                            MessageBox.Show("Odpowiedz B i C to dobra odpowiedź ", "Odpowiedź A to zła odpowiedź!");
                        }
                       else if (quizList[i].Correct_B.Equals(true) & quizList[i].Correct_D.Equals(true))
                        {
                            MessageBox.Show("Odpowiedz B i D to dobra odpowiedź ", "Odpowiedź A to zła odpowiedź!");
                        }
                        else if (quizList[i].Correct_B.Equals(true) & quizList[i].Correct_C.Equals(true) & quizList[i].Correct_D.Equals(true))
                        {
                            MessageBox.Show("Odpowiedz B i C i D to dobra odpowiedź ", "Odpowiedź A to zła odpowiedź!");
                        }
                       else  if (quizList[i].Correct_C.Equals(true) & quizList[i].Correct_D.Equals(true))
                        {
                            MessageBox.Show("Odpowiedz C i D to dobra odpowiedź", "Odpowiedź A to zła odpowiedź!");
                        }
                        else if (quizList[i].Correct_D.Equals(true))
                        {
                            MessageBox.Show("Odpowiedz D to dobra odpowiedź", "Odpowiedź A to zła odpowiedź!");
                        }
                        else if (quizList[i].Correct_C.Equals(true))
                        {
                            MessageBox.Show("Odpowiedz C to dobra odpowiedź", "Odpowiedź A to zła odpowiedź!");
                        }
                    } 
                }
                else if (odp[i] == answerB)
                {
                    if (quizList[i].Correct_B.Equals(true))
                    {
                        MessageBox.Show("Odpowiedź B to dobra odpowiedź", "Poprawna odpowiedź");
                    }
                    else
                    {
                        if (quizList[i].Correct_A.Equals(true) & quizList[i].Correct_C.Equals(true) & quizList[i].Correct_D.Equals(true))
                        {
                            MessageBox.Show("Odpowiedz A i C i D to dobra odpowiedź ", "Odpowiedź B to zła odpowiedź!");
                        }
                        else if (quizList[i].Correct_A.Equals(true) & quizList[i].Correct_C.Equals(true))
                        {
                            MessageBox.Show("Odpowiedz A i C to dobra odpowiedź ", "Odpowiedź B to zła odpowiedź!");
                        }
                        else if (quizList[i].Correct_A.Equals(true) & quizList[i].Correct_D.Equals(true))
                        {
                            MessageBox.Show("Odpowiedz A i D to dobra odpowiedź ", "Odpowiedź B to zła odpowiedź!");
                        }
                        else if (quizList[i].Correct_C.Equals(true) & quizList[i].Correct_D.Equals(true))
                        {
                            MessageBox.Show("Odpowiedz C i D to dobra odpowiedź", "Odpowiedź B to zła odpowiedź!");
                        }
                        else if (quizList[i].Correct_A.Equals(true))
                        {
                            MessageBox.Show("Odpowiedz A to dobra odpowiedź ", "Odpowiedź B to zła odpowiedź!");
                        }
                        else if (quizList[i].Correct_C.Equals(true))
                        {
                            MessageBox.Show("Odpowiedz C to dobra odpowiedź ", "Odpowiedź B to zła odpowiedź!");
                        }
                        else if (quizList[i].Correct_D.Equals(true))
                        {
                            MessageBox.Show("Odpowiedz D to dobra odpowiedź", "Odpowiedź B to zła odpowiedź!");
                        }
                    }
                }
                else if (odp[i] == answerC)
                {
                    if (quizList[i].Correct_C.Equals(true))
                    {
                        MessageBox.Show("Odpowiedź C to dobra odpowiedź", "Poprawna odpowiedź");
                    }
                    else
                    {
                        if (quizList[i].Correct_A.Equals(true) & quizList[i].Correct_B.Equals(true))
                        {
                            MessageBox.Show("Odpowiedz A i B to dobra odpowiedź ", "Odpowiedź C to zła odpowiedź!");
                        }
                        else if (quizList[i].Correct_A.Equals(true) & quizList[i].Correct_D.Equals(true))
                        {
                            MessageBox.Show("Odpowiedz A i D to dobra odpowiedź ", "Odpowiedź C to zła odpowiedź!");
                        }
                        else if (quizList[i].Correct_A.Equals(true) & quizList[i].Correct_B.Equals(true) & quizList[i].Correct_D.Equals(true))
                        {
                            MessageBox.Show("Odpowiedz A i B i D to dobra odpowiedź ", "Odpowiedź C to zła odpowiedź!");
                        }
                        else if (quizList[i].Correct_B.Equals(true) & quizList[i].Correct_D.Equals(true))
                        {
                            MessageBox.Show("Odpowiedz B i D  to dobra odpowiedź", "Odpowiedź C to zła odpowiedź!");
                        }
                        else if (quizList[i].Correct_A.Equals(true))
                        {
                            MessageBox.Show("Odpowiedz A to dobra odpowiedź", "Odpowiedź C to zła odpowiedź!");
                        }
                        else if (quizList[i].Correct_B.Equals(true))
                        {
                            MessageBox.Show("Odpowiedz B to dobra odpowiedź", "Odpowiedź C to zła odpowiedź!");
                        }
                        else if (quizList[i].Correct_D.Equals(true))
                        {
                            MessageBox.Show("Odpowiedz D to dobra odpowiedź", "Odpowiedź C to zła odpowiedź!");
                        }
                    }
                }
                else if (odp[i] == answerD)
                {
                    if (quizList[i].Correct_D.Equals(true))
                    {
                        MessageBox.Show("Odpowiedź D to dobra odpowiedź", "Poprawna odpowiedź");
                    }
                    else
                    {
                        if (quizList[i].Correct_A.Equals(true) & quizList[i].Correct_B.Equals(true))
                        {
                            MessageBox.Show("Odpowiedz A i B to dobra odpowiedź ", "Odpowiedź D to zła odpowiedź!");
                        }
                        else if (quizList[i].Correct_A.Equals(true) & quizList[i].Correct_C.Equals(true))
                        {
                            MessageBox.Show("Odpowiedz A i C to dobra odpowiedź ", "Odpowiedź D to zła odpowiedź!");
                        }
                        else if (quizList[i].Correct_A.Equals(true) & quizList[i].Correct_B.Equals(true) & quizList[i].Correct_C.Equals(true))
                        {
                            MessageBox.Show("Odpowiedz A i B i C to dobra odpowiedź ", "Odpowiedź D to zła odpowiedź!");
                        }
                        else if (quizList[i].Correct_C.Equals(true) & quizList[i].Correct_B.Equals(true))
                        {
                            MessageBox.Show("Odpowiedz C i B to dobra odpowiedź", "Odpowiedź D to zła odpowiedź!");
                        }
                        else if (quizList[i].Correct_B.Equals(true))
                        {
                            MessageBox.Show("Odpowiedz B to dobra odpowiedź", "Odpowiedź D to zła odpowiedź!");
                        }
                        else if (quizList[i].Correct_A.Equals(true))
                        {
                            MessageBox.Show("Odpowiedz A to dobra odpowiedź", "Odpowiedź D to zła odpowiedź!");
                        }
                        else if (quizList[i].Correct_C.Equals(true))
                        {
                            MessageBox.Show("Odpowiedz C to dobra odpowiedź", "Odpowiedź D to zła odpowiedź!");
                        }
                    }
                }   
            }
        }
        
        private void statystyka()
        {
            double wynik = right / all;
            double procent = wynik * 100;
            string accuray = procent.ToString() + "%";
            dt.Stop();
            MessageBox.Show(right.ToString() + "/" + all.ToString() + " " + accuray, "Podsumowanie quizu");
        }
        private ICommand next;
        public ICommand Next
        {
            get
            {
                if (next == null)
                    next = new RelayCommand(
                        (o) =>
                        {
                            if (AnswerId == l)
                            {
                                all += 1;
                                statystyka();
                                isRun = true;
                                i = 1;
                                repeat();
                                isRunNext = false;
                                isStop = true;
                                isAnswer = false;
                                QuestionId = l+1;
                                increment = 0;
                            }
                            else
                            {
                               
                                QuestionId += 1;
                                isRun = true;
                                isRunNext = false;
                                isAnswer = true;
                                isStop = false;
                                all += 1;
                            }
                        }
                        ,
                        (o) => isRunNext

                        );
                return next;
            }
        }
        private ICommand run;
        public ICommand Run
        {
            get
            {
                if (run == null)
                    run = new RelayCommand(
                        (o) =>
                        {
                            dt.Start();
                            QuestionId = 0;
                            isRunNext = !isRunNext;
                            isRun = true;
                            isStop = false;
                            isAnswer = true;
                        }
                        ,
                        (o) => !isRun

                        ); 
                return run;
            }
        }



        private ICommand stop;
        public ICommand Stop
        {
            get
            {
                if (stop == null)
                    stop = new RelayCommand(
                        (o) =>
                        {
                            dt.Stop();
                            increment = 0;
                            isRun = false;
                            QuestionId = -1;
                            isStop = false;
                            right = 0;
                            all = 0;
                            isRunNext = false;
                            i = 0;
                            odp.Clear();
                        }
                        ,
                        (o) => isStop
                        );
                return stop;
            }
        }

    } 
}



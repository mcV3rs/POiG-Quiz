using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Input;
using System.Timers;

using Newtonsoft.Json;

namespace Generator.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // Lista Pytań
        private List<Model.Quiz> quizList = MainViewModel.deserialized();

        // Aktualny numer pytania
        private int questionId = 0;

        public List<int> QuestionsCount
        {
            get => Enumerable.Range(1, quizList.Count).ToList(); 
        }

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
            get => quizList[questionId].Question;
            set
            {
                quizList[questionId].Question = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Question)));
            }
        }

        public string AnswerA
        {
            get => quizList[questionId].Answer_A;
            set
            {
                quizList[questionId].Answer_A = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AnswerA)));
            }
        }

        public string AnswerB
        {
            get => quizList[questionId].Answer_B;
            set
            {
                quizList[questionId].Answer_B = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AnswerB)));
            }
        }

        public string AnswerC
        {
            get => quizList[questionId].Answer_C;
            set
            {
                quizList[questionId].Answer_C = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AnswerC)));
            }
        }

        public string AnswerD
        {
            get => quizList[questionId].Answer_D;
            set
            {
                quizList[questionId].Answer_D = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AnswerD)));
            }
        }

        public bool CorrectA
        {
            get => quizList[questionId].Correct_A;
            set
            {
                quizList[questionId].Correct_A = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CorrectA)));
            }
        }

        public bool CorrectB
        {
            get => quizList[questionId].Correct_B;
            set
            {
                quizList[questionId].Correct_B = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CorrectB)));
            }
        }

        public bool CorrectC
        {
            get => quizList[questionId].Correct_C;
            set
            {
                quizList[questionId].Correct_C = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CorrectC)));
            }
        }

        public bool CorrectD
        {
            get => quizList[questionId].Correct_D;
            set
            {
                quizList[questionId].Correct_D = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CorrectD)));
            }
        }

        private ICommand saveButton;
        public ICommand SaveButton
        {
            get
            {
                if (saveButton == null)
                    saveButton = new RelayCommand(
                        (o) => serialized(),
                        (o) => true
                    );
                return saveButton;
            }
        }

        private ICommand delButton;
        public ICommand DelButton
        {
            get
            {
                if (delButton == null)
                    delButton = new RelayCommand(
                        (o) => del(questionId),
                        (o) => true
                    );
                return delButton;
            }
        }

        private ICommand addButton;
        public ICommand AddButton
        {
            get
            {
                if (addButton == null)
                    addButton = new RelayCommand(
                        (o) => add(),
                        (o) => true
                    );
                return addButton;
            }
        }

        public static List<Model.Quiz> deserialized()
        {
            return JsonConvert.DeserializeObject<List<Model.Quiz>>(System.IO.File.ReadAllText(@"quiz1.json"));
        }

        private void serialized()
        {
            System.IO.File.WriteAllText(@"out.json", JsonConvert.SerializeObject(quizList));
        }

        private void del(int index)
        {
            quizList.RemoveAt(index);
            questionId = 0;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(QuestionId)));
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(QuestionsCount)));
        }

        private void add()
        {
            Model.Quiz quiz = new Model.Quiz();

            quizList.Add(quiz);
            questionId = quizList.Count - 1;

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(QuestionsCount)));
        }
    }
}



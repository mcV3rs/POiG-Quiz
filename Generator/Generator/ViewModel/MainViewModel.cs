using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Windows.Input;

using Newtonsoft.Json;
using System.IO;

namespace Generator.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        // Lista Pytań
        private List<Model.Quiz> quizList;

        // Aktualny ID pytania
        private int questionId = 0;

        public MainViewModel()
        {
            if (!File.Exists(@"quiz1.json") || new FileInfo(@"quiz1.json").Length == 0)
            {
                List<Model.Quiz> empty = new List<Model.Quiz>
                {
                    new Model.Quiz()
                };

                var jsonOutput = JsonConvert.SerializeObject(empty, Formatting.Indented, new JsonSerializerSettings
                {
                    ContractResolver = new EncryptedStringPropertyResolver("#my*S3cr3t")
                });

                File.WriteAllText(@"quiz1.json", jsonOutput);
            }

            quizList = deserialized();
        }

        // Zwrócenie listy numerów pytań
        public List<int> QuestionsCount
        {
            get => Enumerable.Range(1, quizList.Count).ToList(); 
        }

        // Zarządzanie zmianą ID pytania
        public int QuestionId
        {
            get => questionId;
            set
            {
                questionId = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(QuestionId)));

                // Wywołanie zmiany numeru pytania i treści pytania
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AnswerId)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Question)));

                // Wywołanie zmiany treści odpowiedzi
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AnswerA)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AnswerB)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AnswerC)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AnswerD)));

                // Wywołanie zmiany poprawności odpowiedzi
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CorrectA)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CorrectB)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CorrectC)));
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CorrectD)));
            }
        }

        // Zarządzanie numerem pytania 
        public int AnswerId 
        {
            get => questionId + 1;
        }

        // Zarządzanie treścią pytania
        public string Question
        {
            get => quizList[questionId].Question;
            set
            {
                quizList[questionId].Question = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Question)));
            }
        }

        // Zarządzanie odpowiedziami A - B
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

        // Zarządzanie poprawnością A - B
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

        public bool CorrectD
        {
            get => quizList[questionId].Correct_D % 2 == 0 ? true : false;
            set
            {
                Random rnd = new Random();

                quizList[questionId].Correct_D = value ? rnd.Next(int.MaxValue / 2) * 2 : rnd.Next(int.MaxValue / 2) * 2 + 1;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CorrectD)));
            }
        }

        // Zapisywanie stanu quizu
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

        // Usuwanie pytania
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

        // Dodawanie pustego pytania
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

        // Deserializacja danych z pliku JSON
        public static List<Model.Quiz> deserialized()
        {
            return JsonConvert.DeserializeObject<List<Model.Quiz>>(File.ReadAllText(@"quiz1.json"), new JsonSerializerSettings
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

            File.WriteAllText(@"quiz1.json", jsonOutput);
        }

        // Obsługa usuwania pytania
        private void del(int index)
        {
            if(quizList.Count == 1)
            {
                Model.Quiz quiz = new Model.Quiz();
                quizList.Add(quiz);
            }
            
            quizList.RemoveAt(index);

            questionId = index == 0 ? 0 : index - 1;

            globalChanged();
        }

        // Obsługa dodawania pytania
        private void add()
        {
            Model.Quiz quiz = new Model.Quiz();

            quizList.Add(quiz);
            questionId = quizList.Count - 1;

            globalChanged();
        }

        // Globalna emisja zmiany własności
        private void globalChanged()
        {
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(QuestionId)));

            // Wywołanie zmiany numeru pytania i treści pytania
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(AnswerId)));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(Question)));

            // Wywołanie zmiany treści odpowiedzi
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(AnswerA)));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(AnswerB)));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(AnswerC)));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(AnswerD)));

            // Wywołanie zmiany poprawności odpowiedzi
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(CorrectA)));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(CorrectB)));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(CorrectC)));
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(CorrectD)));

            // Wywołanie zmiany listy pytań
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(nameof(QuestionsCount)));
        }
    }
}



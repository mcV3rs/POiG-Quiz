using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace Quiz.ViewModel
{
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private Model.Quize model = new Model.Quize();
        private static bool isRun = false;
        private static bool isRunAnswers = false;
        private static Timer timer;


        private string answerA;
        public string AnswerA
        {
            get => answerA;
            set
            {
                answerA = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AnswerA)));
  
            }
        }
        private string answerB;
        public string AnswerB
        {
            get => answerB;
            set
            {
                answerB = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AnswerB)));
            }
        }

        private string answerC;
        public string AnswerC
        {
            get => answerC;
            set
            {
                answerC = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AnswerC)));
            }
        }
        private string answerD;
        public string AnswerD
        {
            get => answerD;
            set
            {
                answerD = value;
                
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(AnswerD)));
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
                            timer?.Start();
                            isRun = !isRun;
                           
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
                            timer?.Stop();
                            isRun = !isRun;
                            
                        }
                        ,
                        (o) => isRun
                        );
                return stop;
            }
        }
        
    } 
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessageType
{
    [Serializable]
    public class Game_Question
    {
        public string User { get; set; }
        public string Opponent { get; set; }
        public string Question { get; set; }
        public string AnswerA { get; set; }
        public string AnswerB { get; set; }
        public string Correct { get; set; }

        public Game_Question(string user, string opponent, string question, string answerA, string answerB, string correct)
        {
            this.User = user;
            this.Opponent = opponent;
            this.Question = question;
            this.AnswerA = answerA;
            this.AnswerB = answerB;
            this.Correct = correct;
        }
    }
}

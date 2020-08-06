using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Net.Sockets;
using MessageType;
using System.Threading;
using System.ComponentModel;

namespace Client
{
    public partial class Game : Form
    {
        private bool check = true;
        private List<string> steps = new List<string>();
        private List<Button> button_Groups = new List<Button>();
        private string you;
        private string opponent;
        private bool first;
        private string correct;
        NetworkStream stream;
        IFormatter formatter = new BinaryFormatter();
        object obj;

        public Game(NetworkStream stream, string you, string first, string second)
        {
            InitializeComponent();

            this.stream = stream;
            if (first == you)
            {
                this.you = first;
                this.first = true;
                opponent = second;
            }
            else if (first != you)
            {
                this.first = false;
                this.you = second;
                opponent = first;
            }
            Reload();

            label_Your_Name.Text = this.you;
            label_Opponents_Name.Text = this.opponent;
        }

        private void Reload()
        {
            steps.Clear();
            button_Groups.Clear();
            panel_Boxes_Area.Controls.Clear();
            panel_Boxes_Area.BackColor = Color.White;
            int W = 70;
            int H = 70;
            int MARGIN = 5;
            for (int i = 0; i < 3; i++)
            {
                for (int y = 0; y < 3; y++)
                {
                    Button button = new Button();
                    button.Size = new Size(W, H);
                    button.Location = new Point((i * W + MARGIN), (y * H + (y + 1) * MARGIN));
                    button.Click += Button_Click;
                    button.Tag = i + "," + y;
                    panel_Boxes_Area.Controls.Add(button);
                    button_Groups.Add(button);
                }
            }

            Enable_Box_Area(false);
            Display_Q_Area(false);
            Display_A_Area(false);
            Enable_Start_Button(true);
            Display_Q_A_Area(false);
        }
        private void Button_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            steps.Add(button.Tag.ToString());
            Update_Steps(label_Your_Steps_Value, steps.Count.ToString());
            button.Text = "O";
            button.ForeColor = Color.Red;
            button.Enabled = false;
            Enable_Box_Area(false);
            formatter.Serialize(stream, new Game_Turn_Finish(you, opponent, button.Tag.ToString(), steps));

            bool done = false;
            while (!done)
            {
                if (!(obj is GetWinner) && obj != null)
                {
                    Asking();
                    done = true;
                }
                else if (obj is GetWinner)
                {
                    done = true;
                }
                Thread.Sleep(1000);
            }
        }

        private void button_Start_Game_Click(object sender, EventArgs e)
        {
            formatter.Serialize(stream, new Game_Ready(you, true));
            Enable_Start_Button(false);
            Enable_Form(false);
            Thread t = new Thread(new ThreadStart(MessageHandler));
            t.Start();

            bool done = false;
            while (!done)
            {
                if (!(obj is Game_Start) && obj != null)
                {
                    MessageBox.Show("Awaiting your opponent gets ready...");
                    done = true;
                }
                else if (obj is Game_Start)
                {
                    done = true;
                }
                Thread.Sleep(1000);
            }
            check = true;
        }

        private delegate void Enable_Start_Button_DEL(bool yes);
        private void Enable_Start_Button(bool yes)
        {
            if (InvokeRequired)
            {
                Enable_Start_Button_DEL del = Enable_Start_Button;
                button_Start_Game.Invoke(del, yes);
            }
            button_Start_Game.Enabled = yes;
        }

        private void button_Quit_Game_Click(object sender, EventArgs e)
        {
            formatter.Serialize(stream, new Game_Quit(you, opponent));
            check = false;
            this.Close();
        }

        private delegate void Update_Selected_Box_DEL(Button button, string value);
        private void Update_Selected_Box(Button button, string value)
        {
            if (InvokeRequired)
            {
                Update_Selected_Box_DEL del = Update_Selected_Box;
                button.Invoke(del,button,value);
            }
            else
            {

                button.Text = value;
                button.ForeColor = Color.Black;
                button.Enabled = false;
            }
        }
        private void MessageHandler()
        {
            while (check)
            {
                obj = formatter.Deserialize(stream);
                if (obj is Game_Start)
                {
                    if (first)
                    {
                        Enable_Form(true);
                        Asking();
                    }
                    else if (!first)
                    {
                        MessageBox.Show("Waiting for your opponent question...");
                        Display_A_Area(false);
                        Display_Q_Area(false);
                        Enable_Box_Area(false);
                        Display_Q_A_Area(false);
                    }
                }
                else if (obj is Game_Question)
                {
                    Answer_Question();
                    Game_Question game_Question = obj as Game_Question;
                    SetQuestion(game_Question.Question);
                    SetAnswerA(game_Question.AnswerA);
                    SetAnswerB(game_Question.AnswerB);
                    correct = game_Question.Correct;
                }
                else if (obj is Game_Turn_Finish)
                {
                    Game_Turn_Finish game_Turn_Finish = obj as Game_Turn_Finish;
                    Update_Steps(label_Opponent_Steps_Value, game_Turn_Finish.Steps.Count.ToString());
                    if (game_Turn_Finish.Movement == null)
                    {
                        MessageBox.Show("Your opponent failed in your question...\nWaiting for your opponent question...");
                    }
                    else if (game_Turn_Finish.Movement != null)
                    {
                        foreach (Button button in button_Groups)
                        {
                            if (button.Tag.ToString() == game_Turn_Finish.Movement)
                            {
                                Update_Selected_Box(button, "X");
                            }
                        }
                        MessageBox.Show("Your opponent gets a chance for move...\nWaiting for your opponent question...");

                    }
                }
                else if (obj is GetWinner)
                {
                    GetWinner winner = obj as GetWinner;
                    if (winner.Winner == this.you)
                    {
                        MessageBox.Show("You win...");
                    }
                    else
                    {
                        MessageBox.Show("Your opponent win...");
                    }
                    button_Quit_Game.PerformClick();
                }
                else if (obj is Game_Quit)
                {
                    MessageBox.Show("Your opponent quits this game...");
                    check = false;
                    this.Close();
                    //Form_Close();
                }
            }

        }

        private delegate void Form_Close_del();
        private void Form_Close()
        {
            if (InvokeRequired)
            {
                Form_Close_del del = Form_Close;
                this.Invoke(del);
            }
            else
            {
                this.Close();//?????
            }
        }

        delegate void Update_Steps_del(Label label, string value);
        private void Update_Steps(Label label, string value)
        {
            if (InvokeRequired)
            {
                Update_Steps_del del = Update_Steps;
                label.Invoke(del, label, value);
            }
            else
            {
                label.Text = value;
            }
        }
        private void Clear_Question()
        {
            textBox_Question_input.Text = "";
            textBox_Question_A_Input.Text = "";
            textBox_Question_B_Input.Text = "";
            textBox_Question_Correct_Input.Text = "";
        }

        private delegate void SetQuestionDEL(string question);
        private void SetQuestion(string question)
        {
            if (InvokeRequired)
            {
                SetQuestionDEL del = SetQuestion;
                label_Question_To_Answer.Invoke(del, question);
            }
            else
            {
                label_Question_To_Answer.Text = question;
            }
        }
        private delegate void SetAnswerA_DEL(string AnswerA);
        private void SetAnswerA(string AnswerA)
        {
            if (InvokeRequired)
            {
                SetAnswerA_DEL del = SetAnswerA;
                button_Answer_Question_A.Invoke(del, AnswerA);
            }
            else
            {
                button_Answer_Question_A.Text = AnswerA;
            }
        }
        private delegate void SetAnswerB_DEL(string AnswerB);
        private void SetAnswerB(string AnswerB)
        {

            if (InvokeRequired)
            {
                SetAnswerB_DEL del = SetAnswerB;
                button_Answer_Question_B.Invoke(del, AnswerB);
            }
            else
            {
                button_Answer_Question_B.Text = AnswerB;
            }
        }
        private void Asking()
        {
            MessageBox.Show("Your turn to set up a question.\nYour opponent will get a move if his/her answer is correct...");
            Enable_Form(true);
            Display_Q_Area(true);
            Display_A_Area(false);
            Display_Q_A_Area(true);
            Enable_Box_Area(false);
            Clear_Question();
        }
        private void Answer_Question()
        {
            Enable_Form(true);
            Display_Q_A_Area(true);
            Display_Q_Area(false);
            Display_A_Area(true);
            Enable_Box_Area(true);
        }

        private void button_quetions_submit_Click(object sender, EventArgs e)
        {
            string question = textBox_Question_input.Text;
            string answerA = textBox_Question_A_Input.Text;
            string answerB = textBox_Question_B_Input.Text;
            string correct = textBox_Question_Correct_Input.Text;
            formatter.Serialize(stream, new Game_Question(you, opponent, question, answerA, answerB, correct));
            Display_A_Area(false);
            Display_Q_Area(false);
            Display_Q_A_Area(false);
            Enable_Form(false);
        }

        string answers_of_the_Question = null;

        private void button_Answer_Question_B_Click(object sender, EventArgs e)
        {
            answers_of_the_Question = (sender as Button).Text;
        }

        private void button_Answer_Question_A_Click(object sender, EventArgs e)
        {
            answers_of_the_Question = (sender as Button).Text;
        }

        private void button_Answer_Submit_Click(object sender, EventArgs e)
        {
            if (answers_of_the_Question == correct)
            {
                MessageBox.Show("Correct, you get a change to move...");
                Enable_Box_Area(true);
                Display_Q_Area(false);
                Display_A_Area(false);
                Display_Q_A_Area(false);
            }
            else if (answers_of_the_Question != correct)
            {
                MessageBox.Show("Incorrect, you missed your chance...\nSet a question for you opponent...");
                formatter.Serialize(stream, new Game_Turn_Finish(you, opponent, null, steps));
                Asking();
            }
        }


        private delegate void Enable_FormDEL(bool yes);
        public void Enable_Form(bool yes)
        {
            if (InvokeRequired)
            {
                Enable_FormDEL del = Enable_Form;
                panel_parent.Invoke(del, yes);
            }
            else
            {
                panel_parent.Enabled = yes;
            }
        }

        private delegate void Enable_Box_AreaDEL(bool yes);
        private void Enable_Box_Area(bool yes)
        {
            if (InvokeRequired)
            {
                Enable_Box_AreaDEL del = Enable_Box_Area;
                panel_Boxes_Area.Invoke(del, yes);
            }
            else
            {
                panel_Boxes_Area.Enabled = yes;
            }
        }
        private delegate void Display_Q_A_AreaDEL(bool question);
        private void Display_Q_A_Area(bool yes)
        {
            if (InvokeRequired)
            {
                Display_Q_A_AreaDEL del = Display_Q_A_Area;
                panel_Q_A.Invoke(del, yes);
            }
            else
            {
                panel_Q_A.Enabled = yes;
                panel_Q_A.Visible = yes;
            }
        }

        private delegate void Display_Q_AreaDEL(bool question);
        private void Display_Q_Area(bool question)
        {
            if (InvokeRequired)
            {
                Display_Q_AreaDEL del = Display_Q_Area;
                panel_Quetion_Area_Posting.Invoke(del, question);
            }
            else
            {
                panel_Quetion_Area_Posting.Enabled = question;
                panel_Quetion_Area_Posting.Visible = question;
            }
        }
        private delegate void Display_A_AreaDEL(bool answer);
        private void Display_A_Area(bool answer)
        {
            if (InvokeRequired)
            {
                Display_A_AreaDEL del = Display_A_Area;
                panel_Quetion_Area_Answer.Invoke(del, answer);
            }
            else
            {
                panel_Quetion_Area_Answer.Enabled = answer;
                panel_Quetion_Area_Answer.Visible = answer;
            }
        }

        private void Game_Load(object sender, EventArgs e)
        {
            this.FormClosing += new FormClosingEventHandler(MainForm_Closing);
        }

        private void MainForm_Closing(object sender, CancelEventArgs e)
        {
            formatter.Serialize(stream, new Game_Quit(you, opponent));
            check = false;
        }
    }
}

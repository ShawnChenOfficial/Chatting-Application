using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using MessageType;

namespace Server
{
    class MessageHandler
    {
        private TcpClient Client { get; set; }
        private Form1 Form { get; set; }

        private NetworkStream stream;
        private IFormatter formatter = new BinaryFormatter();
        private string sql;
        private static string connString = "server=127.0.0.1;database=chatlifedb;Uid=root;Pwd=";
        private MySqlConnection con = new MySqlConnection(connString);
        private MySqlCommand comd = new MySqlCommand();
        public bool check;
        public MessageHandler(Form1 form, TcpClient client)
        {
            this.Form = form;
            this.Client = client;
        }

        public void run()
        {
            check = true;
            stream = Client.GetStream();

            while (check)
            {
                object obj = formatter.Deserialize(stream);
                if (obj is LogIn)
                {
                    LogIn logIn = obj as LogIn;

                    if (Form.exists_Users.Contains(logIn))
                    {
                        formatter.Serialize(stream, new LogIn_Result(true, logIn));
                        Form.DisplayMessage("Account: " + logIn.Account + " loged in successful from " + Client.Client.RemoteEndPoint + " , at: " + DateTime.Now + "\n");
                        Form.online_Users[logIn.Account] = this;
                        Form.user_Availability.Add(logIn.Account, new User_Availability(logIn.Account, User_Status.Free));
                        Form.DisplayNumberOfOnlineUsers();
                    }
                    else
                    {
                        formatter.Serialize(stream, new LogIn_Result(false, logIn));
                        Form.DisplayMessage("Account: " + logIn.Account + " loged in failed from " + Client.Client.RemoteEndPoint + " , at: " + DateTime.Now + "\n");
                    }
                }
                else if (obj is SignUp)
                {

                    SignUp signUp = obj as SignUp;

                    if (AccountIsExisting(signUp))
                    {
                        formatter.Serialize(stream, new SignUp_Result(false, signUp));
                    }
                    else
                    {
                        con.Open();

                        sql = "insert into userpassword (PasswordID, usrAccount, usrPassword)"
                            + "values (@PasswordID,@usrAccount,@usrPassword)";
                        comd = new MySqlCommand(sql, con);
                        comd.Parameters.AddWithValue("PasswordID", Form.exists_Users.Count + 1);
                        comd.Parameters.AddWithValue("usrAccount", signUp.NewAccount);
                        comd.Parameters.AddWithValue("usrPassword", signUp.NewPassword);
                        comd.ExecuteNonQuery();

                        con.Close();

                        Form.LoadUsers();
                        Form.DisplayNumberOfOnlineUsers();
                        formatter.Serialize(stream, new SignUp_Result(true, signUp));
                    }
                }
                else if (obj is AddChat)
                {
                    AddChat addChat = obj as AddChat;

                    if (Form.online_Users.ContainsKey(addChat.TargetAccount))
                    {
                        formatter.Serialize(stream, new AddChat_Result(true, addChat));
                    }
                    else
                    {
                        formatter.Serialize(stream, new AddChat_Result(false, addChat));
                    }
                }
                else if (obj is Quit)
                {
                    Quit quit = obj as Quit;
                    if (quit.Account != "" && quit.Account != null)
                    {
                        Form.online_Users.Remove(quit.Account);
                        Form.user_Availability.Remove(quit.Account);
                        Form.DisplayMessage("Account :" + quit.Account + " was disconnected from" + Client.Client.RemoteEndPoint + ", at " + DateTime.Now + "\n");
                        check = false;
                        Form.DisplayNumberOfOnlineUsers();
                    }
                    else
                    {
                        Form.DisplayMessage("Unknown User was disconnected from" + Client.Client.RemoteEndPoint + ", at " + DateTime.Now + "\n");
                        check = false;
                        Form.DisplayNumberOfOnlineUsers();
                    }
                }
                else if (obj is Message)
                {
                    Message message = obj as Message;

                    if (Form.online_Users.ContainsKey(message.AccountTo))
                    {
                        Form.online_Users[message.AccountTo].SendMessage(message);//????
                        formatter.Serialize(stream, new Message_Sent_Result(true, ""));
                    }
                    else
                    {
                        formatter.Serialize(stream, new Message_Sent_Result(false, "You can't send a message to a offline user."));
                    }
                }
                else if (obj is Game_Invitation)
                {
                    Game_Invitation game_Invitation = obj as Game_Invitation;
                    if (Form.online_Users.ContainsKey(game_Invitation.Invitation_Reveiver)
                        &&
                        Form.user_Availability.ContainsKey(game_Invitation.Invitation_Reveiver))
                    {
                        if (Form.user_Availability[game_Invitation.Invitation_Reveiver].user_Status == User_Status.Free)
                        {
                            Form.user_Availability[game_Invitation.Inviter].user_Status = User_Status.Inviting;
                            formatter.Serialize(stream, new Game_Invitation_Result(true, ""));
                            Form.online_Users[game_Invitation.Invitation_Reveiver].SendGame_Invitation(game_Invitation);
                        }
                        else if (Form.user_Availability[game_Invitation.Invitation_Reveiver].user_Status == User_Status.Inviting)
                        {
                            formatter.Serialize(stream, new Game_Invitation_Result(false, "The user is inviting somebody else..."));
                        }
                        else if (Form.user_Availability[game_Invitation.Invitation_Reveiver].user_Status == User_Status.Playing)
                        {
                            formatter.Serialize(stream, new Game_Invitation_Result(false, "The user is playing with somebody else..."));
                        }
                    }
                    else
                    {
                        formatter.Serialize(stream, new Game_Invitation_Result(false, "You can't invite a offline user for game."));
                    }
                }
                else if (obj is Game_Request_Result)
                {
                    Game_Request_Result game_Request_Result = obj as Game_Request_Result;
                    string first = null;
                    string second = null;
                    if (game_Request_Result.Begin)
                    {
                        Form.game_Pair.Add(new Game_Ready(game_Request_Result.Inviter, false), new Game_Ready(game_Request_Result.Accepter, false));
                        Form.user_Availability[game_Request_Result.Inviter].user_Status = User_Status.Playing;
                        Form.user_Availability[game_Request_Result.Accepter].user_Status = User_Status.Playing;
                        Random rdm = new Random();
                        int num = rdm.Next(1, 3);
                        switch (num)
                        {
                            case 1:
                                first = game_Request_Result.Accepter;
                                second = game_Request_Result.Inviter;
                                break;
                            case 2:
                                second = game_Request_Result.Accepter;
                                first = game_Request_Result.Inviter;
                                break;
                        }
                        game_Request_Result.First = first;
                        game_Request_Result.Second = second;

                        Form.online_Users[game_Request_Result.Accepter].SendGame_Request_Result(game_Request_Result);
                    }
                    else if (!game_Request_Result.Begin)
                    {
                        Form.user_Availability[game_Request_Result.Inviter].user_Status = User_Status.Free;
                    }
                    Form.online_Users[game_Request_Result.Inviter].SendGame_Request_Result(game_Request_Result);
                }
                else if (obj is Game_Ready)
                {
                    Game_Ready game_Ready = obj as Game_Ready;
                    foreach (KeyValuePair<Game_Ready, Game_Ready> kv in Form.game_Pair)
                    {
                        if (kv.Key.User == game_Ready.User)
                        {
                            kv.Key.Ready = game_Ready.Ready;
                        }
                        else if (kv.Value.User == game_Ready.User)
                        {
                            kv.Value.Ready = game_Ready.Ready;
                        }
                        if (kv.Key.Ready == true && kv.Value.Ready == true)
                        {
                            Form.online_Users[kv.Key.User].SendGameStart();
                            Form.online_Users[kv.Value.User].SendGameStart();
                        }

                    }
                }
                else if (obj is Game_Question)
                {
                    Game_Question game_Question = obj as Game_Question;
                    Form.online_Users[game_Question.Opponent].SendGame_Question(game_Question);
                }
                else if (obj is Game_Turn_Finish)
                {
                    Game_Turn_Finish game_Turn_Finish = obj as Game_Turn_Finish;

                    if (game_Turn_Finish.Steps.Count == 5)
                    {
                        Form.online_Users[game_Turn_Finish.Opponent].SendGame_Winner(game_Turn_Finish.User);
                        Form.online_Users[game_Turn_Finish.User].SendGame_Winner(game_Turn_Finish.User);
                    }
                    else if (game_Turn_Finish.Steps.Count >= 3)
                    {
                        int countH1 = 0;
                        int countH2 = 0;
                        int countH3 = 0;
                        int countV1 = 0;
                        int countV2 = 0;
                        int countV3 = 0;
                        foreach (string position in game_Turn_Finish.Steps)
                        {
                            string[] tem = position.Split(',');
                            int numX = int.Parse(tem[0]);
                            int numY = int.Parse(tem[1]);

                            if (numY == 0)
                            {
                                countH1++;
                            }
                            else if (numY == 1)
                            {
                                countH2++;
                            }
                            else if (numY == 2)
                            {
                                countH3++;
                            }
                            if (numX == 0)
                            {
                                countV1++;
                            }
                            else if (numX == 1)
                            {
                                countV2++;
                            }
                            else if (numX == 2)
                            {
                                countV3++;
                            }
                        }
                        if (countH1 == 3 || countH2 == 3 || countH3 == 3 || countV1 == 3 || countV2 == 3 || countV3 == 3)
                        {
                            Form.online_Users[game_Turn_Finish.Opponent].SendGame_Winner(game_Turn_Finish.User);
                            Form.online_Users[game_Turn_Finish.User].SendGame_Winner(game_Turn_Finish.User);
                        }
                        else if (countH1 == 1 && countH2 == 1 && countH3 == 1 && countV1 == 1 && countV2 == 1 && countV3 == 1)
                        {
                            Form.online_Users[game_Turn_Finish.Opponent].SendGame_Winner(game_Turn_Finish.User);
                            Form.online_Users[game_Turn_Finish.User].SendGame_Winner(game_Turn_Finish.User);
                        }

                    }
                    else if (game_Turn_Finish.Steps.Count < 3)
                    {
                        Form.online_Users[game_Turn_Finish.Opponent].SendGame_Turn_Finish(game_Turn_Finish);
                    }
                }
                else if (obj is Game_Quit)
                {
                    Game_Ready temKey = null;
                    Game_Quit game_Quit = obj as Game_Quit;
                    foreach (KeyValuePair<Game_Ready, Game_Ready> pair in Form.game_Pair)
                    {
                        if (pair.Key.User == game_Quit.You && pair.Value.User == game_Quit.Opponent)
                        {
                            temKey = pair.Key;
                        }
                        else if (pair.Value.User == game_Quit.You && pair.Key.User == game_Quit.Opponent)
                        {
                            temKey = pair.Key;
                        }
                    }
                    Form.online_Users[game_Quit.Opponent].SendGame_Quit(game_Quit);
                    Form.game_Pair.Remove(temKey);
                    Form.user_Availability[game_Quit.You].user_Status = User_Status.Free;
                    Form.user_Availability[game_Quit.Opponent].user_Status = User_Status.Free;
                }
            }
        }

        public void SendGame_Quit(Game_Quit game_Quit)
        {
            formatter.Serialize(stream, game_Quit);
        }
        public void Quit()
        {
            check = false;
        }

        public void SendGame_Question(Game_Question game_Question)
        {
            formatter.Serialize(stream, game_Question);
        }
        public void SendGame_Turn_Finish(Game_Turn_Finish game_Turn_Finish)
        {
            formatter.Serialize(stream, game_Turn_Finish);
        }
        public void SendGame_Winner(string winner)
        {
            formatter.Serialize(stream,new GetWinner(winner));
        }
        public void SendGameStart()
        {
            formatter.Serialize(stream, new Game_Start(true));
        }
        public void SendMessage(Message message)
        {
            formatter.Serialize(stream, message);
        }
        public void SendGame_Invitation(Game_Invitation game_Invitation)
        {
            formatter.Serialize(stream, game_Invitation);
        }
        public void SendGame_Request_Result(Game_Request_Result result)
        {
            formatter.Serialize(stream, result);
        }

        public bool AccountIsExisting(SignUp signUp)
        {
            bool result = false;
            foreach (LogIn user in Form.exists_Users)
            {
                if (user.Account == signUp.NewAccount)
                {
                    result = true;
                    break;
                }
                else
                {
                    result = false;
                }
            }
            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using Virtion.IM.View.Control;

namespace Virtion.IM.View.Windows
{
    public partial class GroupChatWindow : Window
    {
        private Group group;
        private List<Message> messageList;
        private List<User> memberList;
        private Message lastItem;

        public GroupChatWindow()
        {
            InitializeComponent();
            this.messageList = new List<Message>();
           
        }

        public void SetGroup(Group group)
        {
            this.group = group;
        }

        public void AddTextMessage(Message message)
        {
            this.messageList.Add(message);
        }

        public void AddTextMessageList(List<Message> messageList)
        {
            for (int i = 0; i < messageList.Count; i++)
            {
                this.messageList.Add(messageList[i]);
            }
        }

        private void ShowMessage()
        {
            string text = this.TB_InputMessage.Text;
            MainWindow.imMagr.SendGroupMessage(this.group, text);
            Message message = new Message()
            {
                Text = text
            };
            this.AddTextMessage(message);
        }

        public void SetMenberList(List<User> userList)
        {
            this.memberList = userList;
            for (int i = 0; i < userList.Count; i++)
            {
                var userItem = userList[i];
                this.SP_MenberList.Children.Add(UserInfoItem.GotyeUserToUserInfoItem(userItem));
            }
        }


        private void B_SendMessage_Click(object s, RoutedEventArgs e)
        {
            this.ShowMessage();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private void TB_InputMessage_KeyDown(object sender, KeyEventArgs e)
        {
            //Console.WriteLine(e.Key);
            if (e.Key == Key.Enter)
            {
                this.ShowMessage();
                e.Handled = true;
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow.imMagr.GetGroupMenber(this.group);
            System.Timers.Timer t = new System.Timers.Timer(1000);
            t.Elapsed += new System.Timers.ElapsedEventHandler(Check_Timer);
            t.AutoReset = true;
            t.Enabled = true;
        }

        public void Check_Timer(object source, System.Timers.ElapsedEventArgs e)
        {
            while (true)
            {
                Thread.Sleep(1000);
                if (this.lastItem == null)
                {
                    MainWindow.imMagr.GetGroupMessage(this.group, null);
                }
                else
                {
                    MainWindow.imMagr.GetGroupMessage(this.group, this.lastItem.Time);
                }
                this.AddMessageItemToUI();
            }
        }

        public void AddMessageItemToUI()
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                for (int i = 0; i < this.messageList.Count; i++)
                {
                    this.AddMessageItem(messageList[i]);
                    this.lastItem = messageList[i];
                }
                this.messageList.Clear();
            }));
        }
        private void AddMessageItem(Message message)
        {
            var item = MessageItem.GotyeMessageToMessageItem(message);
            item.ShowType = MessageShowType.Left;
            this.ChatContainer.Children.Add(item);
        }

    }
}

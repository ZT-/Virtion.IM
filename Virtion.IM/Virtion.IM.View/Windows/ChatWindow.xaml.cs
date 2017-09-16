using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Virtion.IM.View.Control;

namespace Virtion.IM.View.Windows
{
    public partial class ChatWindow : Window
    {
        private User Guset;
        private List<Message> MessageList;

        public ChatWindow()
        {
            InitializeComponent();
            this.MessageList = new List<Message>();
        }

        public void SetUser(User user)
        {
            this.Guset = user;
        }
        public void AddMessageItemToUI()
        {
            this.Dispatcher.Invoke(new Action(() =>
            {
                for (int i = 0; i < this.MessageList.Count; i++)
                {
                    this.AddMessageItem(MessageList[i]);
                }
                this.MessageList.Clear();
            }));
        }

        private void AddMessageItem(Message message)
        {
            var item = MessageItem.GotyeMessageToMessageItem(message);
            item.ShowType = MessageShowType.Left;
            this.ChatContainer.Children.Add(item);
        }

        public void AddTextMessage(Message message)
        {
            this.MessageList.Add(message);
        }

        public void AddTextMessageList(List<Message> messageList)
        {
            for (int i = 0; i < messageList.Count; i++)
            {
                this.MessageList.Add(messageList[i]);
            }
        }

        private void ShowMessage()
        {
            string text = this.TB_InputMessage.Text;
            MainWindow.imMagr.SendMessage(this.Guset, text);
            Message message = new Message()
            {
                Text = text
            };
            this.AddTextMessage(message);
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

        public void ClickHandler(object sender, MouseButtonEventArgs e)
        {
            var border = e.Source as Border;

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BackgroundWorker bw_test = new BackgroundWorker();
            bw_test.DoWork += new DoWorkEventHandler(Check_Timer);
            bw_test.RunWorkerAsync();
        }


        public void Check_Timer(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                Thread.Sleep(1000);
                MainWindow.imMagr.GetMessage(MainWindow.currentUser);
                this.AddMessageItemToUI();
            }
        }

    }
}

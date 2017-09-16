using System;
using System.Collections.Generic;
using System.Windows;
using Virtion.IM.View.Windows;

namespace Virtion.IM.View
{
    class ImChatListener : Listener
    {
        public void onSendMessage(StatusCode code, Message message)
        {

        }

        public void onReceiveMessage(Message message, String userName)
        {
            if (MainWindow.chatWindowMap.ContainsKey(userName))
            {
                
                Window window = MainWindow.chatWindowMap[userName];
                if (window.GetType() == typeof(ChatWindow) )
                {
                    (MainWindow.chatWindowMap[userName] as ChatWindow).AddTextMessage(message);
                }
                else if (window.GetType() == typeof(GroupChatWindow))
                {
                    (MainWindow.chatWindowMap[userName] as GroupChatWindow).AddTextMessage(message);
                }
                
            }
        }

        public void onGetMessageList(List<Message> list, String userName)
        {
            if (MainWindow.chatWindowMap.ContainsKey(userName))
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Window window = MainWindow.chatWindowMap[userName];
                    if (window.GetType() == typeof(ChatWindow))
                    {
                        (MainWindow.chatWindowMap[userName] as ChatWindow).AddTextMessageList(list);
                    }
                    else if (window.GetType() == typeof(GroupChatWindow))
                    {
                        (MainWindow.chatWindowMap[userName] as GroupChatWindow).AddTextMessageList(list);
                    }
                }

            }
        }

    }
}

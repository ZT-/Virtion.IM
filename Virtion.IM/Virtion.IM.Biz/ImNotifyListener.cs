namespace Virtion.IM.View
{
    class ImNotifyListener : Listener
    {
        /**
        * 收到或发送消息回调通知
        * @param message  消息对象
        */
        public void onReceivePushMessage(Message message)
        {
          

        }


        public void onFriendChanged(bool addOrRemove,User user)
        {
            if (addOrRemove == true)
            {
                MainWindow.friendListWindow.AddNewFriend(user);
            }
            else
            {
                MainWindow.friendListWindow.DeleteFriend(user);
            }
        }

        public void onGroupChanged(bool addOrRemove, Group group)
        {
            if (addOrRemove == true)
            {
                MainWindow.friendListWindow.AddNewGroup(group);
            }
            else
            {
                MainWindow.friendListWindow.DeleteGroup(group);
            }
        }

        public void onNotifyStateChanged()
        { 
        
        }
    }
}

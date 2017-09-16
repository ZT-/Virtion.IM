using System;

namespace Virtion.IM.View
{
    public class TrayMenu
    {
        private NotifyTray notifyTray;
        public TrayMenu(NotifyTray notifyTray)
        {
            this.notifyTray = notifyTray;
            notifyTray.SetMenu("Create Group", Create_Group_EventHandler);
            notifyTray.SetMenu("Register", Register_EventHandler);
            notifyTray.SetMenu("Add Friend", Add_Friend_EventHandler);
            notifyTray.SetMenu("Exit", Exit_EventHandler);
        }

        public void Create_Group_EventHandler(object sender, EventArgs e)
        {
            //MainWindow.createGroupWindow = new Windows.CreateGroupWindow();
            //MainWindow.createGroupWindow.Show();
        }

        public void Register_EventHandler(object sender, EventArgs e)
        {
            //MainWindow.registerWindow = new Windows.RegisterWindow();
            //MainWindow.registerWindow.Show();
        }

        public void Exit_EventHandler(object sender, EventArgs e)
        {
            //var messageBox = new Windows.MessageWindow();

            //messageBox.ShowDialog();

            notifyTray.Dispose();
            //MainWindow.imMagr.LogOut(MainWindow.currentUser);
            //MainWindow.imMagr.Exit();

        }

        public void Add_Friend_EventHandler(object sender, EventArgs e)
        {
            //MainWindow.searchFriendWindow = new Windows.SearchFriendWindow();
            //MainWindow.searchFriendWindow.Show();
        }


    }
}

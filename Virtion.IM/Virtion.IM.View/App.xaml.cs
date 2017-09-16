using System;
using System.Collections.Generic;
using System.Windows;
using Virtion.IM.View.Windows;

namespace Virtion.IM.View
{
    public partial class App : Application
    {
        //public static ImManager imMagr;
        public static LoginWindow loginWindows;
        //public static FriendListWindow friendListWindow;
        //public static SearchFriendWindow searchFriendWindow;
        //public static AlterDataWindow alterInfoWindow;
        public static RegisterWindow registerWindow;
        //public static CreateGroupWindow createGroupWindow;
        public static Dictionary<string, Window> chatWindowMap;
        public static Boolean isTest;
        public static NotifyTray notifyTray;
        //public static User currentUser;

        public App()
        {
            this.Startup += App_Startup;
        }

        private void OnLoaded()
        {
            //this.Hide();
            //imMagr = new ImManager();
            loginWindows = new LoginWindow();
            loginWindows.Show();
            notifyTray = new NotifyTray();
            notifyTray.InitialTray();
        }

        void App_Startup(object sender, StartupEventArgs e)
        {
            (new LoginWindow()).Show();
        }

    }
}

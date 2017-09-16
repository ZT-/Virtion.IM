using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using Virtion.IM.View.Control;

namespace Virtion.IM.View.Windows
{
    public partial class FriendListWindow : Window
    {
        private User user;
        private UserInfoWindow showInfoWindow;
        private List<User> friendList;
        private List<Group> groupList;

        public FriendListWindow()
        {
            InitializeComponent();
        }

        public void SetUser(User user)
        {
            this.user = user;
            TB_NickName.Text = user.NickName;
        }

        public void SetFriendList(List<User> friendList)
        {
            this.friendList = friendList;
            var tm = new System.Timers.Timer();
            tm.Interval = 1;
            tm.AutoReset = false;
            tm.Enabled = true;
            tm.Elapsed += SetFriendItemAction;
            this.PB_Progress.Visibility = Visibility.Hidden;
        }

        public void SetGroupList(List<Group> groupList)
        {
            this.groupList = groupList;
            var tm = new System.Timers.Timer();
            tm.Interval = 1;
            tm.AutoReset = false;
            tm.Enabled = true;
            tm.Elapsed += SetGroupItemAction;
            this.PB_Progress.Visibility = Visibility.Hidden;
        }


        public void AddNewFriend(User user)
        {
            var item = UserInfoItem.GotyeUserToUserInfoItem(user);
            this.SL_FriendList.AddItem(item);
        }

        public void DeleteFriend(User user)
        {
            UIElementCollection UIList = this.SL_FriendList.GetChildren();
            for (int i = 0; i < UIList.Count; i++)
            {
                var item = (UIList[i] as UserInfoItem);
                if (item.User.Username == user.Username)
                {
                    this.SL_FriendList.RemoveItem(item);
                }
            }
        }

        public void AddNewGroup(Group group)
        {
            var item = GroupInfoItem.GroupToUserInfoItem(group);
            this.SL_GroupList.AddItem(item);
        }

        public void DeleteGroup(Group group)
        {
            UIElementCollection UIList = this.SL_GroupList.GetChildren();
            for (int i = 0; i < UIList.Count; i++)
            {
                var item = (UIList[i] as GroupInfoItem);
                if (item.Group.GroupID == group.GroupID)
                {
                    this.SL_GroupList.RemoveItem(item);
                }
            }        

        }


        public User FindUserByName(String name)
        {
            var children = this.SL_FriendList.GetChildren();
            return null;
        }

        private void SetGroupItemAction(object sender, System.Timers.ElapsedEventArgs e)
        {
            for (int i = 0; i < groupList.Count; i++)
            {
                Thread.Sleep(100);
                this.Dispatcher.Invoke(DispatcherPriority.Send, new Action(
                    delegate
                    {
                        var item = GroupInfoItem.GroupToUserInfoItem(groupList[i]);
                        this.SL_GroupList.AddItem(item);
                    }));
            }
        }

        private void SetFriendItemAction(object sender, System.Timers.ElapsedEventArgs e)
        {
            for (int i = 0; i < friendList.Count; i++)
            {
                Thread.Sleep(100);
                this.Dispatcher.Invoke(DispatcherPriority.Send, new Action(
                    delegate
                    {
                        var item = UserInfoItem.GotyeUserToUserInfoItem(friendList[i]);
                        this.SL_FriendList.AddItem(item);
                    }));
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Left = SystemParameters.PrimaryScreenWidth - this.Width;//屏幕宽度
            //SystemParameters.PrimaryScreenHeight;//屏幕高度
            this.SetUser(MainWindow.currentUser);
            MainWindow.imMagr.GetFriendList(user);
            Thread.Sleep(500);
            MainWindow.imMagr.GetGroupList(user);
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.alterInfoWindow = new AlterDataWindow();
            MainWindow.alterInfoWindow.Show();
        }
        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            Border border = (e.Source as Border);
            border.BorderBrush = new SolidColorBrush(Colors.Aqua);

            if (this.showInfoWindow == null)
            {
                this.showInfoWindow = new UserInfoWindow();
            }
            this.showInfoWindow.ResetPostion();
            this.showInfoWindow.Show();
        }
        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            this.showInfoWindow.Hide();
            Border border = (e.Source as Border);
            border.BorderBrush = new SolidColorBrush(Colors.Black);
        }
        private void TB_NickName_MouseEnter(object sender, MouseEventArgs e)
        {
            TextBlock textBlock = (e.Source as TextBlock);
            textBlock.Foreground = new SolidColorBrush(Colors.Aqua);
        }
        private void TB_NickName_MouseLeave(object sender, MouseEventArgs e)
        {
            TextBlock textBlock = (e.Source as TextBlock);
            textBlock.Foreground = new SolidColorBrush(Colors.White);
        }
        private void FriendList_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.SL_FriendList.Visibility = Visibility.Visible;
            this.SL_GroupList.Visibility = Visibility.Hidden;
        }
        private void GroupList_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.SL_FriendList.Visibility = Visibility.Hidden;
            this.SL_GroupList.Visibility = Visibility.Visible;
        }


    }
}

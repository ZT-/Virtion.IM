using System;
using System.Windows;
using System.Windows.Controls;
using Virtion.IM.View.Control;

namespace Virtion.IM.View.Windows
{
    public partial class SearchFriendWindow : Window
    {
        private Object current;
        public SearchFriendWindow()
        {
            InitializeComponent();
        }

        public void ShowResult(bool result, Object resultUI)
        {
            this.SP_Result.Children.Clear();
            if (result == false)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = "未找到";
                this.SP_Result.Children.Add(textBlock);
            }
            else
            {
                this.current = resultUI;
                if (resultUI.GetType() == typeof(User))
                {
                    this.SP_Result.Children.Add(UserInfoItem.GotyeUserToUserInfoItem(resultUI as User));
                }
                else
                {
                    this.SP_Result.Children.Add(GroupInfoItem.GroupToUserInfoItem(resultUI as Group));
                }
            }
        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.RB_User.IsChecked == true)
            {
                MainWindow.imMagr.SearchFriend(this.TB_UserName.Text);
            }
            else
            {
                MainWindow.imMagr.SearchGroup(this.TB_UserName.Text);
            }
        }

        private void AddFriendButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.current == null)
            {
                this.TB_result.Text = "未获得查找信息添加失败";
                return;
            }

            if (this.current.GetType() == typeof(User))
            {
                MainWindow.imMagr.AddFriend(this.current as User);
            }
            else
            {
                MainWindow.imMagr.AddGroup(this.current as Group);
            }
            this.TB_result.Text = "添加成功";
        }
    }
}

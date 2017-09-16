using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Virtion.IM.View.Control
{
    public delegate void GroupInfoItemDoubleClickCallBack(Group user);
    public partial class GroupInfoItem : UserControl
    {
        public static GroupInfoItemDoubleClickCallBack OnDoubleClickCallBack;
        public Group Group { get; set; }
        private Storyboard myStoryboard;

        public String GroupName
        {
            get
            {
                return this.TB_NickName.Text;
            }
            set
            {
                this.TB_NickName.Text = value;
            }
        }

        public GroupInfoItem()
        {
            InitializeComponent();
            this.MouseDown += this.OnDoubleClick;

            ThicknessAnimation ta = new ThicknessAnimation();
            ta.From = new Thickness(-300, 0, 0, 0);
            ta.To = new Thickness(0, 0, 0, 0);
            ta.Duration = TimeSpan.FromSeconds(0.4);
            Storyboard.SetTarget(ta, this.context);
            Storyboard.SetTargetProperty(ta, new PropertyPath(Window.MarginProperty));

            myStoryboard = new Storyboard();
            myStoryboard.Children.Add(ta);
        }

        public static GroupInfoItem GroupToUserInfoItem(Group group)
        {
            GroupInfoItem item = new GroupInfoItem();
            item.GroupName = group.GroupName;
            item.Group = group;
            return item;
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            this.context.Background = new SolidColorBrush(Colors.Aqua);
        }
        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            this.context.Background = new SolidColorBrush(Color.FromArgb(180, 0, 0, 0));
        }
        private void OnDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (GroupInfoItem.OnDoubleClickCallBack != null)
                {
                    GroupInfoItem.OnDoubleClickCallBack(this.Group);
                }
            }
        }
        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            myStoryboard.Begin();
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.imMagr.DeleteGroup(this.Group);
        }

    }
}

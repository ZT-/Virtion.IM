using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Virtion.IM.View.Windows;

namespace Virtion.IM.View.Control
{

    public partial class SlideList : UserControl
    {
        public SlideList()
        {
            InitializeComponent();
            UserInfoItem.OnDoubleClickCallBack += this.OnDoubleClick;
            GroupInfoItem.OnDoubleClickCallBack += this.OnGroupDoubleClick;
        }

        public void Init()
        {
        }

        public void AddItem(UIElement item)
        {
            this.ContainList.Children.Add(item);
        }
        public void RemoveItem(UIElement item)
        {
            this.ContainList.Children.Remove(item);
        }

        public UIElementCollection GetChildren()
        {
            return this.ContainList.Children;
        }

        private T GetVisualChild<T>(DependencyObject parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = 0;
            try
            {
                numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }

        void OnGroupDoubleClick(Group group)
        {
            GroupChatWindow window = null;
            if (MainWindow.chatWindowMap.ContainsKey(group.GroupID) == true)
            {
                window = MainWindow.chatWindowMap[group.GroupID] as GroupChatWindow;
            }
            else
            {
                window = new GroupChatWindow();
                MainWindow.chatWindowMap[group.GroupID] = window;
                window.SetGroup(group);
            }
            window.Show();
        }


        void OnDoubleClick(User user)
        {
            ChatWindow window = null;
            if (MainWindow.chatWindowMap.ContainsKey(user.Username) == true)
            {
                window = MainWindow.chatWindowMap[user.Username] as ChatWindow;
            }
            else
            {
                window = new Windows.ChatWindow();
                MainWindow.chatWindowMap[user.Username] = window;
                window.SetUser(user);
            }
            window.Show();
        }

        private void UpMoreButton_MouseEnter(object sender, MouseEventArgs e)
        {
            this.UpMoreButton.Opacity = 1;
        }
        private void UpMoreButton_MouseLeave(object sender, MouseEventArgs e)
        {
            this.UpMoreButton.Opacity = 0.1;
        }
        private void DownMoreButton_MouseEnter(object sender, MouseEventArgs e)
        {
            this.DownMoreButton.Opacity = 1;
        }
        private void DownMoreButton_MouseLeave(object sender, MouseEventArgs e)
        {
            this.DownMoreButton.Opacity = 0.1;
        }
    }
}

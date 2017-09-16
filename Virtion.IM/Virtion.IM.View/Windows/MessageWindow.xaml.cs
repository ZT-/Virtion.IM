using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace Virtion.IM.View.Windows
{
    public partial class MessageWindow : Window
    {
        private Storyboard myStoryboard;

        public MessageWindow()
        {
            InitializeComponent();

            DoubleAnimation myDoubleAnimation = new DoubleAnimation();
            myDoubleAnimation.From = 0;
            myDoubleAnimation.To = 100;
            myDoubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.3));
            Storyboard.SetTarget(myDoubleAnimation, this.MessageContent);
            Storyboard.SetTargetProperty(myDoubleAnimation, new PropertyPath(Grid.HeightProperty));


            ThicknessAnimation ta = new ThicknessAnimation();
            ta.From = new Thickness(0, 50, 0, 0);
            ta.To = new Thickness(0, 0, 0, 0);
            ta.Duration = TimeSpan.FromSeconds(0.3);
            Storyboard.SetTarget(ta, this.G_Title);
            Storyboard.SetTargetProperty(ta, new PropertyPath(Window.MarginProperty));

            myStoryboard = new Storyboard();
            myStoryboard.Children.Add(myDoubleAnimation);
            myStoryboard.Children.Add(ta);

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            myStoryboard.Begin(this);
        }

        private void CB_Ok_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void CB_Close_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}

using System;
using System.Windows;
using System.Windows.Controls;

namespace Virtion.IM.View.Control
{
    public enum MessageShowType
    {
        Left,
        Right
    }

    public partial class MessageContainer : UserControl
    {
        public String Text
        {
            get
            {
                return this.TB_Message.Text;
            }
            set
            {
                this.TB_Message.Text = value;
            }
        }

        public MessageShowType ShowType
        {
            get
            {
                return this.ShowType;
            }
            set
            {
                if (value == MessageShowType.Left)
                {
                    this.Right_Triangle.Visibility = Visibility.Hidden;
                }
                else if (value == MessageShowType.Right)
                {
                    this.Left_Triangle.Visibility = Visibility.Hidden;
                }
            }
        }

        public MessageContainer()
        {
            InitializeComponent();
        }


    }
}

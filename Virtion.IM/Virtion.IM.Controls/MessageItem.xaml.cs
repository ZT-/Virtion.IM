using System;
using System.Windows;
using System.Windows.Controls;

namespace Virtion.IM.View.Control
{
    /// <summary>
    /// MessageItem.xaml 的交互逻辑
    /// </summary>
    public partial class MessageItem : UserControl
    {
        public String MessageText
        {
            get
            {
                return this.MC_Message.Text;
            }
            set
            {
                this.MC_Message.Text = value;
            }
        }

        public Message message;

        public MessageShowType ShowType
        {
            set
            {
                MC_Message.ShowType = value;

                if (value == MessageShowType.Left)
                {
                    this.G_Container.ColumnDefinitions[0].Width = new GridLength(50);
                    this.G_Container.ColumnDefinitions[1].Width = new GridLength(1, GridUnitType.Star);
                    Grid.SetColumn(this.E_Avatar, 0);
                    Grid.SetColumn(this.MC_Message, 1);
                }
                else if (value == MessageShowType.Right)
                {
                    this.G_Container.ColumnDefinitions[0].Width = new GridLength(1, GridUnitType.Star);
                    this.G_Container.ColumnDefinitions[1].Width = new GridLength(50);
                    Grid.SetColumn(this.MC_Message, 0);
                    Grid.SetColumn(this.E_Avatar, 1);
                }
            }
        }

        public MessageItem()
        {
            InitializeComponent();
        }

        public static MessageItem GotyeMessageToMessageItem(Message message)
        {
            MessageItem item = new MessageItem();
            item.message = message;
            item.MessageText = message.Text;
            return item;
        }
    }
}

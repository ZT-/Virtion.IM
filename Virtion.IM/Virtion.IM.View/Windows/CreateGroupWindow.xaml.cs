using System.Windows;

namespace Virtion.IM.View.Windows
{
    public partial class CreateGroupWindow : Window
    {
        public CreateGroupWindow()
        {
            InitializeComponent();
        }



        private void BT_Ok_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.imMagr.GreateGroup(this.TB_Name.Text, TB_Description.Text);
        }

        private void BT_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}

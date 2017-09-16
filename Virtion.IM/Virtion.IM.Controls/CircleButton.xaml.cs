using System.Windows.Controls;
using System.Windows.Media;

namespace Virtion.IM.Controls
{
    public partial class CircleButton : UserControl
    {
        public ImageSource ImageBackground
        {
            get
            {
                return this.EllipseBackground.ImageSource;
            }
            set
            {
                this.EllipseBackground.ImageSource = value;
            }
        }
        public CircleButton()
        {
            InitializeComponent();
        }




    }
}

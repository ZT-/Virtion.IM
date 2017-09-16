using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Virtion.IM.View
{
    class MediaHelper
    {
        public static ImageBrush CreateImageBrushFromPath(Uri uri)
        {
            if (File.Exists(uri.AbsolutePath) == false)
            {
                MessageBox.Show("File is not found! " + uri.AbsolutePath);
                return null;
            }
            return new ImageBrush(new BitmapImage(uri));
        }
    }
}

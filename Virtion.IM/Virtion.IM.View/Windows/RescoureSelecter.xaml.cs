using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Virtion.IM.View.Windows
{
    public delegate void CellClickEventHandler(object sender, MouseButtonEventArgs e);
    public partial class RescoureSelecter : Window
    {
        private String configPath;
        private BitmapImage bitmap;
        public CellClickEventHandler CellClickEvent;
        public RescoureSelecter(String path)
        {
            this.configPath = path;
            InitializeComponent();
        }
        private Border BorderBuilder()
        {
            Border cell = new Border()
            {
                Width = 100,
                Height = 100,
                BorderBrush = new SolidColorBrush(Colors.Black),
                BorderThickness = new Thickness(1),
            };
            cell.MouseEnter += this.MouseEnterHandler;
            cell.MouseLeave += this.MouseLeaveHandler;
            cell.MouseLeftButtonUp += this.ClickHandler;
            return cell;
        }
        private List<ImageSource> ClipAllParts(BitmapImage sourceBitmap)
        {
            List<ImageSource> partsList = new List<ImageSource>();


            return partsList;
        }
        private void BuildGrid(int column, int row)
        {
            while (row-- > 0)
            {
                RowDefinition rd = new RowDefinition();
                rd.Height = new GridLength(100);
                this.G_Parts.RowDefinitions.Add(rd);
            }
            while (column-- > 0)
            {
                ColumnDefinition cd = new ColumnDefinition();
                cd.Width = new GridLength(100);
                this.G_Parts.ColumnDefinitions.Add(cd);
            }
        }
        private void BuildPartsCell()
        {
            var partsList = this.ClipAllParts(bitmap);

            DrawingVisual dv = new DrawingVisual();
            Rect destR = new Rect(0, 0, 100, 100);

            for (int i = 0; i < partsList.Count; i++)
            {
                var border = this.BorderBuilder();
                var image = partsList[i];
                DrawingContext dc = dv.RenderOpen();
                dc.DrawImage(image, destR);
                dc.Close();

                RenderTargetBitmap rtb = new RenderTargetBitmap((int)Math.Ceiling(destR.Width),
                    (int)Math.Ceiling(destR.Height), 96.0, 96.0, PixelFormats.Default);
                rtb.Render(dv);

                border.Background = new ImageBrush(rtb);


                this.G_Parts.Children.Add(border);

                int row = i / 6;
                int column = i % 6;

                Grid.SetColumn(border, column);
                Grid.SetRow(border, row);
            }
        }
        private void PerpareResource()
        {

        }
        public ImageSource ClipImage(BitmapImage srcImage, Int32Rect cutRect)
        {
            if ((srcImage.Width < cutRect.X + cutRect.Width) || (srcImage.Height < cutRect.Y + cutRect.Height))
            {
                throw new Exception("rect scal is too lager");
            }

            FormatConvertedBitmap srcFormatBitmap = new FormatConvertedBitmap(srcImage,
            PixelFormats.Bgra32, BitmapPalettes.WebPaletteTransparent, 0);
            int stride = srcFormatBitmap.Format.BitsPerPixel * cutRect.Width / 8;
            byte[] data = new byte[cutRect.Height * stride];

            srcFormatBitmap.CopyPixels(cutRect, data, stride, 0);
            return BitmapSource.Create(cutRect.Width, cutRect.Height, 0, 0, PixelFormats.Bgra32,
                 BitmapPalettes.WebPaletteTransparent, data, stride);
        }

        public void ClickHandler(object sender, MouseButtonEventArgs e)
        {
            this.CellClickEvent(sender, e);
           var border =e.Source as Border;

            this.Hide();
        }
        private void G_Parts_Loaded(object sender, RoutedEventArgs e)
        {
            PerpareResource();
            BuildGrid(6, 6);
            BuildPartsCell();
        }
        public void MouseEnterHandler(object sender, MouseEventArgs e)
        {
            var border = e.Source as Border;
            border.BorderBrush = new SolidColorBrush(Colors.AliceBlue);

        }
        public void MouseLeaveHandler(object sender, MouseEventArgs e)
        {
            var border = e.Source as Border;
            border.BorderBrush = new SolidColorBrush(Colors.Black);
        }

    }
}

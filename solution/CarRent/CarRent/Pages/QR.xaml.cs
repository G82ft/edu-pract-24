using Aspose.BarCode.Generation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CarRent.Pages
{
    /// <summary>
    /// Логика взаимодействия для QR.xaml
    /// </summary>
    public partial class QR : Page
    {
        public QR()
        {
            InitializeComponent();

            BitmapImage bitmap = new BitmapImage();
            BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.QR, "https://youtube.com?v=dQw4w9WgXcQ");
            gen.Parameters.Barcode.XDimension.Pixels = 34;
            gen.Save(@"..\..\qr.png", BarCodeImageFormat.Png);

            bitmap.BeginInit();
            Console.WriteLine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName);
            Uri uri = new Uri($@"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName}\qr.png", UriKind.Absolute);
            bitmap.UriSource = uri;
            bitmap.EndInit();

            img.Source = bitmap;
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            AppData.MainFrame.GoBack();
        }
    }
}

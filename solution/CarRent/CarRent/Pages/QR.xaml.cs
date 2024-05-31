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
        private static Random random = new Random();
        private static int id = 0;
        public QR()
        {
            InitializeComponent();

            BitmapImage bitmap = new BitmapImage();
            string randString = new string(Enumerable.Repeat("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 11)
                .Select(s => s[random.Next(s.Length)]).ToArray());
            if (random.Next(100) > 80)
            {
                randString = "dQw4w9WgXcQ";
            }
            BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.QR, $"https://youtube.com/watch?v={randString}");
            gen.Parameters.Barcode.XDimension.Pixels = 34;
            gen.Save($@"..\..\QR\qr{id}.png", BarCodeImageFormat.Png);

            bitmap.BeginInit();
            Console.WriteLine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName);
            Uri uri = new Uri($@"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName}\QR\qr{id}.png", UriKind.Absolute);
            bitmap.UriSource = uri;
            bitmap.EndInit();

            img.Source = bitmap;
            id++;
        }
        private void Back(object sender, RoutedEventArgs e)
        {
            AppData.MainFrame.GoBack();
        }
    }
}

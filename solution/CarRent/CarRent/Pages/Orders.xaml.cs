using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
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
    /// Логика взаимодействия для Orders.xaml
    /// </summary>
    public partial class Orders : Page
    {
        public Orders()
        {
            InitializeComponent();
            listView.ItemsSource = AppData.CurrentUser.Orders.Where(x => x.User == AppData.CurrentUser.ID).ToList();
        }

        private void RemoveOrder(object sender, MouseButtonEventArgs e)
        {
            if (MessageBox.Show("Отменить заказ?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                return;
            }

            StackPanel sp = sender as StackPanel;
            int ID = int.Parse((sp.Children[0] as TextBlock).Text);
            AppData.Model.Orders.Remove(
                AppData.Model.Orders.Where(x => x.ID == ID).First()
            );
            AppData.Model.SaveChanges();
            AppData.MainFrame.GoBack();
            AppData.MainFrame.Navigate(new Orders());
        }

        private void ToPDF(object sender, RoutedEventArgs e)
        {
            PrintDocument document = new PrintDocument();
            document.PrintPage += new PrintPageEventHandler(OnPrintPage);
            document.Print();
        }

        private static void OnPrintPage(object sender, PrintPageEventArgs e)
        {
            float y = 0;
            foreach (CarRent.Orders order in AppData.CurrentUser.Orders.Where(x => x.State == 0).ToList())
            {
                Cars car = order.Cars;
                Models model = car.Models;
                e.Graphics.DrawString(
                    $"{model.Manufacturers.Name} {model.Name} (Номер - {car.Number}): {car.Cost * (order.EndDate - order.StartDate).Days} руб.",
                    new Font("Courier New", 12), System.Drawing.Brushes.Black, 0, y);
                y += 20;
                e.Graphics.DrawString(
                    $"От {order.StartDate.Date} до {order.EndDate.Date}",
                    new Font("Courier New", 12), System.Drawing.Brushes.Black, 0, y);
                y += 30;
                order.State = 1;
                AppData.Model.SaveChanges();
            }
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            AppData.MainFrame.Navigate(new View());
        }
    }
}

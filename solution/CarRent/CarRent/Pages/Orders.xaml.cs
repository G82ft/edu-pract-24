using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        private bool _allUsers;
        public Orders(bool allUsers = false)
        {
            _allUsers = allUsers;
            InitializeComponent();
            if (!allUsers)
            {
                listView.ItemsSource = AppData.CurrentUser.Orders.ToList().Where(x => x.User == AppData.CurrentUser.ID);
            }
            else
            {
                listView.ItemsSource = AppData.Model.Orders.ToList();
                pdf.Visibility = Visibility.Collapsed;
            }
            Console.WriteLine("---");
            foreach (var item in AppData.CurrentUser.Orders)
            {
                Console.WriteLine(item.ID);
                Console.WriteLine(item.User);
            }
            Console.WriteLine(AppData.Model.Orders.Any());
            Console.WriteLine(AppData.CurrentUser.Orders.Where(x => x.User == AppData.CurrentUser.ID).Any());
            Console.WriteLine("---");
        }

        private void OrderClick(object sender, MouseButtonEventArgs e)
        {
            StackPanel sp = sender as StackPanel;
            int ID = int.Parse((sp.Children[0] as TextBlock).Text);

            CarRent.Orders order = AppData.Model.Orders.Where(x => x.ID == ID).First();

            AppData.MainFrame.Navigate(new Order(order, allUsers: _allUsers));
        }

        private void ToPDF(object sender, RoutedEventArgs e)
        {
            if (!AppData.CurrentUser.Orders.Where(x => x.State == 1).Any()) {
                MessageBox.Show("У вас нет товаров.");
                return;
            }
            AppData.MainFrame.Navigate(new QR());
            PrintDocument document = new PrintDocument();
            document.PrintPage += new PrintPageEventHandler(OnPrintPage);
            document.Print();
        }

        private static void OnPrintPage(object sender, PrintPageEventArgs e)
        {
            float y = 0;
            decimal total = AppData.CurrentUser.Orders.Where(x => x.State == 1).Select(x => x.TotalCost).Sum();
            foreach (CarRent.Orders order in AppData.CurrentUser.Orders.Where(x => x.State == 1).ToList())
            {
                Cars car = order.Cars;
                Models model = car.Models;
                e.Graphics.DrawString(
                    $"{model.Manufacturers.Name} {model.Name} (Номер - {car.Number.Trim()})",
                    new Font("Courier New", 12), System.Drawing.Brushes.Black, 0, y);
                y += 20;
                e.Graphics.DrawString(
                    $"От {order.StartDate.Date} до {order.EndDate.Date}",
                    new Font("Courier New", 12), System.Drawing.Brushes.Black, 0, y);
                y += 20;
                e.Graphics.DrawString(
                    $"Цена: {order.TotalCost} руб.",
                    new Font("Courier New", 12), System.Drawing.Brushes.Black, 0, y);
                y += 30;
                order.State = AppData.Model.States.Where(x => x.Name == "Готово").FirstOrDefault().ID;
            }
            y += 40;
            e.Graphics.DrawString(
                    $"Итого: {total} руб.",
                    new Font("Courier New", 16), System.Drawing.Brushes.Black, 0, y);
            AppData.Model.SaveChanges();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            AppData.MainFrame.Navigate(new View());
        }
    }
}

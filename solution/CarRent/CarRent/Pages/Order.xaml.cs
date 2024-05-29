using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Xml.Serialization;

namespace CarRent.Pages
{
    /// <summary>
    /// Логика взаимодействия для Order.xaml
    /// </summary>
    public partial class Order : Page
    {
        public CarRent.Orders orders { get; set; }
        private bool _newOrder = false;
        private bool _allUsers = false;
        public Order(CarRent.Orders order, bool newOrder = false, bool allUsers = false)
        {
            InitializeComponent();

            _newOrder = newOrder;
            _allUsers = allUsers;

            if (AppData.CurrentUser.Role < 2)
            {
                stateSP.Visibility = Visibility.Collapsed;
            }

            orders = order;
            state.ItemsSource = AppData.Model.States.Select(x => x.Name).ToList();
            state.SelectedItem = AppData.Model.States.Where(x => x.ID == order.State).FirstOrDefault().Name;
            DataContext = orders;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            DateTime startDate;
            DateTime endDate;
            if (!(CheckDateFormat(start.Text, out startDate) & CheckDateFormat(end.Text, out endDate)))
            {
                MessageBox.Show("Некорректный формат данных!");
                return;
            }
            if (endDate <= startDate)
            {
                MessageBox.Show("Конечная дата должна быть после начальной.");
                return;
            }

            if (_newOrder)
            {
                AppData.Model.Orders.Add(orders);
            }
            AppData.Model.SaveChanges();
            AppData.MainFrame.Navigate(new Orders());
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            if (!_newOrder && MessageBox.Show("Отменить заказ?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                return;
            }

            AppData.Model.Orders.Remove(orders);
            AppData.Model.SaveChanges();

            if (_newOrder)
            {
                AppData.MainFrame.Navigate(new View());
            }
            else
            {
                AppData.MainFrame.Navigate(new Orders(_allUsers));
            }
        }

        public static bool CheckDateFormat(string date, out DateTime result, string format = "M/dd/yyyy hh:mm:ss tt")
        {
            return DateTime.TryParseExact(date, format, new CultureInfo("en-US"),
                                 DateTimeStyles.None, out result);
        }

        private void ChangeState(object sender, SelectionChangedEventArgs e)
        {
            if (!IsInitialized) return;

            orders.State = AppData.Model.States.Where(x => x.Name == state.SelectedItem).FirstOrDefault().ID;
        }
    }
}

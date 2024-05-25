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
        public Order(CarRent.Orders order)
        {
            InitializeComponent();
            orders = order;
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
            AppData.Model.SaveChanges();
            AppData.MainFrame.Navigate(new Orders());
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            AppData.Model.Orders.Remove(orders);
            AppData.Model.SaveChanges();
            AppData.MainFrame.GoBack();
        }

        public static bool CheckDateFormat(string date, out DateTime result, string format = "M/dd/yyyy hh:mm:ss tt")
        {
            return DateTime.TryParseExact(date, format, new CultureInfo("en-US"),
                                 DateTimeStyles.None, out result);
        }
    }
}

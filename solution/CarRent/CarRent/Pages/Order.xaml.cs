using System;
using System.Collections.Generic;
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
            AppData.Model.SaveChanges();
            AppData.MainFrame.GoBack();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            AppData.Model.Orders.Remove(orders);
            Save(sender, e);
        }
    }
}

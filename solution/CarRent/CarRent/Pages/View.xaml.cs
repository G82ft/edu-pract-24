using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для View.xaml
    /// </summary>
    public partial class View : Page
    {
        public View()
        {
            InitializeComponent();
            
            List<string> mfs = AppData.Model.Manufacturers.Select(x => x.Name).ToList();
            mfs.Insert(0, "*");

            filter.ItemsSource = mfs;
            filter.SelectedIndex = 0;
            sort.SelectedIndex = 0;

            Update();
        }

        public void Update()
        {
            if (filter.SelectedValue == null)
            {
                return;
            }
            string mf = filter.SelectedItem.ToString();

            IQueryable<Cars> cars = AppData.Model.Cars.Where(x => mf == "*" || mf == x.Models.Manufacturers.Name);

            if (!string.IsNullOrEmpty(search.Text))
            {
                cars = cars.Where(x => (x.Models.Manufacturers.Name + x.Models.Name).Contains(search.Text));
            }

            switch (sort.SelectedIndex)
            {
                case 1:
                    cars = cars.OrderBy(x => x.Cost); break;
                case 2:
                    cars = cars.OrderBy(x => x.Mileage); break;
            }

            listView.ItemsSource = cars.ToList();
        }

        private void Filter(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void Search(object sender, RoutedEventArgs e)
        {
            Update();
        }

        private void AddOrder(object sender, MouseButtonEventArgs e)
        {
            StackPanel sp = sender as StackPanel;
            int ID = int.Parse((sp.Children[0] as TextBlock).Text);
            CarRent.Orders order = new CarRent.Orders()
            {
                Car = ID,
                User = AppData.CurrentUser.ID,
                StartDate = DateTime.Now.Date,
                EndDate = DateTime.Now.Date,
                State = 0
            };
            AppData.Model.Orders.Add(order);
            AppData.Model.SaveChanges();
            AppData.MainFrame.Navigate(new Order(order));
        }

        private void ToEdit(object sender, RoutedEventArgs e)
        {
            AppData.MainFrame.Navigate(new Edit());
        }

        private void ToOrders(object sender, RoutedEventArgs e)
        {
            AppData.MainFrame.Navigate(new Orders());
        }
    }
}

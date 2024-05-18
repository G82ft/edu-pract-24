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

            switch (sort.SelectedIndex)
            {
                case 1:
                    cars = cars.OrderBy(x => x.Cost); break;
                case 2:
                    cars = cars.OrderBy(x => x.Mileage); break;
            }

            listView.ItemsSource = cars.ToList();
        }

        private void filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void Search(object sender, RoutedEventArgs e)
        {

            listView.ItemsSource = AppData.Model.Cars.Where(x => (x.Models.Manufacturers.Name + x.Models.Name).Contains(search.Text)).ToList();
        }

        private void Edit(object sender, MouseButtonEventArgs e)
        {
            StackPanel sp = sender as StackPanel;
            int ID = int.Parse((sp.Children[0] as TextBlock).Text);
            AppData.MainFrame.Navigate(new AddEdit(AppData.Model.Cars.Where(x => x.ID == ID).First()));
        }
    }
}

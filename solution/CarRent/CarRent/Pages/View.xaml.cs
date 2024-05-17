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
    /// Логика взаимодействия для View.xaml
    /// </summary>
    public partial class View : Page
    {
        public View()
        {
            InitializeComponent();

            filter.ItemsSource = AppData.Model.Manufacturers.Select(x => x.Name).ToList();
            filter.SelectedIndex = 0;

            Update();
        }

        public void Update()
        {
            if (filter.SelectedValue == null)
            {
                return;
            }
            listView.ItemsSource = AppData.Model.Cars.Where(x => x.Models.Manufacturers.Name == filter.SelectedItem.ToString()).ToList();
        }

        private void filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Update();
        }

        private void Search(object sender, RoutedEventArgs e)
        {

            listView.ItemsSource = AppData.Model.Cars.Where(x => (x.Models.Manufacturers.Name + x.Models.Name).Contains(search.Text)).ToList();
        }
    }
}

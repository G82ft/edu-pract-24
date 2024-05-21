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
    /// Логика взаимодействия для Orders.xaml
    /// </summary>
    public partial class Orders : Page
    {
        public Orders()
        {
            InitializeComponent();
            listView.ItemsSource = AppData.CurrentUser.Orders.ToList();
        }

        private void RemoveOrder(object sender, MouseButtonEventArgs e)
        {
            StackPanel sp = sender as StackPanel;
            int ID = int.Parse((sp.Children[0] as TextBlock).Text);
            AppData.Model.Orders.Remove(
                AppData.Model.Orders.Where(x => x.ID == ID).First()
            );
            AppData.Model.SaveChanges();
            AppData.MainFrame.GoBack();
            AppData.MainFrame.Navigate(new Orders());
        }
    }
}

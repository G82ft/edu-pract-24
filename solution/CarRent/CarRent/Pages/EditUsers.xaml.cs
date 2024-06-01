using System;
using System.Collections.Generic;
using System.Data.Entity;
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
using static System.Net.Mime.MediaTypeNames;

namespace CarRent.Pages
{
    /// <summary>
    /// Логика взаимодействия для EditUsers.xaml
    /// </summary>
    public partial class EditUsers : Page
    {
        public EditUsers()
        {
            InitializeComponent();

            AppData.Model.Users.Load();
            listView.ItemsSource = AppData.Model.Users.Local;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            AppData.Model.SaveChanges();
            AppData.MainFrame.Navigate(new View());
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить пользователя?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                return;
            }
            Button cb = sender as Button;
            int ID = int.Parse(((cb.Parent as DockPanel).Children[0] as TextBlock).Text);
            Users user = AppData.Model.Users.Where(x => x.ID == ID).FirstOrDefault();
                AppData.Model.Users.Remove(user);
                AppData.Model.SaveChanges();
                AppData.MainFrame.GoBack();
                AppData.MainFrame.Navigate(new EditUsers());
        }

        private void ToEdit(object sender, MouseButtonEventArgs e)
        {
            int ID = int.Parse(((sender as DockPanel).Children[0] as TextBlock).Text);
            Users user = AppData.Model.Users.Where(x => x.ID == ID).FirstOrDefault();

            AppData.MainFrame.Navigate(new Registration(user));
        }
    }
}

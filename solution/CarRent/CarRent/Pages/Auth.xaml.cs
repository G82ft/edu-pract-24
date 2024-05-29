using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Page
    {
        public Auth()
        {
            InitializeComponent();

            try
            {
                AppData.Model.Database.Connection.Open();
            }
            catch (SqlException)
            {
                MessageBox.Show("кажется кто-то недостаточный интеллектом забыл подключить бд");
                Application.Current.Shutdown();
            }
            catch (InvalidOperationException)
            {
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            if(!AppData.Model.Users.Where(x => x.Login == login.Text && x.Password == pwd.Password).Any())
            {
                MessageBox.Show("Неверные кредиты!");
                return;
            }
            AppData.CurrentUser = AppData.Model.Users.Where(x => x.Login == login.Text && x.Password == pwd.Password).First();

            MessageBox.Show($"Добро пожаловать, {login.Text}!");
            AppData.MainFrame.Navigate(new View());
        }

        private void ToSignUp(object sender, RoutedEventArgs e)
        {
            AppData.MainFrame.Navigate(new Registration());
        }
    }
}

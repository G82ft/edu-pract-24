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
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Page
    {
        public Auth()
        {
            InitializeComponent();
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

        private void SignUp(object sender, RoutedEventArgs e)
        {
            string loginTxt = login.Text;

            if (AppData.Model.Users.Where(x => x.Login == loginTxt).Any())
            {
                MessageBox.Show("Пользователь с таким логином уже существует!");
                return;
            }
            string password = pwd.Password;
            Users user = new Users()
            {
                Login = loginTxt,
                Password = password,
                Role = 1
            };
            AppData.Model.Users.Add(
                user
            );
            AppData.Model.SaveChanges();
            MessageBox.Show("Регистрация прошла успешно!");
            AppData.MainFrame.Navigate(new View());
        }
    }
}

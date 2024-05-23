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
    public partial class Registration : Page
    {
        public Registration()
        {
            InitializeComponent();
        }
        private void SignUp(object sender, RoutedEventArgs e)
        {
            string loginTxt = login.Text;

            if (AppData.Model.Users.Where(x => x.Login == loginTxt).Any())
            {
                MessageBox.Show("Пользователь с таким логином уже существует!");
                return;
            }
            string password = pwd.Text;
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

        private bool Validate()
        {
            string phoneTxt = phone.Text;
            string emailTxt = email.Text;
            string loginTxt = login.Text;
            string password = pwd.Text;
            foreach (string str in new List<string>() { phoneTxt, emailTxt, loginTxt, password })
            {
                if (string.IsNullOrEmpty(str))
                {
                    MessageBox.Show("Все поля должны быть заполнены!");
                    return false;
                }
            }

            if (AppData.Model.Users.Where(x => x.Login == loginTxt).Any())
            {
                MessageBox.Show("Пользователь с таким логином уже существует!");
                return false;
            }

            return Regex.Match(phoneTxt, @"^[\d]{11}$").Success
                && Regex.Match(emailTxt, @"^[a-z\d.]{1,63}[a-z\d]@[a-z\d]{1,63}\.[a-z]{1,192}$").Success;
        }

        private void ToSignIn(object sender, RoutedEventArgs e)
        {
            AppData.MainFrame.Navigate(new Auth());
        }
    }
}

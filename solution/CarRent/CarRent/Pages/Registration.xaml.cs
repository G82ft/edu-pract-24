﻿using System;
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
        private Users _user;
        public Registration(Users user = null)
        {
            InitializeComponent();
            if (user == null)
            {
                _user = new Users()
                {
                    Login = "",
                    Password = "",
                    Role = 1
                };
            }
            else
            {
                _user = user;
                role.Visibility = Visibility.Visible;
            }

            DataContext = _user;
            role.Text = AppData.Model.Roles.Where(x => x.ID == _user.Role).FirstOrDefault().Name;
            phone.PreviewTextInput += AppData.onlyIntPositive;

        }
        private void SignUp(object sender, RoutedEventArgs e)
        {
            if (!Validate()) return;

            if (AppData.CurrentUser != null)
            {
                AppData.MainFrame.Navigate(new EditUsers());
                return;
            }

            AppData.Model.Users.Add(_user);
            AppData.Model.SaveChanges();
            MessageBox.Show("Регистрация прошла успешно!");
            AppData.MainFrame.Navigate(new Auth());
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

            if (AppData.CurrentUser == null && AppData.Model.Users.Where(x => x.Login == loginTxt).Any())
            {
                MessageBox.Show("Пользователь с таким логином уже существует!");
                return false;
            }

            if (!Regex.Match(phoneTxt, @"^[\d]{11}$").Success)
            {
                MessageBox.Show("Телефон должен быть длинной 11 цифр без символов.");
                return false;
            }
            if (!Regex.Match(emailTxt, @"^[a-z\d.]{1,63}[a-z\d]@[a-z\d]{1,63}\.[a-z]{1,192}$").Success)
            {
                MessageBox.Show("Неверный формат Email!");
                return false;
            }

            return true;
        }

        private void ChangeRole(object sender, SelectionChangedEventArgs e)
        {
            string roleName = (role.SelectedItem as ComboBoxItem).Content.ToString();
            _user.Role = AppData.Model.Roles.Where(x => x.Name == roleName).Select(x => x.ID).FirstOrDefault();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            if (AppData.CurrentUser == null)
            {
                AppData.MainFrame.Navigate(new Auth());
                return;
            }
            AppData.RollBack();
            AppData.MainFrame.Navigate(new EditUsers());
        }
    }
}

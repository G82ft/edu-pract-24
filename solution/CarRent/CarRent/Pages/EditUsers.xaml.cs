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
        private bool rendered = false;
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

        private void ChangeRole(object sender, SelectionChangedEventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            int ID = int.Parse(((cb.Parent as DockPanel).Children[0] as TextBlock).Text);
            Users user = AppData.Model.Users.Where(x => x.ID == ID).FirstOrDefault();
            string roleName = (cb.SelectedItem as ComboBoxItem).Content.ToString();
            Console.WriteLine(roleName);
            int role = AppData.Model.Roles.Where(x => x.Name == roleName).Select(x => x.ID).FirstOrDefault();
            Console.WriteLine(role);
            user.Role = role;
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
        public static List<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            List<T> list = new List<T>();
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        list.Add((T)child);
                    }

                    List<T> childItems = FindVisualChildren<T>(child);
                    if (childItems != null && childItems.Count() > 0)
                    {
                        foreach (var item in childItems)
                        {
                            list.Add(item);
                        }
                    }
                }
            }
            return list;
        }

        private void ScrollViewer_MouseMove(object sender, MouseEventArgs e)
        {
            if (rendered) return;
            foreach (var child in FindVisualChildren<ComboBox>(listView))
            {
                int id = int.Parse(((child.Parent as DockPanel).Children[0] as TextBlock).Text);

                child.Text = AppData.Model.Users.Where(x => x.ID == id).FirstOrDefault().Roles.Name;
            }
            rendered = true;
        }
    }
}

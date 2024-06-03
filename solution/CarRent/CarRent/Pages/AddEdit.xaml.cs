using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Логика взаимодействия для AddEdit.xaml
    /// </summary>
    public partial class AddEdit : Page
    {
        public List<string> manufacturers { get; set; }
        public List<string> models { get; set; }
        public Cars car { get; set; }
        private bool _new;
        public List<string> images { get; set; } = new List<string>()
        {
            "audi.jpg",
            "bmw.jpg",
            "ford.jpg",
            "ford150.jpg"
        };
        public AddEdit(Cars car, bool _new = false)
        {
            manufacturers = AppData.Model.Manufacturers.Select(x => x.Name).ToList();
            models = AppData.Model.Models.Select(x => x.Name).ToList();

            this.car = car;
            this._new = _new;

            DataContext = this;
            
            InitializeComponent();

            delete.Visibility = _new ? Visibility.Collapsed : Visibility.Visible;

            mfs.SelectedItem = car.Models.Manufacturers.Name;

            mdls.SelectedItem = car.Models.Name;

            mdls.ItemsSource = AppData.Model.Models.Where(x => x.Manufacturers.Name == mfs.SelectedItem).Select(x => x.Name).ToList();

            mileage.PreviewTextInput += AppData.onlyIntPositive;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            if (car.Number != number.Text
                || car.Cost.ToString().Replace(',', '.') != cost.Text
                || car.Mileage.ToString() != mileage.Text)
            {
                MessageBox.Show("Неверный формат данных!");
                return;
            }
            car.Model = AppData.Model.Models.Where(x => x.Name == mdls.SelectedItem).Select(x => x.ID).FirstOrDefault();
            if (_new)
            {
                AppData.Model.Cars.Add(car);
            }
            AppData.Model.SaveChanges();
            AppData.MainFrame.Navigate(new View());
        }

        private void ChangeManufacturer(object sender, SelectionChangedEventArgs e)
        {
            if (!IsInitialized) return;

            mdls.ItemsSource = AppData.Model.Models.Where(x => x.Manufacturers.Name == mfs.SelectedItem).Select(x => x.Name).ToList();
            mdls.SelectedIndex = 0;
            string mf = mfs.SelectedItem as string;
            car.Models.Manufacturer = AppData.Model.Manufacturers.Where(x => x.Name == mf).FirstOrDefault().ID;
        }

        private void Delete(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить авто?", "Подтверждение", MessageBoxButton.YesNo) == MessageBoxResult.No)
            {
                return;
            }

            AppData.Model.Cars.Remove(car);
            try
            {
                AppData.Model.SaveChanges();
            }
            catch (InvalidOperationException)
            {
                MessageBox.Show("Невозможно удалить машину, так как есть заказ с ней.");
            }
            AppData.MainFrame.GoBack();
        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            AppData.RollBack();
            AppData.Model.SaveChanges();
            AppData.MainFrame.Navigate(new View());
        }

        private void ValidateCost(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            string text = textBox.Text.Insert(textBox.CaretIndex, e.Text);
            Console.WriteLine(new Regex(@"^[0-9]{1,9}\.[0-9]{0,2}$").IsMatch(text));
            e.Handled = !new Regex(@"^[0-9]{1,9}\.[0-9]{0,2}$").IsMatch(text);
        }

        private void ValidateNumber(object sender, TextCompositionEventArgs e)
        {
            e.Handled = true;
            TextBox textBox = sender as TextBox;
            int index = textBox.CaretIndex;
            if (textBox.Text.Insert(textBox.CaretIndex, e.Text).Trim().Length > 9)
            {
                return;
            }
            textBox.Text = textBox.Text.Insert(textBox.CaretIndex, e.Text).Trim();
            textBox.CaretIndex = index + 1;
        }
    }
}

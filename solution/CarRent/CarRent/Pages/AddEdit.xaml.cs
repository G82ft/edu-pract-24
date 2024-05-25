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
    /// Логика взаимодействия для AddEdit.xaml
    /// </summary>
    public partial class AddEdit : Page
    {
        public List<string> manufacturers { get; set; }
        public List<string> models { get; set; }
        public Cars car { get; set; }
        public AddEdit(Cars car)
        {
            manufacturers = AppData.Model.Manufacturers.Select(x => x.Name).ToList();
            models = AppData.Model.Models.Select(x => x.Name).ToList();

            this.car = car;

            DataContext = this;
            
            InitializeComponent();

            mfs.SelectedItem = car.Models.Manufacturers.Name;

            mdls.SelectedItem = car.Models.Name;

            mdls.ItemsSource = AppData.Model.Models.Where(x => x.Manufacturers.Name == mfs.SelectedItem).Select(x => x.Name).ToList();
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(car.Mileage.ToString());
            Console.WriteLine(mileage.Text);
            Console.WriteLine(car.Mileage.ToString() != mileage.Text);
            if (car.Number != number.Text
                || car.Cost.ToString().Replace(',', '.') != cost.Text
                || car.Mileage.ToString() != mileage.Text)
            {
                MessageBox.Show("Неверный формат данных!");
                return;
            }
            car.Model = AppData.Model.Models.Where(x => x.Name == mdls.SelectedItem).Select(x => x.ID).FirstOrDefault();
            AppData.Model.SaveChanges();
            AppData.MainFrame.GoBack();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!IsInitialized) return;

            mdls.ItemsSource = AppData.Model.Models.Where(x => x.Manufacturers.Name == mfs.SelectedItem).Select(x => x.Name).ToList();
            mdls.SelectedIndex = 0;
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
                MessageBox.Show("Невозможно удалить машину, так как есть незавершённый заказ.");
            }
            AppData.MainFrame.GoBack();
        }
    }
}

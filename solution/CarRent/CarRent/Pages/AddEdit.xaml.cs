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
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            AppData.Model.SaveChanges();
        }
    }
}

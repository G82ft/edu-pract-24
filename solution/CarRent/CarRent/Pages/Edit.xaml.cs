using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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
    /// Логика взаимодействия для Edit.xaml
    /// </summary>
    public partial class Edit : Page
    {
        public List<string> tables { get; set; }
        public Edit()
        {
            tables = new List<string>()
            {
                "Cars",
                "Manufacturers",
                "Models",
                "Orders",
                "Roles",
                "Users"
            };
            DataContext = this;
            InitializeComponent();
            AppData.Model.Cars.Load();
            dataGrid.ItemsSource = AppData.Model.Cars.Local;
        }

        private void ChangeTable(object sender, SelectionChangedEventArgs e)
        {
            if (!IsInitialized)
            {
                return;
            }

            switch((sender as ComboBox).SelectedValue)
            {
                case "Cars":
                    AppData.Model.Cars.Load();
                    dataGrid.ItemsSource = AppData.Model.Cars.Local;
                    break;
                case "Manufacturers":
                    AppData.Model.Manufacturers.Load();
                    dataGrid.ItemsSource = AppData.Model.Manufacturers.Local;
                    break;
                case "Models":
                    AppData.Model.Models.Load();
                    dataGrid.ItemsSource = AppData.Model.Models.Local;
                    break;
                case "Orders":
                    AppData.Model.Orders.Load();
                    dataGrid.ItemsSource = AppData.Model.Orders.Local;
                    break;
                case "Roles":
                    AppData.Model.Roles.Load();
                    dataGrid.ItemsSource = AppData.Model.Roles.Local;
                    break;
                case "Users":
                    AppData.Model.Users.Load();
                    dataGrid.ItemsSource = AppData.Model.Users.Local;
                    break;
            }
        }

        private void GeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            List<Type> table_types = new List<Type>()
            {
                typeof(Cars),
                typeof(Manufacturers),
                typeof(Models),
                typeof(Orders),
                typeof(Roles),
                typeof(Users)
            };
            foreach (Type type in table_types)
            {
                if (type == e.PropertyType)
                {
                    e.Column = null;
                }
            }
            if (e.PropertyType.IsGenericType && typeof(ICollection<>) == e.PropertyType.GetGenericTypeDefinition())
            {
                e.Column = null;
            }
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            AppData.Model.SaveChanges();
            AppData.MainFrame.GoBack();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            AppData.Model.ChangeTracker.Entries()
            .Where(x => x.Entity != null).ToList()
            .ForEach(x => x.State = EntityState.Detached);
            AppData.MainFrame.GoBack();
        }
    }
}

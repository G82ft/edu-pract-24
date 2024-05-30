using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace CarRent
{
    internal class AppData
    {
        public static Frame MainFrame;
        public static CarRentEntities Model;
        public static Users CurrentUser;
        private static readonly Regex _numeric = new Regex("[^0-9.-]+");
        private static readonly Regex _numericPositive = new Regex("[^0-9.]+");
        private static readonly Regex _int = new Regex("[^0-9-]+");
        private static readonly Regex _intPositive = new Regex("[^0-9]+");
        public static void Refresh()
        {
            Model.ChangeTracker.Entries()
            .Where(x => x.Entity != null).ToList()
            .ForEach(x => x.State = EntityState.Detached);
        }

        public static void onlyNumeric(object sender, TextCompositionEventArgs e)
        {
            e.Handled = _numeric.IsMatch(e.Text);
        }
        public static void onlyNumericPositive(object sender, TextCompositionEventArgs e)
        {
            e.Handled = _numericPositive.IsMatch(e.Text);
        }
        public static void onlyInt(object sender, TextCompositionEventArgs e)
        {
            e.Handled = _intPositive.IsMatch(e.Text);
        }
        public static void onlyIntPositive(object sender, TextCompositionEventArgs e)
        {
            e.Handled = _intPositive.IsMatch(e.Text);
        }
    }
}

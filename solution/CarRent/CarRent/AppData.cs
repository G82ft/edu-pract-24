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
        public static void RollBack()
        {
            var context = Model;
            var changedEntries = context.ChangeTracker.Entries()
                .Where(x => x.State != EntityState.Unchanged).ToList();

            foreach (var entry in changedEntries)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.CurrentValues.SetValues(entry.OriginalValues);
                        entry.State = EntityState.Unchanged;
                        break;
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Unchanged;
                        break;
                }
            }
        }

        public static void onlyNumeric(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            e.Handled = _numeric.IsMatch(textBox.Text.Insert(textBox.CaretIndex, e.Text));
        }
        public static void onlyNumericPositive(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            e.Handled = _numericPositive.IsMatch(textBox.Text.Insert(textBox.CaretIndex, e.Text));
        }
        public static void onlyInt(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            e.Handled = _intPositive.IsMatch(textBox.Text.Insert(textBox.CaretIndex, e.Text));
        }
        public static void onlyIntPositive(object sender, TextCompositionEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            e.Handled = _intPositive.IsMatch(textBox.Text.Insert(textBox.CaretIndex, e.Text));
        }
    }
}

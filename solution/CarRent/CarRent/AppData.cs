using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CarRent
{
    internal class AppData
    {
        public static Frame MainFrame;
        public static CarRentEntities Model;
        public static Users CurrentUser;
        public static void Refresh()
        {
            Model.ChangeTracker.Entries()
            .Where(x => x.Entity != null).ToList()
            .ForEach(x => x.State = EntityState.Detached);
        }
    }
}

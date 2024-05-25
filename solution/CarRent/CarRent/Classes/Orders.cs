
using CarRent.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace CarRent
{
    public partial class Orders
    {
        public decimal TotalCost
        {
            get
            {
                return Cars.Cost * (EndDate - StartDate).Days;
            }
        }
    }
}

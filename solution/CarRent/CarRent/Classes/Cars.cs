﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent
{
    public partial class Cars
    {
        public string ImagePath
        {
            get
            {
                if (Image == null || !File.Exists($"..\\..\\Images\\{Image}"))
                {
                    return $"..\\Images\\def.jpg";
                }
                return $"..\\Images\\{Image}";
            }
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CarRent
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            DirectoryInfo di = new DirectoryInfo(@"..\..\QR\");

            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
        }
    }
}

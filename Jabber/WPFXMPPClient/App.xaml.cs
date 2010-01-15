using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            AppStarter appStarter = new AppStarter(false);
        }
    }
}

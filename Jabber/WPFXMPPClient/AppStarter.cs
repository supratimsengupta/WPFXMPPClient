using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WpfApplication1
{
    /// <summary>
    /// This is the parent class for all the windows in this application
    /// All the transitions between windows will be taken care by this class
    /// It will also listen to the events from the library classes
    /// </summary>
    class AppStarter
    {
        public AppStarter(bool showSplashScreen) 
        {
            if (showSplashScreen)
            {
                // load the splash screen, which will load the login window 
                
            }
            else 
            {
                LoginWindow loginWin = new LoginWindow();
                loginWin.Show();
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;
using Microsoft.Shell;
using SettingsProvider = _3DPrinter.setting.SettingsProvider;

namespace _3DPrinter
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application, ISingleInstanceApp
    {
        private void App_OnExit(object sender, ExitEventArgs e)
        {
            SettingsProvider.Instance.saveToXML("configData.xml");
        }


    private const string Unique = "My_Unique_Application_String";

//    [STAThread]
    [System.STAThreadAttribute()]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    public static void Main()
    {
        if (SingleInstance<App>.InitializeAsFirstInstance(Unique))
        {

            SplashScreen splashScreen = new SplashScreen("images/logo.png");
            splashScreen.Show(true);

            var application = new App();

            application.InitializeComponent();
            application.Run();

            // Allow single instance code to perform cleanup operations
            SingleInstance<App>.Cleanup();
        }
    }

    #region ISingleInstanceApp Members

    public bool SignalExternalCommandLineArgs(IList<string> args)
    {
        // handle command line arguments of second instance
        // …

        return true;
    }

    #endregion

    }
}

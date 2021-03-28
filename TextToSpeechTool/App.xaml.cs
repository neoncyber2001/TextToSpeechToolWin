using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace TextToSpeechTool
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        MainVM vm;
        MainWindow window;
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            window = new MainWindow();
            vm = new MainVM();
            vm.getBindings().ForEach((b) => { window.CommandBindings.Add(b); });
            window.DataContext = vm;
            window.Show();

        }
    }
}

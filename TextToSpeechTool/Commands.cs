using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
namespace TextToSpeechTool
{
    public static class Commands
    {
        public static readonly RoutedUICommand Speek = new RoutedUICommand("Speek", "Speek", typeof(Commands));
        public static readonly RoutedUICommand Stop = new RoutedUICommand("Stop", "Stop", typeof(Commands));
        public static readonly RoutedUICommand Save = new RoutedUICommand("Save", "Save", typeof(Commands));

    }
}

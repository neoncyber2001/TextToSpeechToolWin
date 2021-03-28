using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
namespace Recognizer
{
    public static class VoiceRecogCommands
    {
        public static readonly RoutedUICommand Exit = new RoutedUICommand("Exit", "Exit", typeof(VoiceRecogCommands));

        public static readonly RoutedUICommand ToggleDictation = new RoutedUICommand("Toggle Dictation", "Toggle Dictation", typeof(VoiceRecogCommands));
        public static readonly RoutedUICommand DictateToWindow = new RoutedUICommand("Toggles Dictate To Window", "Dictate To Window", typeof(VoiceRecogCommands));
        public static readonly RoutedUICommand Configure = new RoutedUICommand("Open Configuration", "Configure", typeof(VoiceRecogCommands)); 

        public static readonly RoutedUICommand Accept = new RoutedUICommand("Accept Dictation", "Accept", typeof(VoiceRecogCommands));
        public static readonly RoutedUICommand Reject = new RoutedUICommand("Reject Dictation", "Reject", typeof(VoiceRecogCommands));
    }
}

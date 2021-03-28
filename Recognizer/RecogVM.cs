using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Recognition;
using System.Windows.Input;

namespace Recognizer
{

    public class RecogVM : INotifyPropertyChanged
    {

        private SpeechRecognitionEngine _SpeechEngine;

        public SpeechRecognitionEngine SpeechEngine
        {
            get => _SpeechEngine;
            set
            {
                _SpeechEngine = value;
                NotifyPropertyChanged();
            }
        }


        private bool _IsSpeechOn = false;

        public bool IsSpeechOn
        {
            get => _IsSpeechOn;
            set
            {
                _IsSpeechOn = value;
                NotifyPropertyChanged();
            }
        }

        public List<CommandBinding> getBindings()
        {
            List<CommandBinding> bindingList = new List<CommandBinding>();
            bindingList.Add(new CommandBinding(VoiceRecogCommands.Exit, Exit_Executed, Exit_CanExecute));    
            return bindingList;
        }

        //CommandBindingEventHandlers for Exit
        private void Exit_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //Todo
            e.CanExecute = true;
        }

        private void Exit_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Environment.Exit(0);
        }


        private void NotifyPropertyChanged([CallerMemberName] String PropertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}

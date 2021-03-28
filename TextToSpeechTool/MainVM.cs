using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Speech;
using System.Speech.Synthesis;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Windows.Input;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.Diagnostics;
using System.Globalization;

namespace TextToSpeechTool
{

    public class MainVM : INotifyPropertyChanged
    {
        public MainVM()
        {


            _Synth.SetOutputToDefaultAudioDevice();
            CultureInfo.GetCultures(CultureTypes.InstalledWin32Cultures).ToList().ForEach((Cul) => {
                try
                {
                    Synth.GetInstalledVoices(Cul).ToList().ForEach(voi =>
                    {
                        try
                        {
                            Add_Voices(voi);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine(ex);
                        }
                    });
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            });
/*            _Synth.GetInstalledVoices().ToList().ForEach((iv) =>
            {
            });*/
            Rate = _Synth.Rate;
            Synth.SpeakProgress += Synth_SpeakProgress;
            Synth.SpeakCompleted += Synth_SpeakCompleted;
        }


        private bool isNoAudio = false;
        private void Synth_SpeakCompleted(object sender, SpeakCompletedEventArgs e)
        {
            progress = 1;
            if (!isTextEditEnabled)
            {
                isTextEditEnabled = true;
            }
            if (isNoAudio)
            {
                isNoAudio = false;
            }
        }

        private void Synth_SpeakProgress(object sender, SpeakProgressEventArgs e)
        {
            progress=(double)e.CharacterPosition/(double)Phrase.Length;
            Debug.WriteLine(((double)e.CharacterPosition / (double)Phrase.Length).ToString());
            
        }

        private Prompt pr;
        private bool _isTextEditEnabled=true;

        public bool isTextEditEnabled
        {
            get => _isTextEditEnabled;
            set
            {
                _isTextEditEnabled = value;
                NotifyPropertyChanged();
            }
        }


        private int _Rate;

        public int Rate
        {
            get => _Rate;
            set
            {
                _Rate = value;
                NotifyPropertyChanged();
            }
        }


        private SpeechSynthesizer _Synth= new SpeechSynthesizer();

        public System.Speech.Synthesis.SpeechSynthesizer Synth
        {
            get => _Synth;
            set
            {
                _Synth = value;
                NotifyPropertyChanged();
            }
        }


        private ObservableCollection<InstalledVoice> _Voices = new ObservableCollection<InstalledVoice>();
        public ReadOnlyObservableCollection<InstalledVoice> Voices
        {
            get => new ReadOnlyObservableCollection<InstalledVoice>(_Voices);
        }
        public void Add_Voices(InstalledVoice value)
        {
            _Voices.Add(value);
            NotifyPropertyChanged("Voices");
        }
        public void Remove_Voices(InstalledVoice value)
        {
            _Voices.Remove(value);
            NotifyPropertyChanged("Voices");
        }

        private String _Phrase;

        public String Phrase
        {
            get => _Phrase;
            set
            {
                _Phrase = value;
                NotifyPropertyChanged();
            }
        }

        public List<CommandBinding> getBindings()
        {
            List<CommandBinding> bindinglist = new List<CommandBinding>();
            bindinglist.Add(new CommandBinding(Commands.Speek, Speek_Executed, Speek_CanExecute));
            bindinglist.Add(new CommandBinding(Commands.Stop, Stop_Executed, Stop_CanExecute));
            bindinglist.Add(new CommandBinding(Commands.Save, Save_Executed, Save_CanExecute));
            return bindinglist;
        }

        //CommandBindingEventHandlers for Speek
        private void Speek_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //Todo
            if (Phrase != String.Empty && Phrase != null && e.Parameter!=null && !isNoAudio)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }

        private void Speek_Executed(object sender, ExecutedRoutedEventArgs e)
        {

            if (Synth.State == SynthesizerState.Ready) {
                isTextEditEnabled = false;
                InstalledVoice vi = e.Parameter as InstalledVoice;
                
                Synth.SelectVoice(vi.VoiceInfo.Name);
                Synth.Rate = _Rate;
                pr = Synth.SpeakAsync(Phrase);
                NotifyPropertyChanged("Synth");
            }
            else if (Synth.State == SynthesizerState.Speaking) {
                Synth.Pause();
                NotifyPropertyChanged("Synth");
            }
            else if(Synth.State == SynthesizerState.Paused)
            {
                Synth.Resume();
                NotifyPropertyChanged("Synth");
            }
            
        }
        //CommandBindingEventHandlers for Stop
        private void Stop_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //Todo
            if (Synth.State!=SynthesizerState.Ready)
            {
                e.CanExecute = true;
            }
            else
            {
                e.CanExecute = false;
            }
        }

        private void Stop_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            _Synth.SpeakAsyncCancel(pr);
            if (!isTextEditEnabled)
            {
                isTextEditEnabled = true;
            }
            if (isNoAudio)
            {
                isNoAudio = false;
            }

            NotifyPropertyChanged("Synth");
        }
        //CommandBindingEventHandlers for Save
        private void Save_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            //Todo
            if (Phrase != String.Empty && Phrase != null && e.Parameter != null)
            {
                if (Synth.State != SynthesizerState.Speaking)
                {
                    e.CanExecute = true;
                }
                else
                {
                    e.CanExecute = false;
                }
            }
            else
            {
                e.CanExecute = false;
            }
        }

        private void Save_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Wav|*.wav";
            if (dlg.ShowDialog()==true)
            {
                isNoAudio = true;
                Synth.SetOutputToWaveFile(dlg.FileName);
                InstalledVoice vi = e.Parameter as InstalledVoice;
                Synth.SelectVoice(vi.VoiceInfo.Name);
                Synth.Rate = _Rate;
                pr=Synth.SpeakAsync(Phrase);
                
            }
            //isTextEditEnabled = false;

        }



        private double _progress;

        public double progress
        {
            get => _progress;
            set
            {
                _progress = value;
                NotifyPropertyChanged();
            }
        }


        private void NotifyPropertyChanged([CallerMemberName] String PropertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}

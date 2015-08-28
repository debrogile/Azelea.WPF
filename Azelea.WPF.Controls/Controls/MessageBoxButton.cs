using System;
using System.Windows.Input;

namespace Azelea.WPF.Controls
{
    internal class MessageBoxButton
    {
        public string Text { get; set; }
        public bool? Result { get; set; }

        public ICommand Command
        {
            get
            {
                var action = new Action<object>(x => 
                {
                    var win = (MessageBoxWindow)x;
                    win.DialogResult = Result;
                    if (Result == null)
                    {
                        win.Close();
                    }   
                });

                return new RelayCommand(action);
            }
        }
    }
}

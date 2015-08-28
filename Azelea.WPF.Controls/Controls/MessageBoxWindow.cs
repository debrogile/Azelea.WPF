using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Azelea.WPF.Controls
{
    [TemplatePart(Name = "PART_Close", Type = typeof(Button))]
    internal class MessageBoxWindow : Window
    {
        static MessageBoxWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MessageBoxWindow), new FrameworkPropertyMetadata(typeof(MessageBoxWindow)));
            AllowsTransparencyProperty.OverrideMetadata(typeof(MessageBoxWindow), new FrameworkPropertyMetadata(true));
            WindowStyleProperty.OverrideMetadata(typeof(MessageBoxWindow), new FrameworkPropertyMetadata(WindowStyle.None));
        }

        private Button _Close;

        #region DependencyProperty

        #region Message

        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(MessageBoxWindow));

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }

        #endregion

        #region Buttons

        public static readonly DependencyProperty ButtonsProperty =
            DependencyProperty.Register("Buttons", typeof(List<MessageBoxButton>), typeof(MessageBoxWindow));

        public List<MessageBoxButton> Buttons
        {
            get { return (List<MessageBoxButton>)GetValue(ButtonsProperty); }
            set { SetValue(ButtonsProperty, value); }
        }

        #endregion

        #endregion

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _Close = GetTemplateChild("PART_Close") as Button;
            _Close.Click += delegate 
            {
                DialogResult = null;
                Close();
            };
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            DragMove();
        }
    }
}

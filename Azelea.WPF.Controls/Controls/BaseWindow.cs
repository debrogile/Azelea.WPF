using System;
using System.Windows;
using System.Windows.Controls;

namespace Azelea.WPF.Controls
{
    [TemplatePart(Name = "PART_Header", Type = typeof(Panel))]
    public class BaseWindow : Window
    {
        static BaseWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BaseWindow), new FrameworkPropertyMetadata(typeof(BaseWindow)));
        }

        //private FrameworkElement _Header;

        #region DependencyProperty

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(Panel), typeof(BaseWindow));

        public Panel Header
        {
            get { return (Panel)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        public static readonly DependencyProperty ToolsProperty =
            DependencyProperty.Register("Tools", typeof(Panel), typeof(BaseWindow));

        public Panel Tools
        {
            get { return (Panel)GetValue(ToolsProperty); }
            set { SetValue(ToolsProperty, value); }
        }

        #endregion

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }
    }
}

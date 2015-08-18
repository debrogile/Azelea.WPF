using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace Azelea.WPF.Controls
{
    [TemplatePart(Name = "PART_Header", Type = typeof(Panel))]
    [TemplatePart(Name = "PART_Min", Type = typeof(Button))]
    [TemplatePart(Name = "PART_Restore", Type = typeof(Button))]
    [TemplatePart(Name = "PART_Close", Type = typeof(Button))]
    [TemplatePart(Name = "PART_SizeLeft", Type = typeof(Rectangle))]
    [TemplatePart(Name = "PART_SizeRight", Type = typeof(Rectangle))]
    [TemplatePart(Name = "PART_SizeTop", Type = typeof(Rectangle))]
    [TemplatePart(Name = "PART_SizeTopLeft", Type = typeof(Rectangle))]
    [TemplatePart(Name = "PART_SizeTopRight", Type = typeof(Rectangle))]
    [TemplatePart(Name = "PART_SizeBottom", Type = typeof(Rectangle))]
    [TemplatePart(Name = "PART_SizeBottomLeft", Type = typeof(Rectangle))]
    [TemplatePart(Name = "PART_SizeBottomRight", Type = typeof(Rectangle))]
    public class BaseWindow : Window
    {
        static BaseWindow()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(BaseWindow), new FrameworkPropertyMetadata(typeof(BaseWindow)));
        }

        private FrameworkElement _Header;
        private Button _Min;
        private Button _Restore;
        private Button _Close;
        private Rectangle _SizeLeft;
        private Rectangle _SizeRight;
        private Rectangle _SizeTop;
        private Rectangle _SizeTopLeft;
        private Rectangle _SizeTopRight;
        private Rectangle _SizeBottom;
        private Rectangle _SizeBottomLeft;
        private Rectangle _SizeBottomRight;

        #region DependencyProperty

        #region CanResize

        public static readonly DependencyProperty CanResizeProperty =
            DependencyProperty.Register("CanResize", typeof(bool), typeof(BaseWindow), new PropertyMetadata(true));

        public bool CanResize
        {
            get { return (bool)GetValue(CanResizeProperty); }
            set { SetValue(CanResizeProperty, value); }
        }

        #endregion

        #region Header

        public static readonly DependencyProperty HeaderProperty =
            DependencyProperty.Register("Header", typeof(Panel), typeof(BaseWindow));

        public Panel Header
        {
            get { return (Panel)GetValue(HeaderProperty); }
            set { SetValue(HeaderProperty, value); }
        }

        #endregion

        #region Tools

        public static readonly DependencyProperty ToolsProperty =
            DependencyProperty.Register("Tools", typeof(Panel), typeof(BaseWindow));

        public Panel Tools
        {
            get { return (Panel)GetValue(ToolsProperty); }
            set { SetValue(ToolsProperty, value); }
        }

        #endregion

        #region TryClose

        public static readonly DependencyProperty TryCloseProperty =
            DependencyProperty.Register("TryClose", typeof(Func<bool>), typeof(BaseWindow),
                new PropertyMetadata(new Func<bool>(() => true)));

        public Func<bool> TryClose
        {
            get { return (Func<bool>)GetValue(TryCloseProperty); }
            set { SetValue(TryCloseProperty, value); }
        }

        #endregion

        #endregion

        public BaseWindow()
        {
            WindowHelper.RepairWindow(this);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            #region WindowState

            _Header = GetTemplateChild("PART_Header") as FrameworkElement;
            _Header.MouseLeftButtonDown += (s, e) => 
            {
                if (e.ClickCount == 1)
                {
                    WindowHelper.SendMessage(WindowHelper.Handle, WindowHelper.WM_SYSCOMMAND, (IntPtr)WindowHelper.SC_MOVEHEADER, IntPtr.Zero);
                }
                else if (CanResize)
                {
                    WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
                }
            };

            _Min = GetTemplateChild("PART_Min") as Button;
            _Min.Click += delegate 
            {
                WindowState = WindowState.Minimized;
            };

            _Restore = GetTemplateChild("PART_Restore") as Button;
            _Restore.Click += delegate
            {
                WindowState = WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
            };

            _Close = GetTemplateChild("PART_Close") as Button;
            _Close.Click += delegate 
            {
                if (TryClose())
                {
                    Close();
                }
            };

            #endregion

            #region Window Resize

            _SizeLeft = GetTemplateChild("PART_SizeLeft") as Rectangle;
            _SizeLeft.MouseLeftButtonDown += delegate
            {
                WindowHelper.SendMessage(WindowHelper.Handle, WindowHelper.WM_SYSCOMMAND, (IntPtr)WindowHelper.SC_SIZE_LEFT, IntPtr.Zero);
            };

            _SizeRight = GetTemplateChild("PART_SizeRight") as Rectangle;
            _SizeRight.MouseLeftButtonDown += delegate
            {
                WindowHelper.SendMessage(WindowHelper.Handle, WindowHelper.WM_SYSCOMMAND, (IntPtr)WindowHelper.SC_SIZE_RIGHT, IntPtr.Zero);
            };

            _SizeTop = GetTemplateChild("PART_SizeTop") as Rectangle;
            _SizeTop.MouseLeftButtonDown += delegate
            {
                WindowHelper.SendMessage(WindowHelper.Handle, WindowHelper.WM_SYSCOMMAND, (IntPtr)WindowHelper.SC_SIZE_TOP, IntPtr.Zero);
            };

            _SizeTopLeft = GetTemplateChild("PART_SizeTopLeft") as Rectangle;
            _SizeTopLeft.MouseLeftButtonDown += delegate
            {
                WindowHelper.SendMessage(WindowHelper.Handle, WindowHelper.WM_SYSCOMMAND, (IntPtr)WindowHelper.SC_SIZE_TOPLEFT, IntPtr.Zero);
            };

            _SizeTopRight = GetTemplateChild("PART_SizeTopRight") as Rectangle;
            _SizeTopRight.MouseLeftButtonDown += delegate
            {
                WindowHelper.SendMessage(WindowHelper.Handle, WindowHelper.WM_SYSCOMMAND, (IntPtr)WindowHelper.SC_SIZE_TOPRIGHT, IntPtr.Zero);
            };

            _SizeBottom = GetTemplateChild("PART_SizeBottom") as Rectangle;
            _SizeBottom.MouseLeftButtonDown += delegate
            {
                WindowHelper.SendMessage(WindowHelper.Handle, WindowHelper.WM_SYSCOMMAND, (IntPtr)WindowHelper.SC_SIZE_BOTTOM, IntPtr.Zero);
            };

            _SizeBottomLeft = GetTemplateChild("PART_SizeBottomLeft") as Rectangle;
            _SizeBottomLeft.MouseLeftButtonDown += delegate
            {
                WindowHelper.SendMessage(WindowHelper.Handle, WindowHelper.WM_SYSCOMMAND, (IntPtr)WindowHelper.SC_SIZE_BOTTOMLEFT, IntPtr.Zero);
            };

            _SizeBottomRight = GetTemplateChild("PART_SizeBottomRight") as Rectangle;
            _SizeBottomRight.MouseLeftButtonDown += delegate
            {
                WindowHelper.SendMessage(WindowHelper.Handle, WindowHelper.WM_SYSCOMMAND, (IntPtr)WindowHelper.SC_SIZE_BOTTOMRIGHT, IntPtr.Zero);
            };

            #endregion
        }
    }
}

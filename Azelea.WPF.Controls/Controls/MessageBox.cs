using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Azelea.WPF.Controls
{
    public sealed class MessageBox
    {
        public static bool? Show(string message)
        {
            return Show(message, "系统提示");
        }

        public static bool? Show(string message, string caption)
        {
            return Show(message, caption, MessageBoxButtonType.OK);
        }

        public static bool? Show(string message, MessageBoxButtonType type)
        {
            return Show(message, "系统提示", MessageBoxButtonType.OK);
        }

        public static bool? Show(string message, string caption, MessageBoxButtonType type)
        {
            var win = new MessageBoxWindow();
            win.Title = caption;
            win.Message = message;

            var buttons = new List<MessageBoxButton>();
            if (type == MessageBoxButtonType.OK)
            {
                buttons.Add(new MessageBoxButton { Text = "确定", Result = true });
            }
            else if (type == MessageBoxButtonType.OKCancel)
            {
                buttons.Add(new MessageBoxButton { Text = "确定", Result = true });
                buttons.Add(new MessageBoxButton { Text = "取消", Result = false });
            }
            else if (type == MessageBoxButtonType.YesNoCancel)
            {
                buttons.Add(new MessageBoxButton { Text = "是", Result = true });
                buttons.Add(new MessageBoxButton { Text = "否", Result = false });
                buttons.Add(new MessageBoxButton { Text = "取消" });
            }
            else if (type == MessageBoxButtonType.YesNo)
            {
                buttons.Add(new MessageBoxButton { Text = "是", Result = true });
                buttons.Add(new MessageBoxButton { Text = "否", Result = false });
            }
            win.Buttons = buttons;

            var owner = Application.Current.Windows.OfType<Window>().FirstOrDefault(x => x.IsActive);
            if (owner != null)
            {
                win.Owner = owner;
                win.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            }

            return win.ShowDialog();
        }
    }
}

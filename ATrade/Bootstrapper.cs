using Azelea.WPF.TradeClient.ViewModels;
using Caliburn.Micro;
using System.Dynamic;
using System.Windows;

namespace Azelea.WPF.TradeClient
{
    public class Bootstrapper : BootstrapperBase
    {
        public Bootstrapper()
        {
            Initialize();
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            dynamic setting = new ExpandoObject();
            setting.Title = "系统登录";
            setting.Width = 800;
            setting.Height = 600;
            setting.WindowStartupLocation = WindowStartupLocation.CenterScreen;

            DisplayRootViewFor<LoginViewModel>(setting);
        }
    }
}

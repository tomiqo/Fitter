using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Fitter.App.ViewModels;
using Fitter.App.Views;

namespace Fitter.App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("cs");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("cs");
        }
    }
}

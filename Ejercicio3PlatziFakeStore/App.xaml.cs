using Ejercicio3PlatziFakeStore.ViewModels;
using Ejercicio3PlatziFakeStore.Views;

namespace Ejercicio3PlatziFakeStore
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainView());
        }
    }
}

using Ejercicio2_RickAndMorty.Views;

namespace Ejercicio2_RickAndMorty
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage =  new NavigationPage(new MainView());
        }
    }
}
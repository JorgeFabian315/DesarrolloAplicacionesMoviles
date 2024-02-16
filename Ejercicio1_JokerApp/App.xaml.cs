using Ejercicio1_JokerApp.Views;

namespace Ejercicio1_JokerApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainView();
        }
    }
}
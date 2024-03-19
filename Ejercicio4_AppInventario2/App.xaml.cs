using Ejercicio4_AppInventario2.Views;

namespace Ejercicio4_AppInventario2
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage( new MainView());
        }
    }
}

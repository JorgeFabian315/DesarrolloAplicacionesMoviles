using Ejercicio3PlatziFakeStore.Views;

namespace Ejercicio3PlatziFakeStore
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AgregarProductoView), typeof(AgregarProductoView));
        }
    }
}

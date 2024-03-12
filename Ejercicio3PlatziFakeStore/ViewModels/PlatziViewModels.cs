using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Ejercicio3PlatziFakeStore.Models.Dtos;
using Ejercicio3PlatziFakeStore.Services;
using Ejercicio3PlatziFakeStore.Views;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Ejercicio3PlatziFakeStore.ViewModels
{
    public partial class PlatziViewModels : ObservableObject
    {
        EditarProductoView _editarView;
        EliminarProductoView _eliminarView;

        public PlatziViewModels()
        {
            Iniciar();
        }

        private void Iniciar()
        {
            CargarCategorias();
            CargarProductos();
        }

        public ObservableCollection<CategoryDto> Categorias { get; set; } = new();
        public ObservableCollection<ProductoDto> Productos { get; set; } = new();
        public CategoryDto Categoria { get; set; } = new();

        [ObservableProperty]
        public ProductoDto producto = new();

        private ApiService api = new();


        [RelayCommand]
        private void VistaEditarProducto(ProductoDto p)
        {
            Producto = p;
            CambiarVista(_editarView = new());
        }

        [RelayCommand]
        private void VistaEliminarProducto(ProductoDto p)
        {
            Producto = p;
            CambiarVista(_eliminarView = new());
        }

        [RelayCommand]
        async Task EditarProducto()
        {
            if (Producto != null)
            {
                Producto.Images.Clear();
                Producto.Images.Add("https://placeimg.com/640/480/any");
                Producto.Images.Add("https://placeimg.com/640/480/any");

                if (await api.EditarProducto(Producto))
                {
                    CargarProductos();
                    CambiarVista();
                }
            }

        }

        [RelayCommand]
        private async Task Cancelar()
        {
            await Application.Current.MainPage.Navigation.PopToRootAsync();
        }

        private async Task FiltrarProductosCategoria(int id)
        {
            Productos.Clear();

            var p = await api.FiltrarProductosByCategoria(id);

            foreach (var item in p.OrderByDescending(x => x.CreationAt))
            {
                Productos.Add(item);
            }

        }

        [RelayCommand]
        private async Task EliminarProducto()
        {
            if (await api.DeleteProduct(Producto.Id))
            {
                Productos.Remove(Producto);
                CambiarVista();
            }
        }

        [RelayCommand]
        async Task AgregarProducto()
        {
            Producto.CategoryId = Categoria.Id;

            if (await api.CreateProduct(Producto))
            {
                Producto.CreationAt = DateTime.Now;
                Productos.Add(Producto);

                await Cancelar();
            }

        }

        [RelayCommand]
        void VistaAgregar()
        {
            Producto = new();
            Producto.Images.Add("https://placeimg.com/640/480/any");
            Producto.Images.Add("https://placeimg.com/640/480/any");

            CambiarVista(new AgregarProductoView());
        }

        async void CargarCategorias()
        {
            var l = await api.GetCategorias();

            foreach (CategoryDto item in l)
            {
                Categorias.Add(item);
            }

        }

        async void CargarProductos()
        {
            Productos.Clear();

            var p = await api.GetProductos();

            foreach (var item in p.OrderByDescending(x => x.CreationAt))
            {
                Productos.Add(item);
            }
        }

        async void CambiarVista(ContentPage vm = null)
        {
            if (vm != null)
            {
                vm.BindingContext = this;
                await App.Current.MainPage.Navigation.PushAsync(vm);
            }
            else
                await App.Current.MainPage.Navigation.PopToRootAsync();
        }

    }
}

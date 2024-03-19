using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Ejercicio4_AppInventario2.Data;
using Ejercicio4_AppInventario2.Models;
using Ejercicio4_AppInventario2.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Ejercicio4_AppInventario2.ViewModels
{
    public partial class ArticulosViewModel : ObservableObject
    {
        public ObservableCollection<Articulo> Articulos { get; set; } = new();
        private ArticulosDbContext _context = new ArticulosDbContext();

        [ObservableProperty]
        private Articulo articulo;
        public ArticulosViewModel()
        {
            new Thread(CargarArticulos) { IsBackground = true }.Start();
        }

        async Task<bool> EliminarTodos()
        {
            return await _context.EliminarTodos();
        }

        private async void CargarArticulos()
        {
            var timer = new Stopwatch();
            timer.Start();
            Articulos.Clear();
            var datos = await _context.GetAll();
            foreach (var item in datos.OrderBy(x => x.Descripcion))
            {
                Articulos.Add(item);
            }
            timer.Stop();
            var tiempo = timer.ElapsedMilliseconds;

        }

        [RelayCommand]
        public async Task VistaAgregar()
        {
            Articulo = new();
            await CambiarVista(new AgregarView());
        }

        [RelayCommand]
        private async Task Agregar()
        {
            if (await _context.Agregar(Articulo))
            {
                await CambiarVista();
                CargarArticulos();
            }
        }

        [RelayCommand]
        public async Task VistaEditar(Articulo articuloP)
        {
            Articulo clon = new Articulo()
            {
                Descripcion = articuloP.Descripcion,
                Exitencia = articuloP.Exitencia,
                Id = articuloP.Id,
                Precio = articuloP.Precio
            };

            Articulo = clon;

            await CambiarVista(new EditarView());
        }

        [RelayCommand]

        public async Task Editar()
        {
            if (await _context.Editar(Articulo))
            {
                CargarArticulos();
                await CambiarVista();
            }
        }

        [RelayCommand]
        public async Task Eliminar(Articulo a)
        {
            await _context.Eliminar(a.Id);
            CargarArticulos();
        }


        [RelayCommand]
        public void FiltrarArticulos(string buscar)
        {
            CargarArticulos();
            var datosfiltrados = Articulos
                .Where(x => (!string.IsNullOrWhiteSpace(x.Descripcion) && x.Descripcion.StartsWith(buscar, StringComparison.OrdinalIgnoreCase))
                || x.Precio.ToString().StartsWith(buscar, StringComparison.OrdinalIgnoreCase))
                .ToList();
            Articulos.Clear();
            foreach (var item in datosfiltrados)
            {
                Articulos.Add(item);
            }
        }

        private async Task CambiarVista(ContentPage vista = null)
        {
            if (vista != null)
            {
                vista.BindingContext = this;
                await Application.Current.MainPage.Navigation.PushAsync(vista);
            }
            else
                await Application.Current.MainPage.Navigation.PopToRootAsync();

        }

    }
}

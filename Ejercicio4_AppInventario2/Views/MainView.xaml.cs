using Ejercicio4_AppInventario2.Data;
using Ejercicio4_AppInventario2.Models;
using Ejercicio4_AppInventario2.ViewModels;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;

namespace Ejercicio4_AppInventario2.Views;

public partial class MainView : ContentPage
{
    public MainView()
    {
        InitializeComponent();
    }

    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        SearchBar searchBar = (SearchBar)sender;

    }
    private async void btnEliminar_Clicked(object sender, EventArgs e)
    {
        var button = (sender as ImageButton);
        Articulo articuloselecionado = button.CommandParameter as Articulo;
        var vm = this.BindingContext as ArticulosViewModel;
        if (articuloselecionado != null)
        {
            var respuesta = await DisplayAlert("Eliminar articulo", $"¿Desea eliminar este articulo {articuloselecionado.Descripcion}?", "Si", "No");

            if (respuesta && vm is not null)
                await vm.EliminarCommand.ExecuteAsync(articuloselecionado);
        }
    }
}
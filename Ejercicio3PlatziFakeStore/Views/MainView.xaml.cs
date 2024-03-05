using Ejercicio3PlatziFakeStore.Models.Dtos;
using Ejercicio3PlatziFakeStore.Services;
using Ejercicio3PlatziFakeStore.ViewModels;

namespace Ejercicio3PlatziFakeStore.Views;

public partial class MainView : ContentPage
{
	public MainView()
	{
		InitializeComponent();

	}

    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
		var context = this.BindingContext as PlatziViewModels;

		var picker = sender as Picker;
		var categoria = (CategoryDto)picker.SelectedItem;

        context.FiltrarProductosCategoriasCommand.Execute(categoria.Id);

    }
}
using CommunityToolkit.Mvvm.Input;
using Ejercicio1_JokerApp.Services;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ejercicio1_JokerApp.ViewModels
{
    public class JokeViewModel : INotifyPropertyChanged
    {
        private JokeAPI api;
        public List<string> Categories { get; set; }
        public string Category { get; set; } = "any";

        public ICommand MostrarCommnad { get; set; }
        public string Joke { get; private set; } = "Hola";

        private async Task LlenarCategorias()
        {
            api = new();

            Categories = await api.GetCategorias();

            OnPropertyChanged(nameof(Categories));

        }
        public JokeViewModel()
        {
            LlenarCategorias();

            MostrarCommnad = new AsyncCommand(Mostrar);
        }



        public async Task Mostrar()
        {
            Joke = await api.GetJoke(Category);

            OnPropertyChanged(nameof(Joke));
        }




        public void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }





        public event PropertyChangedEventHandler PropertyChanged;
    }
}

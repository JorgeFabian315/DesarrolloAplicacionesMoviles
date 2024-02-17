using Ejercicio2_RickAndMorty.Dtos;
using Ejercicio2_RickAndMorty.Services;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Ejercicio2_RickAndMorty.ViewModel
{
    public class RickAndMortyViewModel : INotifyPropertyChanged
    {
        public List<PersonajeDto> Personajes { get; set; } = new();

        RickAndMortyServices api = new();
        public ICommand VerPersonajeCommand { get; set; }

        public RickAndMortyViewModel()
        {
            Cargar();

            VerPersonajeCommand = new AsyncCommand(VerPersonaje);
        }

        private async Task VerPersonaje()
        {

        }

        public async Task Cargar()
        {
            Personajes = await api.GetAll();

            OnPropertyChanged(nameof(Personajes));
        }


        public void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

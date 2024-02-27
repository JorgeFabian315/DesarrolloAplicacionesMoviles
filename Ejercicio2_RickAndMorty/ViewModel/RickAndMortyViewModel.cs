using Ejercicio2_RickAndMorty.Dtos;
using Ejercicio2_RickAndMorty.Services;
using Ejercicio2_RickAndMorty.Views;
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
        public ObservableCollection<PersonajeDto> Personajes { get; set; } = new();
        public List<Ubicacion> Ubicaciones { get; set; } = new();
        public List<Episodio> Episodios { get; set; } = new();
        public PersonajeDto PersonajeActual { get; set; } = new();

        public bool Internet { get; set; } = true;

        RickAndMortyServices api = new();
        public ICommand VerPersonajeCommand { get; set; }

        public RickAndMortyViewModel()
        {
            Cargar();

            VerPersonajeCommand = new AsyncCommand<PersonajeDto>(VerPersonaje);
        }

        private async Task VerPersonaje(PersonajeDto p)
        {
            PersonajeActual = p;
            var vista = new PersonajeView()
            {
                BindingContext = this
            };
            await Application.Current.MainPage.Navigation.PushAsync(vista);

            OnPropertyChanged(nameof(PersonajeActual));
        }

        public async Task Cargar()
        {

            if (Connectivity.Current.NetworkAccess == NetworkAccess.Internet)
            {
                var personajes = await api.GetAllCharacters();

                foreach (var p in personajes)
                {
                    Personajes.Add(p);
                }

                Ubicaciones = await api.GetAllLocations();
                Episodios = await api.GetAllEpisodes();

                OnPropertyChanged(nameof(Ubicaciones));

                OnPropertyChanged(nameof(Episodios));

                Internet = true;
            }
            else
            {
                Internet = false;
            }


            OnPropertyChanged(nameof(Internet));
        }


        public void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

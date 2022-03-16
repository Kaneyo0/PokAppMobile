using System;
using System.Collections.Generic;
using System.Text;
using PokeApiNet;
using PokApp.Models;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Threading.Tasks;

namespace PokApp.ViewModel
{
    class PokemonViewModel : BaseViewModel
    {
        private static PokemonViewModel _instance = new PokemonViewModel();
        public static PokemonViewModel instance { get { return _instance; } }
        PokeApiClient pokeClient = new PokeApiClient();
        public ObservableCollection<PokApp.Models.Pokemon> Pokemons
        {
            get { return GetValue<ObservableCollection<PokApp.Models.Pokemon>>(); }
            set { SetValue(value); }
        }

        public PokemonViewModel()
        {
            Pokemons = new ObservableCollection<Models.Pokemon>();

            Device.BeginInvokeOnMainThread(async () =>
            {
                for (int i = 1; i < 50; i++)
                {
                    var Types = new List<string>();
                    var Abilities = new List<string>();
                    var pokemonList = new List<Models.Pokemon>();
                    var PokemonApi = await Task.Run(() => pokeClient.GetResourceAsync<PokeApiNet.Pokemon>(i));
                    foreach(PokemonType Type in PokemonApi.Types)
                    {
                        Types.Add(Type.Type.Name);
                    }
                    foreach (PokemonAbility Ability in PokemonApi.Abilities)
                    {
                        Abilities.Add(Ability.Ability.Name);
                    }
                    await App.Database.SavePokemonAsync(new Models.Pokemon
                    {
                        Name = PokemonApi.Name,
                        Picture = PokemonApi.Sprites.FrontDefault,
                        Height = PokemonApi.Height,
                        Weight = PokemonApi.Weight,
                        Types = Types,
                        Abilities = Abilities,
                    });
                }

            });

        }
    }
}

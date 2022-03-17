using PokApp.Models;
using PokeApiNet;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PokApp.ViewModel
{
    class PokemonViewModel : BaseViewModel
    {
        private static PokemonViewModel _instance = new PokemonViewModel();
        public static PokemonViewModel instance { get { return _instance; } }
        PokeApiClient pokeClient = new PokeApiClient();
        public ObservableCollection<Models.Pokemon> Pokemons
        {
            get { return GetValue<ObservableCollection<PokApp.Models.Pokemon>>(); }
            set { SetValue(value); }
        }

        public PokemonViewModel()
        {
            Pokemons = new ObservableCollection<Models.Pokemon>();

            if (App.Database.GetPokemonAsync().Result.Count == 0)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    for (int i = 1; i <= 50; i++)
                    {
                        var TypeSecondaire = "";
                        var Abilities = new List<string>();
                        var pokemonList = new List<Models.Pokemon>();
                        var PokemonApi = await Task.Run(() => pokeClient.GetResourceAsync<PokeApiNet.Pokemon>(i));
                        if (PokemonApi.Types.Count == 2)
                        {
                            TypeSecondaire = PokemonApi.Types[1].Type.Name;
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
                            TypePrincipal = PokemonApi.Types[0].Type.Name,
                            TypeSecondaire = TypeSecondaire,
                            Abilities = Abilities,
                            HP = PokemonApi.Stats[0].BaseStat,
                            Atk = PokemonApi.Stats[1].BaseStat,
                            Def = PokemonApi.Stats[2].BaseStat,
                        });
                    }
                });
            }
        }
    }
}

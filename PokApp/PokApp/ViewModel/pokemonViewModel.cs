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
        //Permet d'utiliser la PokéApi
        PokeApiClient pokeClient = new PokeApiClient();
        public ObservableCollection<Models.Pokemon> Pokemons
        {
            get { return GetValue<ObservableCollection<PokApp.Models.Pokemon>>(); }
            set { SetValue(value); }
        }

        public PokemonViewModel()
        {
            Pokemons = new ObservableCollection<Models.Pokemon>();

            //Si la table est vide
            if (App.Database.GetPokemonAsync().Result.Count == 0)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    int NombrePokemon = 50;
                    //Récupère et sauvegarde 50 Pokémons dans la base de données
                    for (int pokemonId = 1; pokemonId <= NombrePokemon; pokemonId++)
                    {
                        var TypeSecondaire = "";
                        var TalentSecondaire = "";
                        var TalentSecrete = "";
                        var pokemonList = new List<Models.Pokemon>();
                        var PokemonApi = await Task.Run(() => pokeClient.GetResourceAsync<PokeApiNet.Pokemon>(pokemonId));
                        PokemonSpecies Species = Task.Run(() => pokeClient.GetResourceAsync(PokemonApi.Species)).Result;
                        //Type Types = Task.Run(() => pokeClient.GetResourceAsync<Type>(0)).Result;
                        if (PokemonApi.Types.Count == 2)
                        {
                            TypeSecondaire = PokemonApi.Types[1].Type.Name;
                        }
                        if (PokemonApi.Abilities.Count > 1)
                        {
                            TalentSecondaire = PokemonApi.Abilities[1].Ability.Name;
                        }
                        if (PokemonApi.Abilities.Count > 2)
                        {
                            TalentSecrete = PokemonApi.Abilities[2].Ability.Name;
                        }
                        //Sauvegarde du Pokemon
                        await App.Database.SavePokemonAsync(new Models.Pokemon
                        {
                            Name = Species.Names.Find(name => name.Language.Name.Equals("fr")).Name,
                            Picture = PokemonApi.Sprites.FrontDefault,
                            Color = Species.Color.Name,
                            Height = PokemonApi.Height,
                            Weight = PokemonApi.Weight,
                            TypePrincipal = PokemonApi.Types[0].Type.Name,
                            TypeSecondaire = TypeSecondaire,
                            TalentPrincipal = PokemonApi.Abilities[0].Ability.Name,
                            TalentSecondaire = TalentSecondaire,
                            TalentSecrete = TalentSecrete,
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

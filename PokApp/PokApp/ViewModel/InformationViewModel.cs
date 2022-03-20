using PokeApiNet;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PokApp.ViewModel
{
    class InformationViewModel : BaseViewModel
    {
        private static InformationViewModel _instance = new InformationViewModel();
        public static InformationViewModel instance { get { return _instance; } }
        
        //Permet d'utiliser la PokéApi
        PokeApiClient pokeClient = new PokeApiClient();

        public InformationViewModel()
        {
            //Si la table est vide
            if (App.Database.GetTypesCollectionAsync().Result.Count == 0)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var Types = new PokeApiNet.Type();
                    int NombreTypes = 18;
                    //Enregistre les Types
                    for (int indexType = 1; indexType <= NombreTypes; indexType++)
                    {
                        Types = Task.Run(() => pokeClient.GetResourceAsync<PokeApiNet.Type>(indexType)).Result;
                        await App.Database.SaveTypeAsync(new Models.CollectionTypes
                        {
                            Name = Types.Name,
                        });
                    }
                });
            }

            //Si la table est vide
            if (App.Database.GetColorCollectionAsync().Result.Count == 0)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var Color = new PokemonColor();
                    int NombreCouleur = 10;
                    //Enregistre les couleurs
                    for (int indexColor = 1; indexColor <= NombreCouleur; indexColor++)
                    {
                        Color = Task.Run(() => pokeClient.GetResourceAsync<PokemonColor>(indexColor)).Result;
                        await App.Database.SaveColorAsync(new Models.CollectionColor
                        {
                            Color = Color.Name,
                        });
                    }
                });
            }
        }
    }
}

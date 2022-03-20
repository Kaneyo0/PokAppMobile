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
        public ObservableCollection<Models.CollectionTypes> AllTypes
        {
            get { return GetValue<ObservableCollection<Models.CollectionTypes>>(); }
            set { SetValue(value); }
        }

        public ObservableCollection<Models.CollectionColor> AllSpecies
        {
            get { return GetValue<ObservableCollection<Models.CollectionColor>>(); }
            set { SetValue(value); }
        }

        public InformationViewModel()
        {
            AllTypes = new ObservableCollection<Models.CollectionTypes>();
            AllSpecies = new ObservableCollection<Models.CollectionColor>();

            if (App.Database.GetTypesCollectionAsync().Result.Count == 0)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var Types = new PokeApiNet.Type();
                    for (int indexType = 1; indexType < 19; indexType++)
                    {
                        Types = Task.Run(() => pokeClient.GetResourceAsync<PokeApiNet.Type>(indexType)).Result;
                        await App.Database.SaveTypeAsync(new Models.CollectionTypes
                        {
                            Name = Types.Name,
                        });
                    }
                });
            }

            if (App.Database.GetColorCollectionAsync().Result.Count == 0)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    var Color = new PokemonColor();
                    for (int indexColor = 1; indexColor <= 10; indexColor++)
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

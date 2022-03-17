using PokApp.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PokApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DescriptionPage : ContentPage
    {
        public DescriptionPage(Pokemon Pokemon)
        {
            InitializeComponent();
            BindingContext = Pokemon;
        }

        async void DeletePokemon(object sender)
        {
            await App.database.DeletePokemonAsync(PokemonName.ToString());
        }
    }
}
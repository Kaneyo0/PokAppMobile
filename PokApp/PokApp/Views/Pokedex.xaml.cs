using PokApp.Models;
using PokApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PokApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class Pokedex : ContentPage
    {
        public Pokedex()
        {
            InitializeComponent();
            BindingContext = PokemonViewModel.instance;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            listview.ItemsSource = await App.Database.GetPokemonAsync();
        }

        async void OnClick(object Sender, ItemTappedEventArgs e)
        {
            Pokemon CurrentPokemon = e.Item as Pokemon;
            await Navigation.PushAsync(new DescriptionPage(CurrentPokemon));
        }
    }
}
using PokApp.Models;
using System;
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
        //Supprime le Pokemon actuel grâce à son Id
        async void DeletePokemon(object sender, EventArgs e)
        {
            var IdPokemon = App.database.GetOnePokemonAsync(int.Parse(PokemonId.Text));
            await App.database.DeletePokemonAsync(IdPokemon);
            await Application.Current.MainPage.Navigation.PushAsync(new Pokedex(), true);
        }
    }
}
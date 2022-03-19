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
        async void DeletePokemon(object sender, System.EventArgs e)
        {
            await App.database.DeletePokemonAsync(Int32.Parse(PokemonId.Text));
        }
    }
}
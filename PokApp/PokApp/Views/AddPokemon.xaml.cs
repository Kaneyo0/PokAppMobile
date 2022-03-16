using Plugin.Media;
using Plugin.Media.Abstractions;
using PokApp.Models;
using PokApp.ViewModel;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PokApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPokemon : ContentPage
    {
        MediaFile mediaFile;
        public AddPokemon()
        {
            InitializeComponent();
        }

        //Actualise le texte des sliders
        void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            if (sender == heightSlider)
            {
                heightLabel.Text = String.Format("Taille = {0:F2} cm", (int)args.NewValue);
            }
            else if (sender == weightSlider)
            {
                weightLabel.Text = String.Format("Poids = {0:F2} kg", (int)args.NewValue);
            }

            else if (sender == atkSlider)
            {
                atkLabel.Text = String.Format("Atk = {0:F2}", (int)args.NewValue);
            }

            else if (sender == defSlider)
            {
                defLabel.Text = String.Format("Def = {0:F2}", (int)args.NewValue);
            }
        }

        //Ajoute un Pokemon dans la base de donnée
        async void OnButtonClicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(PokemonNameEntry.Text))
            {
                string[] imageSource = PokemonImage.Source.ToString().Split(':');
                await App.Database.SavePokemonAsync(new Pokemon
                {
                    Name = PokemonNameEntry.Text,
                    Picture = imageSource[1],
                    Height = Convert.ToInt32(heightSlider.Value),
                    Weight = Convert.ToInt32(weightSlider.Value),
                });

                await DisplayAlert("Félicitations !", "Votre Pokémon a correctement été ajouté.", "OK");

                PokemonNameEntry.Text = string.Empty;
                PokemonImage.Source = "";
                heightSlider.Value = 0.0;
                weightSlider.Value = 0.0;
                atkSlider.Value = 0.0;
                defSlider.Value = 0.0;
            }
        }

       
        //Ajoute une image depuis la galerie
        private async void AddAttachments(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Erreur", "Votre téléphone ne supporte pas les téléchargement.", "OK");
                return;
            }

            mediaFile = await CrossMedia.Current.PickPhotoAsync();
            PokemonImage.Source = mediaFile.Path;
            AddImageButton.Text = "Changer l'image";

            if (mediaFile == null)
            {
                return;
            }

        }
    }
}
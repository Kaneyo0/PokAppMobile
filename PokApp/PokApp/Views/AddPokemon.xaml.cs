using Plugin.Media;
using Plugin.Media.Abstractions;
using PokApp.Models;
using PokApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            BindingContext = InformationViewModel.instance;
        }

        //Actualise le texte des sliders en temps réel
        void OnSliderValueChanged(object sender, ValueChangedEventArgs args)
        {
            if (sender == heightSlider)
            {
                heightLabel.Text = string.Format("Taille = {0:F2} dm", (float)args.NewValue);
            }

            else if (sender == weightSlider)
            {
                weightLabel.Text = string.Format("Poids = {0:F2} hg", (int)args.NewValue);
            }

            else if (sender == hpSlider)
            {
                hpLabel.Text = string.Format("HP = {0:F2}", (int)args.NewValue);
            }

            else if (sender == atkSlider)
            {
                atkLabel.Text = string.Format("Atk = {0:F2}", (int)args.NewValue);
            }

            else if (sender == defSlider)
            {
                defLabel.Text = string.Format("Def = {0:F2}", (int)args.NewValue);
            }
        }

        //Permet de choisir un type pour notre Pokemon
        async void SelectType(object sender, EventArgs e)
        {
            var TypesList = new List<string>();
            for (int TypeId = 1; TypeId <= App.database.GetTypesCollectionAsync().Result.Count; TypeId++)
            {
                TypesList.Add(App.Database.GetOneTypeAsync(TypeId).Name);
            }
                
            string action = await DisplayActionSheet("Selectionnez le type :", "Annuler", null, TypesList.ToArray());
            if (action != "Annuler")
            {
                if (TypePrincipal.Text == "Type Principal")
                {
                    TypePrincipal.Text = action;
                }
                else
                {
                    TypeSecondaire.Text = action;
                }
            } 
        }

        async void SelectColor(object sender, EventArgs e)
        {
            var ColorsList = new List<string>();
            for (int ColorId = 1; ColorId <= App.database.GetColorCollectionAsync().Result.Count; ColorId++)
            {
                ColorsList.Add(App.Database.GetOneColorAsync(ColorId).Color);
            }

            string action = await DisplayActionSheet("Selectionnez l'espèce :", "Annuler", null, ColorsList.ToArray());
            if (action != "Annuler")
            {
                Color.Text = action;
            }
        }

        //Ajoute un Pokemon dans la base de donnée
        async void SavePokemon(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(PokemonNameEntry.Text) && TypePrincipal.Text != "Type Principal")
            {
                string[] imageSource = PokemonImage.Source.ToString().Split(' ');
                string typeSecondaire = "";
                if (TypeSecondaire.Text != "Type Secondaire")
                {
                    typeSecondaire = TypeSecondaire.Text;
                }
                //Ajoute le nouveau Pokemon
                await App.Database.SavePokemonAsync(new Pokemon
                {
                    Name = PokemonNameEntry.Text,
                    Picture = imageSource[1],
                    Color = Color.Text,
                    TypePrincipal = TypePrincipal.Text,
                    TypeSecondaire = typeSecondaire,
                    Height = Convert.ToInt32(heightSlider.Value),
                    Weight = Convert.ToInt32(weightSlider.Value),
                    HP = Convert.ToInt32(hpSlider.Value),
                    Atk = Convert.ToInt32(atkSlider.Value),
                    Def = Convert.ToInt32(defSlider.Value),
                });

                await DisplayAlert("Félicitations !", "Votre Pokémon a correctement été ajouté.", "OK");
                //Réinitialise les valeurs de la page
                PokemonNameEntry.Text = string.Empty;
                PokemonImage.Source = "";
                PokemonImage.WidthRequest = 0;
                PokemonImage.HeightRequest = 0;
                Color.Text = "Choisir couleur";
                TypePrincipal.Text = "Type Principal";
                TypeSecondaire.Text = "Type Secondaire";
                heightSlider.Value = 0.0;
                weightSlider.Value = 0.0;
                hpSlider.Value = 0.0;
                atkSlider.Value = 0.0;
                defSlider.Value = 0.0;
            }
            else
            {
                await DisplayAlert("Oups !", "Vous avez oublié de choisir un nom ou un type à votre Pokemon.", "Je corrige cela");
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

            //Affiche l'image
            mediaFile = await CrossMedia.Current.PickPhotoAsync();
            PokemonImage.Source = mediaFile.Path;
            PokemonImage.WidthRequest = 100;
            PokemonImage.HeightRequest = 100;
            AddImageButton.Text = "Changer l'image";

            if (mediaFile == null)
            {
                return;
            }
        }
    }
}
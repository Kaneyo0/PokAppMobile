using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PokApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        int compteur = 0;
        public Home()
        {
            InitializeComponent();
        }

        //Easter Egg
        void Accueil(object sender, System.EventArgs e)
        {

            compteur++;
            var image = (Image)sender;
            if (compteur % 2 == 0)
            {
                image.Source = "salameche.png";
            }
            else
            {
                image.Source = "salamecheShiny.jpg";
            }

        }
    }
}
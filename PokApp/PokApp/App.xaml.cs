using System;
using System.IO;
using PokApp.Models;
using PokApp.Views;
using Xamarin.Forms;

namespace PokApp
{
    public partial class App : Application
    {
        public static Database database;

        public static Database Database
        {
            get
            {
                //Si la base de données n'existe pas
                if (database == null)
                {
                    //Création de la base de données
                    database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "pokemon.db3"));
                }
                return database;
            }
        }
        public App()
        {
            InitializeComponent();
            MainPage = new ShellPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

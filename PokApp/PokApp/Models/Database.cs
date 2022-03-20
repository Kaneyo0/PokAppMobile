using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokApp.Models
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Pokemon>().Wait();
            _database.CreateTableAsync<CollectionTypes>().Wait();
            _database.CreateTableAsync<CollectionColor>().Wait();
        }


        /**********POKEMON**********/
        //Récupère les Pokémons de la Base de données
        public Task<List<Pokemon>> GetPokemonAsync()
        {
            return _database.Table<Pokemon>().ToListAsync();
        }
        //Récupère un Pokemon en fonction de son Id
        public Pokemon GetOnePokemonAsync(int Id)
        {
            return _database.FindAsync<Pokemon>(Id).Result;
        }
        //Ajoute le Pokemon en paramètre, dans la base de données
        public Task<int> SavePokemonAsync(Pokemon pokemon)
        {
            return _database.InsertAsync(pokemon);
        }
        //Supprime un Pokemon de la base de données en fonction de l'Id en paramètre 
        public Task<int> DeletePokemonAsync(Pokemon pokemon)
        {
            return _database.DeleteAsync(pokemon);
        }


        /**********TYPES**********/
        //Récupère les Types de la Base de données
        public Task<List<CollectionTypes>> GetTypesCollectionAsync()
        {
            return _database.Table<CollectionTypes>().ToListAsync();
        }
        //Récupère un Type en fonction de son Id
        public CollectionTypes GetOneTypeAsync(int Id)
        {
            return _database.FindAsync<CollectionTypes>(Id).Result;
        }
        //Ajoute le Type en paramètre, dans la base de données
        public Task<int> SaveTypeAsync(CollectionTypes Type)
        {
            return _database.InsertAsync(Type);
        }


        /**********COULEURS**********/
        //Récupère les couleur de la Base de données
        public Task<List<CollectionColor>> GetColorCollectionAsync()
        {
            return _database.Table<CollectionColor>().ToListAsync();
        }
        //Récupère une couleur en fonction de son Id
        public CollectionColor GetOneColorAsync(int Id)
        {
            return _database.FindAsync<CollectionColor>(Id).Result;
        }
        //Ajoute la couleur en paramètre, dans la base de données
        public Task<int> SaveColorAsync(CollectionColor Color)
        {
            return _database.InsertAsync(Color);
        }
    }
}

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
        }
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

        //Récupère les Pokémons de la Base de données
        public Task<List<CollectionTypes>> GetTypesCollectionAsync()
        {
            return _database.Table<CollectionTypes>().ToListAsync();
        }

        //Ajoute le Pokemon en paramètre, dans la base de données
        public Task<int> SaveTypeAsync(CollectionTypes Type)
        {
            return _database.InsertAsync(Type);
        }
    }
}

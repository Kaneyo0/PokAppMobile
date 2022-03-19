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
        }
        //Récupère les Pokémons de la Base de données
        public Task<List<Pokemon>> GetPokemonAsync()
        {
            return _database.Table<Pokemon>().ToListAsync();
        }
        //Ajoute le Pokemon en paramètre, dans la base de données
        public Task<int> SavePokemonAsync(Pokemon pokemon)
        {
            return _database.InsertAsync(pokemon);
        }
        //Supprime un Pokemon de la base de données en fonction de l'Id en paramètre 
        //(NON FONCTIONNEL ! "It has no PK")
        public Task<int> DeletePokemonAsync(int Id)
        {
            return _database.DeleteAsync(Id);
        }
    }
}

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

        public Task<List<Pokemon>> GetPokemonAsync()
        {
            return _database.Table<Pokemon>().ToListAsync();
        }

        public Task<int> SavePokemonAsync(Pokemon pokemon)
        {
            return _database.InsertAsync(pokemon);
        }

        public Task<int> DeletePokemonAsync(string PokemonName)
        {
            return _database.DeleteAsync(PokemonName);
        }
    }
}

using SQLite;

namespace PokApp.Models
{
    public class CollectionTypes
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

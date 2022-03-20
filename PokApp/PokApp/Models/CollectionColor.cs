using SQLite;

namespace PokApp.Models
{
    public class CollectionColor
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Color { get; set; }
    }
}

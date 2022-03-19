using SQLite;

namespace PokApp.Models
{
    public class Pokemon
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public string Color { get; set; }
        public int HP { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public string TypePrincipal { get; set; }
        public string TypeSecondaire { get; set; }
        public string TalentPrincipal { get; set; }
        public string TalentSecondaire { get; set; }
        public string TalentSecrete { get; set; }
    }
}

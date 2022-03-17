using Newtonsoft.Json;
using SQLite;
using System.Collections.Generic;

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
        public string AbilitiesBlobbed { get; set; }
        [Ignore]
        public List<string> Abilities
        {
            get
            {
                return JsonConvert.DeserializeObject<List<string>>(AbilitiesBlobbed);
            }
            set
            {
                AbilitiesBlobbed = JsonConvert.SerializeObject(value);
            }
        }
    }
}

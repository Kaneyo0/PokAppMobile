using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

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
        public string TypesBlobbed { get; set; }
        public string AbilitiesBlobbed { get; set; }
        [Ignore]
        public List<string> Types
        {
            get
            {
                return JsonConvert.DeserializeObject<List<string>>(TypesBlobbed);
            }
            set
            {
                TypesBlobbed = JsonConvert.SerializeObject(value);
            }
        }
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

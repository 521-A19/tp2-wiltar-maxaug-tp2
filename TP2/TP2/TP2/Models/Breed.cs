using System.Collections.Generic;

namespace TP2.Models
{
    public class Breed
    {
        public string Name { get; set; }
        public List<string> SubBreeds { get; set; } //Variables doient avoir le nom identique au JSON
    }
}

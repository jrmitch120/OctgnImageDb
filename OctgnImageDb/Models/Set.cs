using System.Collections.Generic;

namespace OctgnImageDb.Models
{
    public class Set
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public bool ImagesNeeded { get; set; }

        public readonly List<Card> Cards = new List<Card>();
    }
}
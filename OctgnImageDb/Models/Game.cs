using System.Collections.Generic;

namespace OctgnImageDb.Models
{
    public class Game
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public readonly List<Set> Sets = new List<Set>();
    }
}

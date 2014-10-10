using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OctgnImageDb.Models
{
    public class Game
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public readonly List<Set> Sets = new List<Set>();
    }
}

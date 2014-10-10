using System;
using System.Collections.Generic;
using System.Linq;

namespace OctgnImageDb.Models
{
    public static class ModelExtensions
    {
        public static Set FindSetByName(this List<Set> sets, string name)
        {
            return sets.FirstOrDefault(s => s.Name.Equals(name, StringComparison.OrdinalIgnoreCase)) ??
                   sets.FirstOrDefault(
                       s =>
                           s.Name.Equals(name.Replace("&", "and").Replace("Base", "Core"),
                               StringComparison.OrdinalIgnoreCase));


        }

        public static Set FindSetById(this List<Set> sets, string setId)
        {
            return sets.FirstOrDefault(s => s.Id == setId);
        }

        public static Set FindSetById(this List<Game> games, string setId)
        {
            return games.SelectMany(g => g.Sets).FirstOrDefault(s => s.Id == setId);
        }
    }
}

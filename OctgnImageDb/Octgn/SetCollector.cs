using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using OctgnImageDb.Models;

namespace OctgnImageDb.Octgn
{
    public class SetCollector
    {
        private readonly CardCollector _cardCollector;

        public SetCollector(CardCollector cardCollector)
        {
            _cardCollector = cardCollector;
        }

        public List<Set> CollectSets(string gameId)
        {
            var sets = new List<Set>();
            var di = new DirectoryInfo(OctgnPaths.SetsDirectory(gameId));

            foreach (var setDirectory in di.EnumerateDirectories())
            {
                var setDefinition = XDocument.Parse(File.ReadAllText(setDirectory.FullName + @"\" + "set.xml"));

                if (setDefinition.Root != null)
                {
                    sets.Add(new Set
                    {
                        Id = setDefinition.Root.Attribute("id").Value,
                        Name = setDefinition.Root.Attribute("name").Value,
                        ImagesNeeded = true
                    });
                }
            }

            sets.ForEach(s => s.Cards.AddRange(_cardCollector.CollectCards(gameId, s.Id)));

            return sets;
        }
    }
}
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using OctgnImageDb.Models;

namespace OctgnImageDb.Octgn
{
    public class GameCollector
    {
        private readonly SetCollector _setCollector;

        public GameCollector(SetCollector setCollector)
        {
            _setCollector = setCollector;
        }

        public List<Game> CollectGames()
        {
            var games = new List<Game>();
            var di = new DirectoryInfo(OctgnPaths.GameDatabaseDirectory);

            foreach (var gameDirectory in di.EnumerateDirectories())
            {
                var gameDefinition = XDocument.Parse(File.ReadAllText(gameDirectory.FullName + @"\" + "definition.xml"));

                if (gameDefinition.Root != null)
                {
                    games.Add(new Game
                    {
                        Id = gameDefinition.Root.Attribute("id").Value,
                        Name = gameDefinition.Root.Attribute("name").Value
                    });
                }
            }

            games.ForEach(g => g.Sets.AddRange(_setCollector.CollectSets(g.Id)));

            return games;
        }
    }
}

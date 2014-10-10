using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using OctgnImageDb.Models;

namespace OctgnImageDb.Octgn
{
    public class CardCollector
    {
        public List<Card> CollectCards(string gameId, string setId)
        {
            var cards = new List<Card>();

            var setDefinition = XDocument.Parse(File.ReadAllText(OctgnPaths.SetDefinitionPath(gameId, setId)));

            if (setDefinition.Root != null)
            {
                foreach (var card in setDefinition.Root.Descendants("card"))
                {
                    cards.Add(new Card
                    {
                        Id = card.Attribute("id").Value,
                        Name = card.Attribute("name").Value
                    });
                }
            }
            

            return cards;
        }
    }
}
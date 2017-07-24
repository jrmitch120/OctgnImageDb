using System.Linq;
using System.Net;
using System.Web.Helpers;
using OctgnImageDb.Imaging.Cache;
using OctgnImageDb.Logging;
using OctgnImageDb.Models;
using OctgnImageDb.Octgn;

namespace OctgnImageDb.Imaging.LotR
{
    [ImageProvider("Lord of the Rings - The Card Game")]
    public class LotrDBImages : IImageProvider
    {
        private const string ApiBaseUrl = "http://ringsdb.com";
        private readonly ImageWriter _imageWriter;
        private readonly ImageCache _cache;

        public LotrDBImages(ImageWriter imageWriter, ImageCache cache)
        {
            _imageWriter = imageWriter;
            _cache = cache;
        }

        public void GetCardImages(Game game)
        {
            LogManager.GetLogger().Log(game.Name, LogType.Game);

            var wc = new WebClient();

            dynamic apiSets = Json.Decode(wc.DownloadString(ApiBaseUrl + "/api/public/packs/"));

            foreach (var apiSet in apiSets)
            {
                string setName = apiSet.name;
                
				//I needed to match the sets using a custom method since there were no identifying marks except for the actual name.
                var set = game.Sets.SingleOrDefault(s => StringExtensions.CustomContains(s.Name, setName));

                if (set == null || !set.ImagesNeeded)
                {
                    if (set == null)
                        LogManager.GetLogger().Log("Unable to map set: " + setName + ".  It may not be on OCTGN yet.", LogType.Error);

                    continue;
                }

                LogManager.GetLogger().Log(set.Name + ": " + apiSet.name, LogType.Set);

                dynamic apiCards = Json.Decode(wc.DownloadString(ApiBaseUrl + "/api/public/cards/" + apiSet.code));

                foreach (var apiCard in apiCards)
                {
                    var card = set.Cards.FirstOrDefault(c => c.Id.Equals(apiCard.octgnid));

                    if (card != null && apiCard.imagesrc != string.Empty)
                    {
                        try
                        {
                            byte[] image = _cache.GetImage(card.Id);

                            if (image == null)
                            {
                                image = wc.DownloadData(ApiBaseUrl + apiCard.imagesrc);
                                _cache.SaveImage(card.Id, ".jpg", image);
                            }

                            _imageWriter.WriteImage(OctgnPaths.CardImagePath(game.Id, set.Id, card.Id, ".jpg"), image);
                        }
                        catch (WebException ex)
                        {
                            LogManager.GetLogger().Log("Unable to find: " + card.Name, LogType.Card);

                            if (((HttpWebResponse)ex.Response).StatusCode != HttpStatusCode.NotFound)
                                throw;
                        }
                    }
                    else
                        LogManager.GetLogger().Log("Unable to find: " + apiCard.title, LogType.Card);
                }
            }
        }
    }
}

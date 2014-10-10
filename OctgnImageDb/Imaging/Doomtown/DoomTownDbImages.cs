using System;
using System.Linq;
using System.Net;
using System.Web.Helpers;
using OctgnImageDb.Imaging.Cache;
using OctgnImageDb.Logging;
using OctgnImageDb.Models;
using OctgnImageDb.Octgn;

namespace OctgnImageDb.Imaging.Doomtown
{
    [ImageProvider("Doomtown-Reloaded")]
    public class DoomtownDbImages : IImageProvider
    {
        private const string ApiBaseUrl = "http://dtdb.co";
        private readonly ImageWriter _imageWriter;
        private readonly ImageCache _cache;

        public DoomtownDbImages(ImageWriter imageWriter, ImageCache cache)
        {
            _imageWriter = imageWriter;
            _cache = cache;
        }

        public void GetCardImages(Game game)
        {
            LogManager.GetLogger().Log(game.Name, LogType.Game);

            var wc = new WebClient();

            dynamic apiSets = Json.Decode(wc.DownloadString(ApiBaseUrl + "/api/sets/"));

            foreach (var apiSet in apiSets)
            {
                string setName = apiSet.name;
                var set = game.Sets.FindSetByName(setName);

                if (set == null || !set.ImagesNeeded)
                {
                    if(set == null)
                        LogManager.GetLogger().Log("Unable to map set: set.Name", LogType.Error);

                    continue;
                }

                LogManager.GetLogger().Log(set.Name, LogType.Set);

                dynamic apiCards = Json.Decode(wc.DownloadString(ApiBaseUrl + "/api/set/" + apiSet.code));

                foreach (var apiCard in apiCards)
                {
                    var card =
                        set.Cards.FirstOrDefault(
                            c => c.Name.Equals(apiCard.title.ToString(), StringComparison.OrdinalIgnoreCase));

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
                }
            }
        }
    }
}

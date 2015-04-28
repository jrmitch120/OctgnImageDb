using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
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
                        LogManager.GetLogger().Log("Unable to map set: " + setName + ".  It may not be on OCTGN yet.", LogType.Error);

                    continue;
                }

                LogManager.GetLogger().Log(set.Name, LogType.Set);

                dynamic apiCards = Json.Decode(wc.DownloadString(ApiBaseUrl + "/api/set/" + apiSet.code));

                foreach (var apiCard in apiCards)
                {
                    
                    // Certain card names can found acrosss multiple sets.  DTDB labels them with (Ext.#).  Need to strip that out for OCTGN lookup.
                    string apiTile = Regex.Replace(apiCard.title.ToString(), @"\(Exp.[\s]*[\d]*\)", "",RegexOptions.IgnoreCase).Trim();

                    // There is some inconsitent nameing with é between the definition files and dtdb.  Try both ways if necessary 
                    var card =
                        set.Cards.FirstOrDefault(
                            c => c.Name.Equals(apiTile, StringComparison.OrdinalIgnoreCase)) ??
                        set.Cards.FirstOrDefault(
                            c => c.Name.Trim().Equals(apiTile.Replace("\u00e9", "e"), StringComparison.OrdinalIgnoreCase));

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

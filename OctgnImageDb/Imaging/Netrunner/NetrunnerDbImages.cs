using System.Net;
using OctgnImageDb.Imaging.Cache;
using OctgnImageDb.Logging;
using OctgnImageDb.Models;
using OctgnImageDb.Octgn;

namespace OctgnImageDb.Imaging.Netrunner
{
    [ImageProvider("Android-Netrunner")]
    public class NetrunnerDbImages : IImageProvider
    {
        private const string ApiBaseUrl = "http://netrunnerdb.com";
        private readonly ImageWriter _imageWriter;
        private readonly ImageCache _cache;

        public NetrunnerDbImages(ImageWriter imageWriter, ImageCache cache)
        {
            _imageWriter = imageWriter;
            _cache = cache;
        }

        public void GetCardImages(Game game)
        {
            LogManager.GetLogger().Log(game.Name, LogType.Game);

            var wc = new WebClient();

            foreach (var set in game.Sets)
            {
                if (set == null || !set.ImagesNeeded)
                    continue;

                LogManager.GetLogger().Log(set.Name, LogType.Set);

                foreach (var card in set.Cards)
                {
                    try
                    {
                        byte[] image = _cache.GetImage(card.Id);
                        
                        if(image == null)
                        {
                            image = wc.DownloadData(ApiBaseUrl + "/web/bundles/netrunnerdbcards/images/cards/en/" + card.Id.Substring(card.Id.Length - 5) + ".png");
                            _cache.SaveImage(card.Id, ".png", image);
                        }

                        _imageWriter.WriteImage(OctgnPaths.CardImagePath(game.Id, set.Id, card.Id, ".png"), image);
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

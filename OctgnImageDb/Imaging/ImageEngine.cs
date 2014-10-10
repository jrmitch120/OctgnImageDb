using System;
using System.Collections.Generic;
using System.Linq;
using OctgnImageDb.Models;

namespace OctgnImageDb.Imaging
{
    public class ImageEngine
    {
        private readonly IImageProvider[] _providers;

        public ImageEngine(IImageProvider[] providers)
        {
            _providers = providers;
        }

        public void GetCardImages(IEnumerable<Game> games)
        {
            foreach (Game game in games)
            {
                IImageProvider provider = TryGetProvider(game.Name);

                if (provider == null)
                    continue;

                provider.GetCardImages(game);
            }
        }

        private IImageProvider TryGetProvider(string gameName)
        {
            foreach (IImageProvider provider in _providers)
            {
                var attribute =
                    (ImageProviderAttribute)
                        provider.GetType().GetCustomAttributes(typeof (ImageProviderAttribute), true).FirstOrDefault();

                if (attribute != null && attribute.GameName.Equals(gameName, StringComparison.OrdinalIgnoreCase))
                    return provider;
            }

            return null;
        }
    }
}

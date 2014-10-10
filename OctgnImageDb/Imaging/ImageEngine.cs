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
                IImageProvider provider = TryGetRunner(game.Name);

                if (provider == null)
                    continue;

                provider.GetCardImages(game);
            }
        }

        private IImageProvider TryGetRunner(string gameName)
        {
            foreach (IImageProvider runner in _providers)
            {
                var attribute =
                    (ImageProviderAttribute)
                        runner.GetType().GetCustomAttributes(typeof (ImageProviderAttribute), true).FirstOrDefault();

                if (attribute != null && attribute.GameName.Equals(gameName, StringComparison.CurrentCultureIgnoreCase))
                    return runner;
            }

            return null;
        }
    }
}

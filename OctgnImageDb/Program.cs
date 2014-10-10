using System.Configuration;
using System.Linq;
using System.Web.Helpers;
using Ninject;
using OctgnImageDb.Imaging;
using OctgnImageDb.Imaging.Doomtown;
using OctgnImageDb.Imaging.Netrunner;
using OctgnImageDb.Imaging.Starwars;
using OctgnImageDb.Models;
using OctgnImageDb.Octgn;

namespace OctgnImageDb
{
    class Program
    {
        static void Main(string[] args)
        {
            var kernel = new StandardKernel();

            kernel.Bind<IImageProvider>().To<NetrunnerDbImages>();
            kernel.Bind<IImageProvider>().To<DoomtownDbImages>();
            kernel.Bind<IImageProvider>().To<TopTierDbImages>();

            var games = kernel.Get<GameCollector>().CollectGames();

            // Ignore sets as indicated.  Usually markers
            ConfigurationManager.AppSettings["IgnoreSets"].Split(new[] {','}).ToList().ForEach(setId =>
            {
                Set ignoreSet = games.FindSetById(setId);

                if (ignoreSet != null)
                    ignoreSet.ImagesNeeded = false;
            });

            kernel.Get<ImageEngine>().GetCardImages(games);            
        }
    }
}

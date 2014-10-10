using System;
using System.Configuration;
using System.Linq;
using Ninject;
using OctgnImageDb.Imaging;
using OctgnImageDb.Imaging.Doomtown;
using OctgnImageDb.Imaging.Netrunner;
using OctgnImageDb.Imaging.Starwars;
using OctgnImageDb.Logging;
using OctgnImageDb.Models;
using OctgnImageDb.Octgn;

namespace OctgnImageDb
{
    class Program
    {
        static void Main(string[] args)
        {
            try
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
            catch (Exception ex)
            {
                LogManager.GetLogger().Log(ex.Message, LogType.Error);
            }

            LogManager.GetLogger().Log("Import Complete!");

            Console.ReadKey();
        }
    }
}

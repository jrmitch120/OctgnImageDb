using System;
using System.Configuration;
using System.Linq;
using Ninject;
using OctgnImageDb.Imaging;
using OctgnImageDb.Imaging.Cache;
using OctgnImageDb.Logging;
using OctgnImageDb.Models;
using OctgnImageDb.Octgn;
using OctgnImageDb.Setup;

namespace OctgnImageDb
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var kernel = Init.Container();

                var packManager = kernel.Get<ImagePackManager>();

                if (packManager.ImagePacksAvailavble)
                {
                    Console.Write("Image packs detected.  Would you like to import them? (y/n): ");
                    
                    while (true)
                    {
                        var keypress = Console.ReadKey(true);
                        if (keypress.KeyChar.ToString().ToLower() == "y" || keypress.KeyChar.ToString().ToLower() == "n")
                        {
                            Console.WriteLine(keypress.KeyChar);

                            if (keypress.KeyChar == 'y')
                                packManager.ImportImagePacks();

                            break;
                        }
                    }
                }
                    
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

            LogManager.GetLogger().Log("Complete!");

            Console.ReadKey();
        }
    }
}

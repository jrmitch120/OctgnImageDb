using Ninject;
using OctgnImageDb.Imaging;
using OctgnImageDb.Imaging.Doomtown;
using OctgnImageDb.Imaging.Netrunner;
using OctgnImageDb.Imaging.Starwars;

namespace OctgnImageDb.Setup
{
    public partial class Init
    {
        public static IKernel Container()
        {
            var kernel = new StandardKernel();

            kernel.Bind<IImageProvider>().To<NetrunnerDbImages>();
            kernel.Bind<IImageProvider>().To<DoomtownDbImages>();
            //TopTier seemed to go under.  No reliable source for hi-rez images that I know of anymore
            //kernel.Bind<IImageProvider>().To<TopTierDbImages>(); 

            return kernel;
        }
    }
}

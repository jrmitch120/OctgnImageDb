using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            kernel.Bind<IImageProvider>().To<TopTierDbImages>();

            return kernel;
        }
    }
}

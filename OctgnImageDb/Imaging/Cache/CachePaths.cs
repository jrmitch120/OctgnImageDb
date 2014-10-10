using System;

namespace OctgnImageDb.Imaging.Cache
{
    static class CachePaths
    {
        public static string CacheDirectory
        {
            get
            {
                string cacheLocation;
                #if DEBUG
                    cacheLocation = AppDomain.CurrentDomain.BaseDirectory + @"..\..\..\compiled\ImageCache\";  // haxores
                #else
                    cacheLocation = AppDomain.CurrentDomain.BaseDirectory + @"ImageCache\";
                #endif
                return cacheLocation;
            }
        }
    }
}

using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using OctgnImageDb.Logging;

namespace OctgnImageDb.Imaging.Cache
{
    public class ImagePackManager
    {
        private readonly string _cacheLocation = CachePaths.CacheDirectory;

        private const string PackExtension = ".o8c";

        public bool ImagePacksAvailavble
        {
            get
            {
                var di = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

                return di.GetFiles("*" + PackExtension).Any();
            }
        }

        public void ImportImagePacks()
        {
            // The datapacks
            var di = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);

            foreach (var file in di.GetFiles("*" + PackExtension))
            {
                using (var dataPack = new FileStream(file.FullName, FileMode.Open))
                {
                    var archive = new ZipArchive(dataPack);

                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        if (entry.Name != string.Empty)
                        {
                            var iw = new ImageWriter();
                            using (var image = entry.Open())
                            {
                                iw.WriteImage(_cacheLocation + entry.Name, image);
                            }
                        }
                    }
                }

                LogManager.GetLogger().Log(file.Name, LogType.Set);
            }
        }
    }
}

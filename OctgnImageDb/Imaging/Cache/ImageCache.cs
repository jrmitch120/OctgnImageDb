using System.IO;
using System.Linq;

namespace OctgnImageDb.Imaging.Cache
{
    public class ImageCache
    {
        private readonly ImageWriter _imageWriter;
        private readonly string _cacheLocation = CachePaths.CacheDirectory;
        
        public ImageCache(ImageWriter imageWriter)
        {
            _imageWriter = imageWriter;
        }

        public void SaveImage(string cardId, string fileExtension, byte [] image)
        {
            _imageWriter.WriteImage(_cacheLocation + cardId + fileExtension, image);
        }

        public byte[] GetImage(string cardId)
        {
            Directory.CreateDirectory(_cacheLocation);
            
            var di = new DirectoryInfo(_cacheLocation).GetFiles(cardId + ".*");
            var image = di.FirstOrDefault();

            if(image == null)
                return null;

            return File.ReadAllBytes(image.FullName);
        }
    }
}

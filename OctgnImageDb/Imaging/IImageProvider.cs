using OctgnImageDb.Models;

namespace OctgnImageDb.Imaging
{
    public interface IImageProvider
    {
        void GetCardImages(Game game);
    }
}

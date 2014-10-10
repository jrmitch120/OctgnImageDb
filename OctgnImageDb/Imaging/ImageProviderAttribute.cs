using System;

namespace OctgnImageDb.Imaging
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ImageProviderAttribute : Attribute
    {
        public string GameName { get; set; }

        public ImageProviderAttribute(string gameName)
        {
            GameName = gameName;
        }
    }
}

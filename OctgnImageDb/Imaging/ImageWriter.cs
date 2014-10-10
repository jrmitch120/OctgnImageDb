using System.IO;

namespace OctgnImageDb.Imaging
{
    public class ImageWriter
    {
        public void WriteImage(string path, byte[] image)
        {
            var directory = path.Substring(0,path.LastIndexOf(@"\", System.StringComparison.Ordinal));
            var file = path.Substring(path.LastIndexOf(@"\", System.StringComparison.Ordinal) + 1);
            Directory.CreateDirectory(directory);

            foreach (FileInfo f in new DirectoryInfo(directory).GetFiles(file.Remove(file.IndexOf(".", System.StringComparison.CurrentCulture)) + ".*"))
                f.Delete();
            
            File.WriteAllBytes(path, image);
        }
    }
}

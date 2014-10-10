using System;

namespace OctgnImageDb.Octgn
{
    static class OctgnPaths
    {
        private static readonly string BasePath;
        
        public static string CardImagePath(string gameId, string setId, string cardId, string fileExtension)
        {
            return String.Format(@"{0}{1}\Sets\{2}\Cards\{3}{4}", ImageDatabaseDirectory, gameId, setId, cardId, fileExtension);
        }

        public static string GameDatabaseDirectory
        {
            get { return BasePath + @"GameDatabase\"; }
        }

        public static string ImageDatabaseDirectory
        {
            get { return BasePath + @"ImageDatabase\"; }
        }

        public static string SetsDirectory(string gameId)
        {
            return String.Format(@"{0}{1}\Sets\", GameDatabaseDirectory, gameId);
        }

        public static string SetDefinitionPath(string gameId, string setId)
        {
            return String.Format(@"{0}{1}\Sets\{2}\set.xml", GameDatabaseDirectory, gameId, setId);
        }

        public static string SetImageDirectory(string gameId, string setId)
        {
            return String.Format(@"{0}{1}\Sets\{2}\Cards\", ImageDatabaseDirectory, gameId, setId);
        }

        static OctgnPaths()
        {
            BasePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\OCTGN\";
        }
    }
}

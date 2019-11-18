using System.IO;

namespace Epita.BlobStorage.Model
{
    /// <summary>
    /// Container hosting a stream, a name, and a contentType
    /// </summary>
    public class CloudFile
    {
        public string Name { get; set; }

        public string ContentType { get; set; }

        public Stream File { get; set; }
    }
}
using System.Collections.Generic;

namespace Epita.BlobStorage.Model
{
    public class Photo
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public Status Status { get; set; }

        public IEnumerable<string> Tags { get; set; }

        public IEnumerable<string> AlbumIds { get; set; }

        public string UserId { get; set; }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Epita.BlobStorage.Logic.Contracts;
using Epita.BlobStorage.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Epita.BlobStorage.Gateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class AlbumsController : ControllerBase
    {
        private readonly IAlbumLogic albumLogic;

        public AlbumsController(IAlbumLogic albumLogic)
        {
            this.albumLogic = albumLogic;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] string albumName)
        {
            string userId = HttpContext.User.Identity.Name;

            string albumId = await albumLogic.CreateAsync(userId, albumName).ConfigureAwait(false);

            if (string.IsNullOrEmpty(albumId))
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(GetByIdAsync), new { albumId }, albumId);
        }

        [HttpGet("{albumId}")]
        public async Task<IActionResult> GetByIdAsync(string albumId)
        {
            string userId = HttpContext.User.Identity.Name;

            Album album = await albumLogic.GetByIdAsync(userId, albumId);

            if (album == null)
            {
                return NotFound();
            }

            return Ok(album);
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            string userId = HttpContext.User.Identity.Name;

            IEnumerable<Album> albums = await albumLogic.GetAsync(userId).ConfigureAwait(false);



            if (albums == null)
            {
                return Ok(Enumerable.Empty<Album>());
            }

            return Ok(albums);
        }

        [HttpPut("{albumId}/photos/{photoId}")]
        public async Task<IActionResult> AddPhotoToAlbumByAsync(string albumId, string photoId)
        {
            string userId = HttpContext.User.Identity.Name;

            bool success = await albumLogic.AddPhotoToAlbumByAsync(userId, albumId, photoId).ConfigureAwait(false);

            if (success)
            {
                return NoContent();
            }

            return BadRequest();
        }

        [HttpGet("{albumId}/photos")]
        public async Task<IActionResult> GetAllPhotoByIdAsync(string albumId, [FromQuery] IEnumerable<string> tags)
        {
            string userId = HttpContext.User.Identity.Name;

            IReadOnlyList<string> tagParameters = null;

            if (tags != null)
            {
                tagParameters = new List<string>(tags);
            }

            IEnumerable<Photo> photos = await albumLogic.GetPhotosByAlbumIdAsync(userId, albumId, tagParameters);

            if (photos == null)
            {
                return NotFound();
            }

            return Ok(photos);
        }
    }
}
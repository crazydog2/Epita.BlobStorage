using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Epita.BlobStorage.Logic.Contracts;
using Epita.BlobStorage.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Epita.BlobStorage.Gateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class PhotosController : ControllerBase
    {
        private readonly IPhotoLogic photoLogic;

        public PhotosController(IPhotoLogic photoLogic)
        {
            this.photoLogic = photoLogic;
        }

        [HttpPost]
        public async Task<IActionResult> UploadAsync([FromForm] IFormFile file)
        {
            string userId = HttpContext.User.Identity.Name;

            await using Stream stream = file.OpenReadStream();

            string filename = file.FileName;

            string photoId = await photoLogic.UploadAsync(stream, filename, userId).ConfigureAwait(false);

            if (!string.IsNullOrEmpty(photoId))
            {
                return CreatedAtAction(nameof(GetByIdAsync), new { photoId }, photoId);
            }

            return BadRequest();
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] IEnumerable<string> tags)
        {
            string userId = HttpContext.User.Identity.Name;

            IReadOnlyList<string> tagParameters = null;

            if (tags != null)
            {
                tagParameters = new List<string>(tags);
            }

            IEnumerable<Photo> photos = await photoLogic.GetAsync(userId, tagParameters);

            if (photos == null)
            {
                return NotFound();
            }

            return Ok(photos);
        }

        [HttpGet("{photoId}")]
        public async Task<IActionResult> GetByIdAsync(string photoId)
        {
            string userId = HttpContext.User.Identity.Name;

            Photo photo = await photoLogic.GetByIdAsync(userId, photoId);

            if (photo == null)
            {
                return NotFound();
            }

            return Ok(photo);
        }

        [HttpPut("{photoId}/tags")]
        public async Task<IActionResult> AddTagsIdAsync(string photoId, [FromBody] IEnumerable<string> tags)
        {
            string userId = HttpContext.User.Identity.Name;

            bool success = await photoLogic.AddOrUpdateTagsAsync(userId, photoId, tags);

            if (!success)
            {
                return BadRequest();
            }

            return NoContent();
        }

        [HttpGet("{photoId}/download")]
        public async Task<IActionResult> DownloadByIdAsync(string photoId)
        {
            string userId = HttpContext.User.Identity.Name;

            CloudFile file = await photoLogic.DownloadByIdAsync(userId, photoId);

            if (file == null)
            {
                return NotFound();
            }

            return File(file.File, file.ContentType, file.Name);
        }

        [HttpGet("{photoId}.redirect")]
        public async Task<IActionResult> RedirectByIdAsync(string photoId)
        {
            string userId = HttpContext.User.Identity.Name;

            Uri photoUri = await photoLogic.GetSharedAccessUriAsync(userId, photoId);

            if (photoUri == null)
            {
                return NotFound();
            }

            return Redirect(photoUri.ToString());
        }
    }
}
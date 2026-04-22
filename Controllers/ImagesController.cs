using ECommerceAPI.BLL;
using ECommerceAPI.Common;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private IImageManager _imageManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImagesController(IImageManager imageManager, IWebHostEnvironment webHostEnvironment)
        {
            _imageManager = imageManager;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<ActionResult<GeneralResult<ImageUploadResultDTo>>> UploadAsync([FromForm] ImageUploadDTo imageUploadDTo)
        {
            var schema = Request.Scheme;
            var host = Request.Host.Value;
            var basePath = _webHostEnvironment.ContentRootPath;

            var result = await _imageManager.UploadAsync(imageUploadDTo,basePath,schema,host);
            if (result == null)
            {
                return BadRequest(GeneralResult<ImageUploadResultDTo>.FailResult("Failed to upload the image"));
            }
            return Ok(GeneralResult<ImageUploadResultDTo>.SuccessResult(result,"Image Uploaded Successfully"));
        }
    }
}

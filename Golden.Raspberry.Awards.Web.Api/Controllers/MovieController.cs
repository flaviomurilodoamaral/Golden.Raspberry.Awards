using Golden.Raspberry.Awards.Business.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Golden.Raspberry.Awards.Web.Api.Controllers
{
    [ApiController]
    [Route("api/movies")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieBusiness _business;

        public MovieController(IMovieBusiness business)
        {
            _business = business;
        }

        [HttpPost("upload")]
        [Consumes("multipart/form-data")]
        public IActionResult UploadCsv([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file provided.");

            try
            {
                using var stream = file.OpenReadStream();

                _business.ProcessCsv(stream);

                return Ok("File processed successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error processing file: {ex.Message}");
            }
        }

        [HttpGet("award-intervals")]
        public IActionResult GetAwardIntervals()
        {
            try
            {
                var (min, max) = _business.GetAwardIntervals();

                return Ok(new { min, max });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error fetching award intervals: {ex.Message}");
            }
        }
    }
}

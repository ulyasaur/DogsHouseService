using DogsHouseService.BLL.Abstractions;
using DogsHouseService.BLL.DTOs;
using DogsHouseService.BLL.DTOs.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace DogsHouseService.Controllers
{
    [Route("/[controller]")]
    [ApiController]
    [EnableRateLimiting("concurrency")]
    public class DogsController : ControllerBase
    {
        private readonly IDogsService _dogsService;

        private readonly ILogger<DogsController> _logger;

        public DogsController(IDogsService dogsService, ILogger<DogsController> logger)
        {
            _dogsService = dogsService;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> AddDog(DogDto dog)
        {
            try
            {
                await _dogsService.AddDogAsync(dog);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException)
            {
                return Conflict();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "The dog with name {Name} could not be created", dog.Name);
                return Problem();
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<DogDto>>> GetDogs(
            [FromQuery]
            PagingParamsDto pagingParams,
            [FromQuery]
            SortingParamsDto sortingParams)
        {
            try
            {
                List<DogDto> dogDtos = await _dogsService.GetDogsAsync(pagingParams, sortingParams);

                return Ok(dogDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "The list of dogs could not be retrieved");
                return Problem();
            }
        }
    }
}

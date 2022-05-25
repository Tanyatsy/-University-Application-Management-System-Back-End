using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Unipply.Services;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using Unipply.Models;

namespace Unipply.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HobbiesController : ControllerBase
    {
        private readonly ILogger<HobbiesController> _logger;
        private readonly IRecommendationsService _recommendationsService;

        public HobbiesController(
            ILogger<HobbiesController> logger,
            IRecommendationsService recommendationsService)
        {
            _logger = logger;
            _recommendationsService = recommendationsService;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IEnumerable<HobbyModel>> GetAllHobbiesAsync()
        {
            return await _recommendationsService.GetRecomendationsHobbiesAsync();
        }
    }
}

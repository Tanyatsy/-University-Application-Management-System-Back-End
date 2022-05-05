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
        private readonly ILogger<FacultiesController> _logger;
        private readonly IRecommendationsIteractor _recommendationsIteractor;

        public HobbiesController(
            ILogger<FacultiesController> logger,
            IRecommendationsIteractor recommendationsIteractor)
        {
            _logger = logger;
            _recommendationsIteractor = recommendationsIteractor;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IEnumerable<HobbyModel>> GetAllHobbiesAsync()
        {
            var response = await _recommendationsIteractor.GetRecomendationsHobbiesAsync();
            return JsonConvert
                       .DeserializeObject<List<HobbyModel>>(await response.Content.ReadAsStringAsync());
        }
    }
}

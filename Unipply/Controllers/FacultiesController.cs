using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Unipply.Services;
using System.Threading.Tasks;
using Unipply.Models.Faculty;
using System.Collections.Generic;
using System.Linq;
using Unipply.Models.Specialty;
using Newtonsoft.Json;
using Unipply.Models;

namespace Unipply.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FacultiesController : ControllerBase
    {
        private readonly ILogger<FacultiesController> _logger;
        private readonly IFacultyDataService _facultyDataService;
        private readonly ISpecialtyDataService _specialtyDataService;
        private readonly IRecommendationsIteractor _recommendationsIteractor;

        public FacultiesController(
            ILogger<FacultiesController> logger,
            IFacultyDataService facultyDataService,
            IRecommendationsIteractor recommendationsIteractor,
            ISpecialtyDataService specialtyDataService)
        {
            _logger = logger;
            _facultyDataService = facultyDataService;
            _specialtyDataService = specialtyDataService;
            _recommendationsIteractor = recommendationsIteractor;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IEnumerable<FacultyModel>> GetAllFacultiesAsync()
        {
            var faculties = await _facultyDataService.GetAsync();
            var specialties = await _specialtyDataService.GetAsync();

            return faculties.Select(faculty => new FacultyModel
            {
                Id = faculty.Id,
                Info = faculty.Info,
                Title = faculty.Title,
                Specialties = specialties.Where(s => s.FacultyId == faculty.Id)
                .Select(s => new SpecialtyModel
                {
                    Id = s.Id,
                    Description = s.Description,
                    Title = s.Title
                }).ToList()
            });
        }

        [HttpPost]
        [Route("recommendations")]
        public async Task<IEnumerable<RecommendationModel>> GetRecomendationsFacultiesAsync([FromBody] List<string> hobbies) 
        {
            var response = await _recommendationsIteractor.GetRecomendationsFacultiesAsync(hobbies);
            return JsonConvert
                       .DeserializeObject<List<RecommendationModel>>(await response.Content.ReadAsStringAsync());
        }
    }
}

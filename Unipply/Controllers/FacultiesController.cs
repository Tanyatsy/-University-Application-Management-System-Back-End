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
using Unipply.Models.Recommendation;
using System;

namespace Unipply.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FacultiesController : ControllerBase
    {
        private readonly ILogger<FacultiesController> _logger;
        private readonly IFacultyDataService _facultyDataService;
        private readonly ISpecialtyDataService _specialtyDataService;
        private readonly IRecommendationsService _recommendationsService;

        public FacultiesController(
            ILogger<FacultiesController> logger,
            IFacultyDataService facultyDataService,
            IRecommendationsService recommendationsService,
            ISpecialtyDataService specialtyDataService)
        {
            _logger = logger;
            _facultyDataService = facultyDataService;
            _specialtyDataService = specialtyDataService;
            _recommendationsService = recommendationsService;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IEnumerable<FacultyModel>> GetAllFacultiesAsync()
        {
          var faculties = await _facultyDataService.GetAsync();
          return faculties.Select(f =>
           new FacultyModel
           {
               Id = f.Id,
               Title = f.Title,
               Info = f.Info,
               Specialties = f.Specialties
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
        public async Task<IEnumerable<RecommendationFacultiesModel>> GetRecomendationsFacultiesAsync([FromBody] List<string> hobbies, [FromQuery] Guid userId) 
        {
            return await _recommendationsService.GetRecomendationsFacultiesAsync(hobbies, userId);
        }
    }
}

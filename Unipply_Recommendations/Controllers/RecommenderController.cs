using IndexService.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Reccomender.DataStructures;
using System.Collections.Generic;
using System.Linq;
using Unipply_Recommendations.DataStructures;
using Unipply_Recommendations.Models;
using Unipply_Recommendations.Repositories;
using Unipply_Recommendations.Services;

namespace Unipply_Recommendations.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecommenderController : ControllerBase
    {
        private readonly ILogger<RecommenderController> _logger;
        private readonly FacultyRecommender _facultyRecommender;
        private readonly FacultyRepository _facultyRepository;
        private readonly HobbyRepository _hobbyRepository;
       
        public RecommenderController(
            ILogger<RecommenderController> logger,
            FacultyRecommender facultyRecommender,
            FacultyRepository facultyRepository,
            HobbyRepository hobbyRepository)
        {
            _logger = logger;
            _facultyRecommender = facultyRecommender;
            _facultyRepository = facultyRepository;
            _hobbyRepository = hobbyRepository;
        }

        [HttpGet]
        [Route("hobbies")]
        public IEnumerable<HobbyModel> GetAllHobbies()
        {
            return _hobbyRepository.Get().Select(h => new HobbyModel { HobbyId = h.hobbyId, Title = h.title });
        }

            [HttpPost]
        [Route("faculties")]
        public IEnumerable<RecommendationModel> GetRecomendationsByHobbies([FromBody] List<string> hobbies)
        {
            HobbyEnum hobby = new();
            Dictionary<int, string> desiredHobbies = new();

            var desiredHobbiesIds = hobbies.Select(inputHobby =>
            {
                var hobbyId = hobby.hobbies.FirstOrDefault(x => x.Value.Trim().ToLower().Contains(inputHobby.Trim().ToLower())).Key;
                desiredHobbies.Add(hobbyId, inputHobby.Trim().ToLower());
                return hobbyId;
            });

            var facultyIds = desiredHobbiesIds
              .SelectMany(desiredHobby => _facultyRecommender.PredictFacultiesForHobby(desiredHobby, _facultyRecommender.predictionengine))
              .Distinct()
              .ToHashSet();

            //var duplicates = hobbiesIds.GroupBy(n => n).Where(c => c.Count() > 1).Select(c => c.Key).ToList();

            var result = facultyIds.Select(id =>
            {
                List<HobbyScore> matchHobbies = new();
                Faculty facultyService = new();

                foreach (var entry in desiredHobbies)
                {
                    matchHobbies.Add(new HobbyScore
                    {
                        HobbyTitle = entry.Value,
                        Score = _facultyRecommender.PredictPrecentMatchForHobby(entry.Key, id, _facultyRecommender.predictionengine)
                    });
                };

                return new RecommendationModel
                {
                    Title = facultyService.Get(id).title,
                    HobbiesData = matchHobbies
                };
            }).ToList();

            return result;
        }
    }
}

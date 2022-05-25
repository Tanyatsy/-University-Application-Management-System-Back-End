using IndexService.MessageBus;
using IndexService.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Reccomender.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using Unipply_Recommendations.DataStructures;
using Unipply_Recommendations.Messges.Commands;
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
        private readonly IMessageBusClient _messageBus;
        private readonly FacultyRecommender _facultyRecommender;
        private readonly FacultyRepository _facultyRepository;
        private readonly HobbyRepository _hobbyRepository;
        private readonly RecommendationDataRepository _recommendationDataRepository;
       
        public RecommenderController(
            ILogger<RecommenderController> logger,
            IMessageBusClient messageBus,
            FacultyRecommender facultyRecommender,
            FacultyRepository facultyRepository,
            HobbyRepository hobbyRepository,
            RecommendationDataRepository recommendationDataRepository)
        {
            _logger = logger;
            _messageBus = messageBus;
            _facultyRecommender = facultyRecommender;
            _facultyRepository = facultyRepository;
            _hobbyRepository = hobbyRepository;
            _recommendationDataRepository = recommendationDataRepository;
        }

        [HttpGet]
        [Route("hobbies")]
        public IEnumerable<HobbyModel> GetAllHobbies()
        {
            return _hobbyRepository.Get().Select(h => new HobbyModel { HobbyId = h.hobbyId, Title = h.title });
        }

        [HttpPost]
        [Route("application")]
        public IActionResult CreateRecommendationData([FromBody] List<RecommendationDataModel> datas)
        {
            Faculty facultyService = new(); 
            Hobby hobbyService = new();

            var lastRecommendationLabel = _recommendationDataRepository.GetLast().Label + 1;
            var recommendationData = datas.Select(data => new RecommendationData
            {
                _id = ObjectId.GenerateNewId(),
                Label = (lastRecommendationLabel++),
                HobbyID = (uint)hobbyService.GetOrCreate(data.Hobby).hobbyId,
                FacultyID = (uint)facultyService.Get(data.SpecialtyTitile).facultyId,
            }).ToList();

            _recommendationDataRepository.CreateManyAsync(recommendationData);
            _facultyRecommender.LoadData();

            return Ok(); 
        }

        [HttpPost]
        [Route("faculties")]
        public IEnumerable<RecommendationModel> GetRecomendationsByHobbies([FromQuery] Guid userId, [FromBody] List<string> hobbies)
        {
            HobbyEnum hobby = new();
            Dictionary<int, string> desiredHobbies = new();

            var hobbiesData = hobbies
                .Select(h => new HobbyModel
                    { 
                        HobbyId = hobby.hobbies.FirstOrDefault(x => x.Value.Trim().ToLower().Contains(h.Trim().ToLower())).Key,
                        Title = h 
                    })
                .ToList();

           // SendRecommendationHobbiesData(hobbiesData, userId);

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
                        HobbyId = entry.Key,
                        Score = _facultyRecommender.PredictPrecentMatchForHobby(entry.Key, id, _facultyRecommender.predictionengine)
                    });
                };

                return new RecommendationModel
                {
                    Title = facultyService.Get(id).title,
                    RecommendationScore = matchHobbies.Select(x => x.Score).Sum() / matchHobbies.Count,
                    HobbiesData = matchHobbies
                };
            }).ToList();


            SendRecommendationFacultiesData(result, userId);

            return result;
        }

        private void SendRecommendationFacultiesData(List<RecommendationModel> recommendationModel, Guid userId)
        {
            var recommendationCommand = new RecommendationFacultiesDataCommand
            {
                RecommendationFacultiesData = new RecommendationFaculties 
                {
                    UserId = userId,
                    RecommendationModel = recommendationModel
                }
            };

            foreach (var @event in recommendationCommand.Events)
            {
                var routingKey = "api-recommendationFaculties";

                _messageBus.Publish(
                    message: @event,
                    routingKey: routingKey,
                    exchange: "api-service"
                );
            }
        }

        private void SendRecommendationHobbiesData(List<HobbyModel> hobbies, Guid userId)
        {
            var recommendationCommand = new RecommendationHobbiesDataCommand
            {
                HobbyData = new HobbyData
                {
                    UserId = userId,
                    Hobbies = hobbies
                }
            };

            foreach (var @event in recommendationCommand.Events)
            {
                var routingKey = "api-recommendationHobbies";

                _messageBus.Publish(
                    message: @event,
                    routingKey: routingKey,
                    exchange: "api-service"
                );
            }
        }
    }
}

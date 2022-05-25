using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Unipply.Models;
using Unipply.Models.Recommendation;

namespace Unipply.Services
{
    public class RecommendationsService : IRecommendationsService
    {  
        private readonly IRecommendationsIteractor _recommendationsIteractor;
        private readonly IFacultyDataService _facultyDataService;
        private readonly ISpecialtyDataService _specialtyDataService;


        public RecommendationsService(
            IFacultyDataService facultyDataService,
            IRecommendationsIteractor recommendationsIteractor,
            ISpecialtyDataService specialtyDataService)
        {
            _facultyDataService = facultyDataService;
            _specialtyDataService = specialtyDataService;
            _recommendationsIteractor = recommendationsIteractor;
        }

        public async Task<IEnumerable<RecommendationFacultiesModel>> GetRecomendationsFacultiesAsync(List<string> hobbies, Guid userId)
        {
            var response = await _recommendationsIteractor.GetRecomendationsFacultiesAsync(hobbies, userId);
            var recommendations = JsonConvert
                     .DeserializeObject<List<RecommendationModel>>(await response.Content.ReadAsStringAsync());
            return ParseRecomendationsFaculties(recommendations);
        }

        public async Task<IEnumerable<HobbyModel>> GetRecomendationsHobbiesAsync()
        {
            var response = await _recommendationsIteractor.GetRecomendationsHobbiesAsync();
            return JsonConvert
                       .DeserializeObject<List<HobbyModel>>(await response.Content.ReadAsStringAsync());
        }

        public IEnumerable<RecommendationFacultiesModel> ParseRecomendationsFaculties(List<RecommendationModel> recommendations)
        {
            var recommendationSpecilaties = recommendations.Where(x => _specialtyDataService.GetSpecialtyByTitle(x.Title) != null);
            var result = recommendations.Select(x =>
            {
                var faculty = _facultyDataService.GetFacultyByTitle(x.Title);
                var specialty = _specialtyDataService.GetSpecialtyByTitle(x.Title);

                if (faculty != null)
                {
                    var validSpecialties = recommendationSpecilaties.Where(x => faculty.Specialties.Any(s => s.Title == x.Title));
                    var recommendation = new RecommendationFacultiesModel
                    {
                        FacultyTitle = faculty.Title,
                        RecommendationScore = x.RecommendationScore,
                        Specialties = validSpecialties.Select(specilaty => new Specialty
                        {
                            Title = specilaty.Title,
                            Score = specilaty.RecommendationScore,
                            HobbiesData = specilaty.HobbiesData
                        }).ToList()
                    };
                    return recommendation;
                }
                else if (specialty != null)
                {
                    var validSpecialties = recommendationSpecilaties.Where(x => specialty.Faculty.Specialties.Any(s => s.Title == x.Title));
                    var recommendation = new RecommendationFacultiesModel
                    {
                        FacultyTitle = specialty.Faculty.Title,
                        RecommendationScore = x.RecommendationScore,
                        Specialties = validSpecialties.Select(specilaty => new Specialty
                        {
                            Title = specilaty.Title,
                            Score = specilaty.RecommendationScore,
                            HobbiesData = specilaty.HobbiesData
                        }).ToList()
                    };
                    return recommendation;
                }

                return null;

            }).Where(x => x != null && x.Specialties.Count() > 0).Distinct().ToList();

            var grouped = result.GroupBy(x => x.FacultyTitle).ToList();

            foreach (var group in grouped)
            {

                var duplicates = group.ToList();

                foreach (var d in duplicates)
                {
                    result.Remove(d);
                }

                var single = duplicates.OrderByDescending(x => x.Specialties.Count()).First();
                result.Add(single);
            }

            return result;
        }
    }

    public interface IRecommendationsService
    {
        IEnumerable<RecommendationFacultiesModel> ParseRecomendationsFaculties(List<RecommendationModel> recommendations);
        Task<IEnumerable<RecommendationFacultiesModel>> GetRecomendationsFacultiesAsync(List<string> hobbies, Guid userId);
        Task<IEnumerable<HobbyModel>> GetRecomendationsHobbiesAsync();
    }
}

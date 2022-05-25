using System.Collections.Generic;

namespace Unipply.Models.Recommendation
{
    public class RecommendationFacultiesModel
    {
        public string FacultyTitle { get; set; }
        public int RecommendationScore { get; set; }
        public List<Specialty> Specialties { get; set; }
    }
   
    public class Specialty
    {
        public string Title { get; set; }
        public int Score { get; set; }
        public List<HobbyScore> HobbiesData { get; set; }
    }
}

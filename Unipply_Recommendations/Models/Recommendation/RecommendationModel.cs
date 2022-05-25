using System.Collections.Generic;

namespace Unipply_Recommendations.Models
{
    public class RecommendationModel
    {
        public string Title { get; set; }
        public int RecommendationScore { get; set; }
        public List<HobbyScore> HobbiesData { get; set; }
    }
}

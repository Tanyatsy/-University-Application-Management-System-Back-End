using System.Collections.Generic;

namespace Unipply.Models.Recommendation
{
    public class RecommendationModel
    {
        public string Title { get; set; }
        public int RecommendationScore { get; set; }
        public List<HobbyScore> HobbiesData { get; set; }
    }

    public class HobbyScore
    {
        public string HobbyTitle { get; set; }
        public int HobbyId { get; set; }
        public int Score { get; set; }
    }
}

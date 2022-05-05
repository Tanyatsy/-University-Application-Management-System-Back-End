using System.Collections.Generic;

namespace Unipply.Models
{
    public class RecommendationModel
    {
        public string Title { get; set; }
        public List<HobbyScore> HobbiesData { get; set; }
    }
    public class HobbyScore
    {
        public string HobbyTitle { get; set; }
        public int Score { get; set; }
    }
}

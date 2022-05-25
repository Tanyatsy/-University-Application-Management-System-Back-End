using System;
using System.Collections.Generic;

namespace Unipply_Recommendations.Models
{
    public class RecommendationFaculties
    {
        public Guid UserId { get; set; }
        public List<RecommendationModel> RecommendationModel { get; set; }
    }
}

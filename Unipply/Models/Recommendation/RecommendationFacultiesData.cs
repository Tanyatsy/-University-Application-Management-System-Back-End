using System;
using System.Collections.Generic;

namespace Unipply.Models.Recommendation
{
    public class RecommendationFacultiesData
    {
        public Guid UserId { get; set; }
        public List<RecommendationModel> RecommendationModel { get; set; }
    }
}

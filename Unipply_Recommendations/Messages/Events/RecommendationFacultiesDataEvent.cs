using Unipply_Recommendations.Models;
using Users.Domain.Events.Interfaces;

namespace Unipply_Recommendations.Messages.Events
{
    public class RecommendationFacultiesDataEvent : IDomainEvent {
        public RecommendationFacultiesDataEvent(RecommendationFaculties recommendationFaculties)
        {
            this.RecommendationFacultiesData = recommendationFaculties;
        }
        public RecommendationFaculties RecommendationFacultiesData { get; set; }
    }
}

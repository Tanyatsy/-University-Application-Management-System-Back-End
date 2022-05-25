using Unipply.Models.Recommendation;
using Users.Domain.Events.Interfaces;

namespace Unipply.Messages.Events
{
    public class RecommendationFacultiesDataEvent : IDomainEvent {
        public RecommendationFacultiesDataEvent(RecommendationFacultiesData recommendationFaculties)
        {
            this.RecommendationFacultiesData = recommendationFaculties;
        }
        public RecommendationFacultiesData RecommendationFacultiesData { get; set; }
    }
}

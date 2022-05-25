using Unipply_Recommendations.Models;
using Users.Domain.Events.Interfaces;

namespace Unipply_Recommendations.Messages.Events
{
    public class RecommendationHobbiesDataEvent : IDomainEvent {
        public RecommendationHobbiesDataEvent(HobbyData hobbyData)
        {
            this.hobbyData = hobbyData;
        }
        public HobbyData hobbyData { get; set; }
    }
}

using Unipply_Recommendations.Messages;
using Unipply_Recommendations.Messages.Events;
using Unipply_Recommendations.Models;

namespace Unipply_Recommendations.Messges.Commands
{
    public class RecommendationHobbiesDataCommand : AggregateRoot
    {
        private HobbyData hobbyData { get; set; }
        public HobbyData HobbyData
        {
            get
            {
                return hobbyData;
            }
            set
            {
                hobbyData = value;
                AddEvent(new RecommendationHobbiesDataEvent(hobbyData));
            }
        }
    }
}

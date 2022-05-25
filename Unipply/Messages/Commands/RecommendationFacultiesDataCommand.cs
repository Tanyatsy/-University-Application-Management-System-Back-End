using Unipply.Messages.Events;
using Unipply.Models.Recommendation;
using Unipply.Messages;

namespace Unipply.Messges.Commands
{
    public class RecommendationFacultiesDataCommand : AggregateRoot
    {
        private RecommendationFacultiesData recommendationFacultiesData { get; set; }
        public RecommendationFacultiesData RecommendationFacultiesData
        {
            get
            {
                return recommendationFacultiesData;
            }
            set
            {
                recommendationFacultiesData = value;
                AddEvent(new RecommendationFacultiesDataEvent(recommendationFacultiesData));
            }
        }
    }
}

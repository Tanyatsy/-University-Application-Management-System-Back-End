using Unipply_Recommendations.Messages;
using Unipply_Recommendations.Messages.Events;
using Unipply_Recommendations.Models;

namespace Unipply_Recommendations.Messges.Commands
{
    public class RecommendationFacultiesDataCommand : AggregateRoot
    {
        private RecommendationFaculties recommendationFacultiesData { get; set; }
        public RecommendationFaculties RecommendationFacultiesData
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

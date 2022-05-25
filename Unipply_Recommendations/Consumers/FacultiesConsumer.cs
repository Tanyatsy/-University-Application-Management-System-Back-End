using System;
using System.Threading.Tasks;
using Unipply_Recommendations.Models;

namespace IndexService.Consumers
{
    public class FacultiesConsumer :
        AbstractConsumer<RecommendationFaculties>
    {
        public FacultiesConsumer(
            IServiceProvider serviceProvider
        ) : base(serviceProvider)
        {
            Queue = "recommender-service/faculties";

            _routingKey = "recommender-faculties";

            _exchange = "recommender-service";

            InitializeEventBus();
        }

        protected override void LogMessageReceived(RecommendationFaculties message)
        {
        }

        protected override Task SendData(RecommendationFaculties message)
        {
            return Task.CompletedTask;
        }
    }
}

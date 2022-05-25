using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using Unipply_Recommendations.Services;

namespace Unipply_Recommendations.Repositories
{
    public class RecommendationDataRepository
    {
        private readonly IMongoCollection<RecommendationData> _recommendationData;
        public RecommendationDataRepository()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("Unipply");

            _recommendationData = database.GetCollection<RecommendationData>("RecommendationData");
        }

        public IEnumerable<RecommendationData> Get()
        {
            var recommendationData = _recommendationData.Find(data => true).ToEnumerable();
            return recommendationData;
        }

        public RecommendationData GetLast()
        {
            var recommendationData = _recommendationData.Find(data => true).ToEnumerable();
            return recommendationData.Last();
        }

        public void CreateManyAsync(List<RecommendationData> data)
        {
            _recommendationData.InsertManyAsync(data);
        }
    }
}

using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using Unipply_Recommendations.DataStructures;

namespace IndexService.Repositories
{
    public class HobbyRepository
    {
        private readonly IMongoCollection<Hobby> _hobbies;
        public HobbyRepository()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("Unipply");

            _hobbies = database.GetCollection<Hobby>("Hobbies");
        }

        public List<Hobby> Get()
        {
            var hobbies = _hobbies.Find(hobby => true).ToList();
            return hobbies;
        }

        /*  public void CreateManyAsync(List<Hobby> hobbies)
          {
              _hobbies.InsertManyAsync(hobbies);
          }*/
    }
}

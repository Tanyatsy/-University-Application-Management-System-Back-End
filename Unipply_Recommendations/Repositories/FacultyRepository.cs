using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using Unipply_Recommendations.DataStructures;

namespace Unipply_Recommendations.Repositories
{
    public class FacultyRepository
    {
        private readonly IMongoCollection<Faculty> _faculties;
        public FacultyRepository()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("Unipply");

            _faculties = database.GetCollection<Faculty>("Faculties");
        }

        public List<Faculty> Get()
        {
            var faculties = _faculties.Find(faculty => true);
            return faculties.ToList();
        }

        public void CreateManyAsync(List<Faculty> faculties)
        {
            _faculties.InsertManyAsync(faculties);
        }
    }
}

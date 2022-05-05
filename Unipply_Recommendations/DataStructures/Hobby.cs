using IndexService.Repositories;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Unipply_Recommendations.DataStructures
{
    public class Hobby
    {
        public ObjectId Id { get; set; }
        public int hobbyId { get; set; }
        public string title { get; set; }

        public Lazy<List<Hobby>> _hobbies = new Lazy<List<Hobby>>(() => LoadHobbieData());
        
        public Hobby()
        {
        }

        public Hobby Get(int id)
        {
            return _hobbies.Value.Single(m => m.hobbyId == id);
        }

        private static List<Hobby> LoadHobbieData()
        {
            var hobbyRepository = new HobbyRepository();
            return hobbyRepository.Get();
        }
    }
}

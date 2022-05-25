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

        public static  HobbyRepository hobbyRepository = new HobbyRepository();

        public Lazy<List<Hobby>> _hobbies = new Lazy<List<Hobby>>(() => LoadHobbieData());
        
        public Hobby()
        {
        }

        public Hobby Get(int id)
        {
            return _hobbies.Value.Single(m => m.hobbyId == id);
        }

        public Hobby Get(string hobby)
        {
            return _hobbies.Value.Single(h => h.title.Trim().ToLower().Contains(hobby.Trim().ToLower()));
        }

        public Hobby GetOrCreate(string hobby)
        {
            var result = _hobbies.Value.Find(h => h.title.Trim().ToLower().Contains(hobby.Trim().ToLower()));
            if (result == null)
            {
                return hobbyRepository.Create(new Hobby
                {
                    Id = ObjectId.GenerateNewId(),
                    title = hobby.Trim(),
                    hobbyId = hobbyRepository.GetLast().hobbyId + 1
                });
            }
            else 
            {
                return result;
            }
        }

        private static List<Hobby> LoadHobbieData()
        {
            return hobbyRepository.Get();
        }
    }
}

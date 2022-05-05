using IndexService.Repositories;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using Unipply_Recommendations.Repositories;

namespace Unipply_Recommendations.DataStructures
{
    [BsonIgnoreExtraElements]
    public class Faculty
    {
        public ObjectId _id { get; set; }
        public int facultyId { get; set; }
        public string title { get; set; }

        public Lazy<List<Faculty>> _faculties = new Lazy<List<Faculty>>(() => LoadFacultyDataAsync());

        public Faculty()
        {
        }

        public Faculty Get(int id)
        {
            return _faculties.Value.Single(m => m.facultyId == id);
        }
      
        private static List<Faculty> LoadFacultyDataAsync()
        {
            FacultyRepository facultyRepository = new();

            return facultyRepository.Get();
        }
    }
}

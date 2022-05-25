using System;
using System.Collections.Generic;
using Unipply.Models.Recommendation;
using Unipply.Models.Specialty;

namespace Unipply.Models.User
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public int Age { get; set; }
        public string AboutMe { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string Avatar { get; set; }
        public List<string> Documents { get; set; }
        public List<SpecialtyModel> FavoriteSpecialties { get; set; }
        public List<HobbyModel> Hobbies { get; set; }
        public List<RecommendationFacultiesModel> Recommendations { get; set; }
    }
}

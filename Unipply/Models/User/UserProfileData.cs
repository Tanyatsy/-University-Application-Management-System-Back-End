using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Unipply.Models.Faculty;
using Unipply.Models.Recommendation;
using Unipply.Models.Specialty;

namespace Unipply.Models.User
{
    public class UserProfileData
    {
        public Guid Id { get; set; }
        [ForeignKey("UserDataId")]
        public Guid UserDataId { get; set; }
        public int Age { get; set; }
        public string AboutMe { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public string Avatar { get; set; }
        public List<string> Documents { get; set; }
        [Column(TypeName = "json")]
        public List<HobbyModel> Hobbies { get; set; }
        [Column(TypeName = "json")]
        public List<RecommendationModel> Recommendations { get; set; }


        public virtual UserData UserData { get; set; }
        public virtual ICollection<FacultyData> FavouritesFaculties { get; set; }
        public virtual ICollection<SpecialtyData> FavouritesSpecialties { get; set; }
    }
}

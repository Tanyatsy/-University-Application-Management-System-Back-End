using System;
using System.ComponentModel.DataAnnotations;

namespace Unipply.Models.User
{
    public class SpecialtyDataUserProfileData
    {
        [Key]
        public Guid FavouritesSpecialtiesId { get; set; }
        [Key]
        public Guid UserProfileDatasId { get; set; }
    }
}

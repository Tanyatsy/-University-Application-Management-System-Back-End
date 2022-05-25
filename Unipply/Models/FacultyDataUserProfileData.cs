using System;
using System.ComponentModel.DataAnnotations;

namespace Unipply.Models.User
{
    public class FacultyDataUserProfileData
    {
        [Key]
        public Guid FavouritesFacultiesId { get; set; }
        [Key]
        public Guid UserProfileDatasId { get; set; }
    }
}

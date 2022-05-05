using System;
using System.ComponentModel.DataAnnotations.Schema;
using Unipply.Models.Faculty;

namespace Unipply.Models.Specialty
{
    [Table("SpecialtyData")]
    public class SpecialtyData
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        [ForeignKey("FacultyId")]
        public Guid FacultyId { get; set; }

        public virtual FacultyData Faculty { get; set; }
    }
}

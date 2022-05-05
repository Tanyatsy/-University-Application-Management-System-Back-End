using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Unipply.Models.Specialty;

namespace Unipply.Models.Faculty
{

    [Table("FacultyData")]
    public class FacultyData
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Info { get; set; }
        public virtual ICollection<SpecialtyData> Specialties { get; set; }
    }
}

using System;
using System.Collections.Generic;
using Unipply.Models.Specialty;

namespace Unipply.Models.Faculty
{
    public class FacultyModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Info { get; set; }
        public List<SpecialtyModel> Specialties { get; set; }
    }
}

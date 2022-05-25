using System;
using System.Collections.Generic;

namespace Unipply.Models
{
    public class HobbyData
    {
        public Guid UserId { get; set; }
        public List<HobbyModel> Hobbies {get; set;}
    }
}

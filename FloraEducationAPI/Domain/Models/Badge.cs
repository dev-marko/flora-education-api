using FloraEducationAPI.Domain.Models.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloraEducationAPI.Domain.Models
{
    public class Badge : BaseEntity
    {
        public User Owner { get; set; }
        public string PlantName { get; set; }
    }
}

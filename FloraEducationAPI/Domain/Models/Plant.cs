using FloraEducationAPI.Domain.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloraEducationAPI.Domain.Models
{
    public class Plant : BaseEntity
    {
        public string Name { get; set; }
        public PlantType Type { get; set; }
        public string Description { get; set; }
        public string Predispositions { get; set; }
        public string Maintenance { get; set; }
    }
}

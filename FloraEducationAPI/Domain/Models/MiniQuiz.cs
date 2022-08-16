using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloraEducationAPI.Domain.Models
{
    public class MiniQuiz : BaseEntity
    {
        public Plant Plant { get; set; }
        public string Title { get; set; }
    }
}

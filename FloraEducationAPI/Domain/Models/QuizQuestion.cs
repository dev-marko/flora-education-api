using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FloraEducationAPI.Domain.Models
{
    public class QuizQuestion : BaseEntity
    {
        public MiniQuiz Quiz { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}

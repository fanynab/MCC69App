using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class JobHistory
    {
        [Key]
        [ForeignKey("Employee")]
        public int Id { get; set; }
        public virtual Employee Employee { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public virtual Job Job { get; set; }
        [ForeignKey("Job")]
        public int Job_Id { get; set; }

        public virtual Department Department { get; set; }
        [ForeignKey("Department")]
        public int Department_Id { get; set; }
    }
}

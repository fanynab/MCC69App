using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Location
    {
        [Key]
        public int Id { get; set; }
        public string StreetAddress { get; set; }
        public int PostalCode { get; set; }
        public string City { get; set; }

        public virtual Country Country { get; set; }
        [ForeignKey("Country")]
        public int Country_Id { get; set; }
    }
}

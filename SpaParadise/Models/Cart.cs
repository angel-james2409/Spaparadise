using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SpaParadise.Models
{
    public class Cart
    {

        [Key]
        public int CartId { get; set; }
        public int ServiceId { get; set; }

        public string ServiceName { get; set; }
        public int CustomerId { get; set; }
        [DataType(DataType.Date)]
        public DateTime? BookingDate { get; set; }
        [DataType(DataType.Time)]
        public DateTime? BookingTime { get; set; }

        public string Specialist { get; set; }
        public double Amount { get; set; }

    }
}
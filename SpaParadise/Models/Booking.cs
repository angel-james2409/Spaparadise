using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SpaParadise.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; }
        public int ServiceId { get; set; }
        public int CustomerId { get; set; }

        [Required]
        [ExcludeChar("!@.")]
        public string ServiceName { get; set; }
        [Required]
        [ExcludeChar("!@.")]
        public string Specialist { get; set; }

        [DataType(DataType.Time)]
        public DateTime? BookingTime { get; set; }

        [DataType(DataType.Date)]
        public DateTime? BookingDate { get; set; }
        public string Location { get; set; }
        public int Amount { get; set; }

    }
}
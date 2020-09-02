using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SpaParadise.Models
{
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }
        [Required]
        [ExcludeChar("!@.")]
        public string ServiceName { get; set; }
        [Required]
        [ExcludeChar("!@.")]
        public string ServiceType { get; set; }


        [DisplayName("UploadImage")]
        public string ServiceImage { get; set; }

        [Required]
        [ExcludeChar("!@.")]
        public String Specialist { get; set; }
        [Required]
        [ExcludeChar("!@.")]
        public string ServiceDescription { get; set; }

        public int Amount { get; set; }

    }
}
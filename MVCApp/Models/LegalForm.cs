using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCApp.Models
{
    public class LegalForm
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(100)]
        public string FilePath { get; set; }

        [Required]
        [StringLength(300)]
        public string Reason { get; set; }
    }
}
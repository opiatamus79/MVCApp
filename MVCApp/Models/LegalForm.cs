using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCApp.DataAccess
{
    public class LegalForm
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string FilePath { get; set; }

        [Required]
        [StringLength(50)]
        public string Reason { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCApp.DataAccess
{
    public class FormStatus
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(20)]
        public string StatusName { get; set; }

        [Required]
        [StringLength(20)]
        public string Description { get; set; }

    }



}
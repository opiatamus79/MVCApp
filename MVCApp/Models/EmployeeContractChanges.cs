using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCApp.Models
{
    public class EmployeeContractChanges
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string NewLastName { get; set; }

        [Required]
        [StringLength(200)]
        public string NewEmail { get; set; }

        [Required]
        [StringLength(50)]
        public string NewAddress { get; set; }

        [Required]
        [StringLength(50)]
        public string NewCity { get; set; }

        [Required]
        [StringLength(50)]
        public string NewState { get; set; }

        public int NewZipcode { get; set; }

        [Required]
        [StringLength(50)]
        public string NewCountry { get; set; }

        [StringLength(20)]
        public string NewHomePhone { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateCreated { get; set; }

        //[Required]
        public int StatusID { get; set; }

        //[Required]
        public int LegalFormsID { get; set; }

        //[Required]
        public int EmployeeID { get; set; }


        [ForeignKey("StatusID")]
        public virtual FormStatus FormStatus { get; set; }


        [ForeignKey("LegalFormsID")]
        public virtual LegalForm LegalForm { get; set; }

        [ForeignKey("EmployeeID")]
        public virtual Employee Employee { get; set; }
    }
}
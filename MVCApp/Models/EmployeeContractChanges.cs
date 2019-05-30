using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVCApp.DataAccess
{
    public class EmployeeContractChanges
    {
        [Key]
        public int ID { get; set; }


        [Required]
        [StringLength(50)]
        [DisplayName("Last Name")]
        public string NewLastName { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Email")]
        public string NewEmail { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Address")]
        public string NewAddress { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("City")]
        public string NewCity { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("State")]
        public string NewState { get; set; }

        [DisplayName("Zipcode")]
        public int NewZipcode { get; set; }

        [Required]
        [StringLength(50)]
        [DisplayName("Country")]
        public string NewCountry { get; set; }


        [StringLength(20)]
        [DisplayName("Home Phone")]
        public string NewHomePhone { get; set; }


        [Column(TypeName = "date")]
        public DateTime DateCreated { get; set; }

        public int ChangeLogID { get; set; }

        [Required]
        public int StatusID { get; set; }

        [Required]
        public int LegalFormsID { get; set; }

        [Required]
        public int EmployeeID { get; set; }


        [ForeignKey("StatusID")]
        public virtual FormStatus FormStatus { get; set; }


        [ForeignKey("LegalFormsID")]
        public virtual LegalForm LegalForm { get; set; }

        [ForeignKey("EmployeeID")]
        public virtual Employee Employee { get; set; }
    }
}
namespace MVCApp.Models.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
   

    //[Table("Employee")]
    public partial class Employee
    {
        [Key]
        public int ID { get; set; }

        public int StaffID { get; set; }

        [Required]
        [StringLength(50)]
        public string HRID { get; set; }

        [Column(TypeName = "date")]
        public DateTime LastUpdate { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        [StringLength(50)]
        public string Address { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [Required]
        [StringLength(50)]
        public string State { get; set; }

        public int Zipcode { get; set; }

        [Required]
        [StringLength(50)]
        public string Country { get; set; }

        [StringLength(20)]
        public string HomePhone { get; set; }

        public bool IsActive { get; set; }

        public Guid ActivationCode { get; set; }

        public virtual ICollection<Role> Roles { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateCreated { get; set; }
    }
}

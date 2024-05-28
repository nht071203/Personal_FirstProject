using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Personal_FirstProject.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required (ErrorMessage = "Enter your First Name")]
        [StringLength(100, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required (ErrorMessage = "Enter your Last Name")]
        [StringLength(100, MinimumLength = 2)]
        public string LastName { get; set; }

        [Required (ErrorMessage = "Enter your Date Of Birth")]
        public DateTime DateOfBirth { get; set; }

        [Required (ErrorMessage = "Enter your Gender")]
        [StringLength(6)]
        public string Gender { get; set; }

        [Required  (ErrorMessage = "Enter your Address")]
        [StringLength(200)]
        public string Address { get; set; }

        [Required (ErrorMessage = "Enter your Zip Code")]
        [StringLength(10)]
        public string ZipCode { get; set; }

        [Required (ErrorMessage = "Enter your Phone Number")]
        [StringLength(15, MinimumLength = 10, ErrorMessage = "Please enter your real Phone Number")]
        public string PhoneNumber { get; set; }

        [Required (ErrorMessage = "Enter your Email")]
        [StringLength(100, MinimumLength = 10, ErrorMessage = "Please enter your real Email")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Invalid Email Format"),]
        public string Email { get; set; }

        [Required (ErrorMessage = "Enter your Username")]
        [StringLength(30, MinimumLength = 8, ErrorMessage = "User name should be from 8 to 30 characters")]
        public string Username { get; set; }

        [Required (ErrorMessage = "Enter your ID Card/Passport Number")]
        [StringLength(50, MinimumLength = 10, ErrorMessage = "Please enter your real ID Card/Passport Number")]
        public string IDCardPassportNumber { get; set; }

        [Required (ErrorMessage = "Enter your Password")]
        [StringLength(32, MinimumLength = 8, ErrorMessage = "Password should be from 8 to 32 characters")]
        public string PasswordHash { get; set; }

        [Required (ErrorMessage = "Enter your Role")]
        [StringLength(15)]
        public string Role { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public virtual ICollection<ActivityHistory> ActivityHistories { get; set; }
        public virtual ICollection<EmailQueue> EmailQueues { get; set; }
        public virtual ICollection<OTP> OTPs { get; set; }
    }
}
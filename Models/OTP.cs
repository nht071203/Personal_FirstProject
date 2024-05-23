using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Personal_FirstProject.Models
{
    public class OTP
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OTPId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        [StringLength(10)]
        public string OTPCode { get; set; }

        [Required]
        public DateTime ExpiresAt { get; set; }

        [Required]
        public bool IsUsed { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
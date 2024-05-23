using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Personal_FirstProject.Models
{
    public class EmailQueue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmailQueueId { get; set; }

        public int? UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        [Required]
        [StringLength(200)]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public bool IsSent { get; set; } = false;

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? SentAt { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Personal_FirstProject.Models
{
    public class ActivityHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ActivityId { get; set; }
        public int? UserId { get; set; }
        [Required]
        [StringLength(10)]
        public string Activity { get; set; }
        [Required]
        public DateTime Timestamp { get; set; } = DateTime.Now;
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
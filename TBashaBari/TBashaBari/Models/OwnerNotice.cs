using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TBashaBari.Models
{
    public class OwnerNotice
    {
        [Key]
        public int NoticeId { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        [DisplayName("Email")]
        public String OwnerEmail { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 1, ErrorMessage = "Maximum text length 60")]
        [DisplayName("Notice")]
        public String NoticeText { get; set; }

        [Required]
        [DisplayName("Time")]
        public String NoticeTime { get; set; }
        
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TBashaBari.Models
{
    public class TenantRequest
    {
        [Key]
        public int RequestId { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Email is not valid")]
        [DisplayName("Email")]
        public String TenantEmail { get; set; }

        [Required]
        [StringLength(60, MinimumLength = 1, ErrorMessage = "Maximum text length 60")]
        [DisplayName("Request")]
        public String RequestText { get; set; }

        [Required]
        [DisplayName("Time")]
        public String RequestTime { get; set; }

        [StringLength(60, MinimumLength = 1, ErrorMessage = "Maximum text length 60")]
        [DisplayName("Comment")]
        public String CommentOnRequestText { get; set; }

    }
}

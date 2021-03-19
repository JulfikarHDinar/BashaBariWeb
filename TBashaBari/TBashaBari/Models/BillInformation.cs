using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TBashaBari.Models
{
    public class BillInformation
    {
        [Key]
        public int BillId { get; set; }

        [Required]
        public String OwnerEmail { get; set; }

        [Required]
        public String TenantEmail { get; set; }

        [Required]
        public String BillTime { get; set; }

        [Required]
        public String WaterAmount { get; set; }

        [Required]
        public String WaterPaid { get; set; }

        [Required]
        public String WaterVerified { get; set; }

        [Required]
        public String ElectricAmount { get; set; }

        [Required]
        public String ElectricPaid { get; set; }

        [Required]
        public String ElectricVerified { get; set; }

        [Required]
        public String RentAmount { get; set; }

        [Required]
        public String RentPaid { get; set; }

        [Required]
        public String RentVerified { get; set; }

        [Required]
        public String GasAmount { get; set; }

        [Required]
        public String GasPaid { get; set; }

        [Required]
        public String GasVerified { get; set; }
    }
}

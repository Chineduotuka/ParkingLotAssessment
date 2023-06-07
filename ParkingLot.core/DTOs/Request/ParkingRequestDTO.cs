using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLot.core.DTOs.Request
{
    public class ParkingRequestDTO
    {
        [Required]
        public string? VehicleType { get; set; }
        [Required]
        public string? VehicleNumber { get; set; }
        [Required]
        public string? LocationName { get; set; }
    }
}

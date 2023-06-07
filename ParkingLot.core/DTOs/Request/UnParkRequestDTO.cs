using ParkingLot.Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLot.core.DTOs.Request
{
    public class UnParkRequestDTO
    {
        [Required]
        public string? TicketNUmber { get; set; }
        [Required]
        public string? VehicleNumber { get; set; }
        [Required]
        public string? location { get; set; }
    }
}

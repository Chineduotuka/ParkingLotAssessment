using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLot.Domain.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string? VehicleType { get; set; }
        public string? VehicleNumber { get; set; }
        public string? TicketNumber { get; set; }
        public int Spot { get; set; }
        public bool IsParked { get; set; }
        public DateTime EntryDate { get; set; }
        public string? ParkingTicket { get; set; }
        public string? ParkingReciept { get; set; }
        public int LocationId { get; set; }
        public Location? Location { get; set; }
    }
}

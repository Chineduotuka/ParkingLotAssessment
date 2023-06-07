using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLot.core.DTOs.Response
{
    public class ParkingTicket
    {
        public string? TicketNumber { get; set; }
        public long SpotNumber { get; set; }
        public DateTime EntryDate { get; set; }
    }
}

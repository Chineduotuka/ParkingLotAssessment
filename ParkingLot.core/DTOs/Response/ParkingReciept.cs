using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLot.core.DTOs.Response
{
    public class ParkingReciept
    {
        public string ReceiptNumber { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime ExitDate { get; set; }
        public decimal Fees { get; set; }
    }
}

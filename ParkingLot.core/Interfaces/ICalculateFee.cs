using ParkingLot.core.DTOs.Response;

namespace ParkingLot.core.Interfaces
{
    public interface ICalculateFee
    {
        ApiResponse<decimal> CalculateFees(string location, ParkingTicket ticket, string carType);
    }
}
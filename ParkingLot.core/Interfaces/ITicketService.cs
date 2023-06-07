using ParkingLot.core.DTOs.Request;
using ParkingLot.core.DTOs.Response;
using ParkingLot.Domain.Models;

namespace ParkingLot.core.Interfaces
{
    public interface ITicketService
    {
        Task<ApiResponse<ParkingTicket>> BookTicket(Location location, ParkingRequestDTO request);
    }
}
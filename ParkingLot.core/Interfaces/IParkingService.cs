using ParkingLot.core.DTOs.Request;
using ParkingLot.core.DTOs.Response;

namespace ParkingLot.core.Interfaces
{
    public interface IParkingService
    {
        Task<ApiResponse<ParkingTicket>> ParkVehicle(ParkingRequestDTO request);
        Task<ApiResponse<ParkingReciept>> UnParkVehicle(UnParkRequestDTO request);
    }
}
namespace ParkingLot.core.Interfaces
{
    public interface IParkingServiceHelper
    {
        Task<bool> CanPark(string location, string carType);
        Task<string> GetVehicleClass(string carType);
    }
}
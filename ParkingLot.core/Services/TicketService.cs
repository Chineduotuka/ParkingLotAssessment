using Newtonsoft.Json;
using ParkingLot.core.DTOs.Request;
using ParkingLot.core.DTOs.Response;
using ParkingLot.core.Interfaces;
using ParkingLot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLot.core.Services
{
    public class TicketService : ITicketService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TicketService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<ParkingTicket>> BookTicket(Location location, ParkingRequestDTO request)
        {
            try
            {
                await resetDailyCount(location);

                if (request.VehicleType.ToLowerInvariant() == "suv" || request.VehicleType.ToLowerInvariant() == "car")
                {
                    if (location.SuvsAndCarsAvailableSpots < 1)
                    {
                        return ApiResponse<ParkingTicket>.Fail($"All Spot a filled up for this vehicle type, try a different location", (int)HttpStatusCode.BadRequest);
                    }

                    var spot = MaxSpots.SuvsAndCars - location.SuvsAndCarsAvailableSpots;
                    var ticketNumber = (location.DailyCount + 1).ToString("D3");

                    ParkingTicket ticket = new ParkingTicket
                    {
                        TicketNumber = ticketNumber,
                        SpotNumber = spot + 1,
                        EntryDate = DateTime.Now,
                    };

                    string serializedTicket = JsonConvert.SerializeObject(ticket);

                    var vehicle = new Vehicle
                    {
                        Spot = spot + 1,
                        IsParked = true,
                        LocationId = location.Id,
                        TicketNumber = ticketNumber,
                        VehicleNumber = request.VehicleNumber,
                        VehicleType = request.VehicleType,
                        ParkingTicket = serializedTicket,
                        EntryDate = DateTime.Now
                    };

                    location.SuvsAndCarsAvailableSpots -= 1;
                    location.TotalAvailableSpots -= 1;
                    location.LastIssuedTicket = DateTime.Now;
                    location.DailyCount += 1;

                    _unitOfWork.LocationRepository.Update(location);

                    await _unitOfWork.VehicleRepository.AddAsync(vehicle);
                    await _unitOfWork.SaveAsync();

                    return ApiResponse<ParkingTicket>.Success($"Spot booked for {request.VehicleType}", ticket);
                }
                else if (request.VehicleType.ToLowerInvariant() == "bus" || request.VehicleType.ToLowerInvariant() == "truck")
                {
                    if (location.BusesAndTrucksAvailableSpots < 1)
                    {
                        return ApiResponse<ParkingTicket>.Fail($"All Spot a filled up for this vehicle type, try a different location", (int)HttpStatusCode.BadRequest);
                    }

                    var spot = MaxSpots.BusesAndTrucks - location.BusesAndTrucksAvailableSpots;
                    var ticketNumber = (location.DailyCount + 1).ToString("D3");

                    ParkingTicket ticket = new ParkingTicket
                    {
                        TicketNumber = ticketNumber,
                        SpotNumber = spot + 1,
                        EntryDate = DateTime.Now,
                    };

                    string serializedTicket = JsonConvert.SerializeObject(ticket);

                    var vehicle = new Vehicle
                    {
                        Spot = spot + 1,
                        IsParked = true,
                        LocationId = location.Id,
                        TicketNumber = ticketNumber,
                        VehicleNumber = request.VehicleNumber,
                        VehicleType = request.VehicleType,
                        ParkingTicket = serializedTicket
                    };

                    location.BusesAndTrucksAvailableSpots -= 1;
                    location.TotalAvailableSpots -= 1;
                    location.LastIssuedTicket = DateTime.Now;
                    location.DailyCount += 1;

                    _unitOfWork.LocationRepository.Update(location);

                    await _unitOfWork.VehicleRepository.AddAsync(vehicle);
                    await _unitOfWork.SaveAsync();

                    return ApiResponse<ParkingTicket>.Success($"Spot booked for {request.VehicleType}", ticket);
                }
                else if (request.VehicleType.ToLowerInvariant() == "motorcycle" || request.VehicleType.ToLowerInvariant() == "scooter")
                {
                    if (location.MotorcyclesAndScootersAvailableSpots < 1)
                    {
                        return ApiResponse<ParkingTicket>.Fail($"All Spot a filled up for this vehicle type, try a different location", (int)HttpStatusCode.BadRequest);
                    }

                    var spot = MaxSpots.MotorcyclesAndScooters - location.MotorcyclesAndScootersAvailableSpots;
                    var ticketNumber = (location.DailyCount + 1).ToString("D3");

                    ParkingTicket ticket = new ParkingTicket
                    {
                        TicketNumber = ticketNumber,
                        SpotNumber = spot + 1,
                        EntryDate = DateTime.Now,
                    };

                    string serializedTicket = JsonConvert.SerializeObject(ticket);

                    var vehicle = new Vehicle
                    {
                        Spot = spot + 1,
                        IsParked = true,
                        LocationId = location.Id,
                        TicketNumber = ticketNumber,
                        VehicleNumber = request.VehicleNumber,
                        VehicleType = request.VehicleType,
                        ParkingTicket = serializedTicket
                    };

                    location.MotorcyclesAndScootersAvailableSpots -= 1;
                    location.TotalAvailableSpots -= 1;
                    location.LastIssuedTicket = DateTime.Now;
                    location.DailyCount += 1;

                    _unitOfWork.LocationRepository.Update(location);

                    await _unitOfWork.VehicleRepository.AddAsync(vehicle);
                    await _unitOfWork.SaveAsync();

                    return ApiResponse<ParkingTicket>.Success($"Spot booked for {request.VehicleType}", ticket);
                }

                return ApiResponse<ParkingTicket>.Fail($"Inavlid Vehicle Type {request.VehicleType}", (int)HttpStatusCode.BadRequest);
            }
            catch(Exception ex)
            {
                return ApiResponse<ParkingTicket>.Fail($"{ex.Message}", (int)HttpStatusCode.InternalServerError);
            }
           
        }

        private async Task<ApiResponse<string>> resetDailyCount(Location location)
        {
            try
            {
                var curentdate = DateTime.Now.ToString("d");
                var locationLastBooked = location.LastIssuedTicket.ToString("d");

                if (curentdate != locationLastBooked)
                {
                    location.DailyCount = 0;

                    _unitOfWork.LocationRepository.Update(location);

                    return ApiResponse<string>.Fail($"success", (int)HttpStatusCode.OK);
                }
                return ApiResponse<string>.Fail($"could not reset count", (int)HttpStatusCode.BadRequest);
            }
            catch(Exception ex)
            {
                return ApiResponse<string>.Fail($"{ex.Message}", (int)HttpStatusCode.InternalServerError);
            }
            
        }
    }
}

using Newtonsoft.Json;
using ParkingLot.core.DTOs.Request;
using ParkingLot.core.DTOs.Response;
using ParkingLot.core.Interfaces;
using ParkingLot.Domain.Enums;
using ParkingLot.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLot.core.Services
{
    public class ParkingService : IParkingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IParkingServiceHelper _parkingServiceHelper;
        private readonly ITicketService _ticketService;
        private readonly ICalculateFee _calculateFee;

        public ParkingService(IUnitOfWork unitOfWork, IParkingServiceHelper parkingServiceHelper, ITicketService ticketService, ICalculateFee calculateFee)
        {
            _unitOfWork = unitOfWork;
            _parkingServiceHelper = parkingServiceHelper;
            _ticketService = ticketService;
            _calculateFee = calculateFee;
        }

        public async Task<ApiResponse<ParkingTicket>> ParkVehicle(ParkingRequestDTO request)
        {
            try
            {
                var location = await _unitOfWork.LocationRepository.GetLocationByName(request.LocationName);
                if (location == null)
                {
                    return ApiResponse<ParkingTicket>.Fail("Inavalid Location Input", (int)HttpStatusCode.BadRequest);
                }

                VehiclesEnum vehicleType;
                if (Enum.TryParse(request.VehicleType.ToLowerInvariant(), out vehicleType))
                {
                    if (!await _parkingServiceHelper.CanPark(request.LocationName, request.VehicleType))
                    {
                        return ApiResponse<ParkingTicket>.Fail($"No spot is available to park a/an {request.VehicleType}. Try the Mall", (int)HttpStatusCode.BadRequest);
                    }

                    return await _ticketService.BookTicket(location, request);

                }

                return ApiResponse<ParkingTicket>.Fail("Vehicle Type not allowed", (int)HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                return ApiResponse<ParkingTicket>.Fail($"{ex.Message}", (int)HttpStatusCode.InternalServerError);
            }         
        }

        public async Task<ApiResponse<ParkingReciept>> UnParkVehicle(UnParkRequestDTO request)
        {
            try
            {
                var location = await _unitOfWork.LocationRepository.GetLocationByName(request.location);
                var vehicle = await _unitOfWork.VehicleRepository.GetVehicleByTicketNumber(request.TicketNUmber);


                if (string.IsNullOrEmpty(request.VehicleNumber))
                    return ApiResponse<ParkingReciept>.Fail("Vehicle number can not be empty");

                if(string.IsNullOrEmpty(request.TicketNUmber))
                    return ApiResponse<ParkingReciept>.Fail("Ticket number can not be empty");

                if (string.IsNullOrEmpty(request.location))
                    return ApiResponse<ParkingReciept>.Fail("location can not be empty");

                if(location == null)
                    return ApiResponse<ParkingReciept>.Fail("Invalid Location", (int)HttpStatusCode.BadRequest);

                if (vehicle == null)
                    return ApiResponse<ParkingReciept>.Fail($"{request.TicketNUmber} is not valid", (int)HttpStatusCode.BadRequest);

                if (vehicle.LocationId != location.Id)
                    return ApiResponse<ParkingReciept>.Fail($"Invalid location, Try other parking Lot", (int)HttpStatusCode.BadRequest);

                if (vehicle.VehicleNumber != request.VehicleNumber)
                    return ApiResponse<ParkingReciept>.Fail($"Invalid Vehicle Number {request.VehicleNumber}", (int)HttpStatusCode.BadRequest);

                if (vehicle.IsParked == false)
                    return ApiResponse<ParkingReciept>.Fail($"Vehicle with number {request.VehicleNumber} is not parked", (int)HttpStatusCode.BadRequest);

                var result = JsonConvert.DeserializeObject<ParkingTicket>(vehicle.ParkingTicket);

                if(result == null)
                    return ApiResponse<ParkingReciept>.Fail("Invalid parking Ticket", (int)HttpStatusCode.BadRequest);

                var fee =  _calculateFee.CalculateFees(request.location, result, vehicle.VehicleType);

                ParkingReciept reciept = new ParkingReciept
                {
                    ReceiptNumber = $"R-{vehicle.TicketNumber}",
                    EntryDate = result.EntryDate,
                    ExitDate = DateTime.Now,
                    Fees = fee.Data
                };

                var parkingReciept = JsonConvert.SerializeObject(reciept);

                if(vehicle.VehicleType == "suv" || vehicle.VehicleType == "car")
                    location.SuvsAndCarsAvailableSpots += 1;
                else if(vehicle.VehicleType == "truck" || vehicle.VehicleType == "bus")
                    location.BusesAndTrucksAvailableSpots += 1;

                else if(vehicle.VehicleType == "motorcycle" || vehicle.VehicleType == "scooter")
                    location.MotorcyclesAndScootersAvailableSpots += 1;

                location.LastIssuedTicket = DateTime.Now;
                vehicle.IsParked = false;
                vehicle.ParkingReciept = parkingReciept;

                _unitOfWork.VehicleRepository.Update(vehicle);
                _unitOfWork.LocationRepository.Update(location);
                await _unitOfWork.SaveAsync();

                return ApiResponse<ParkingReciept>.Success("Reciept successfully generated", reciept);

            }
            catch(Exception ex)
            {
                return ApiResponse<ParkingReciept>.Fail($"{ex.Message}", (int)HttpStatusCode.InternalServerError);
            }
        }
    }
}

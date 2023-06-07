using AutoFixture;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;
using ParkingLot.core.DTOs.Request;
using ParkingLot.core.DTOs.Response;
using ParkingLot.core.Interfaces;
using ParkingLot.core.Services;
using ParkingLot.Domain.Enums;
using ParkingLot.Domain.Models;

namespace TestProject1.serviceTest
{
    public class ParkingLotServiceTest
    {
        private readonly IFixture _fixture;
        private readonly Mock<ITicketService> _ticketService;
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly Mock<IParkingServiceHelper> _parkingServiceHelper;
        private readonly Mock<ICalculateFee> _calculateFee;
        private readonly Mock<Enum> _enum;
        private readonly ParkingService _sut;

        public ParkingLotServiceTest()
        {
            _fixture = new Fixture();
            _parkingServiceHelper = _fixture.Freeze<Mock<IParkingServiceHelper>>();
            _ticketService = _fixture.Freeze<Mock<ITicketService>>();
            _calculateFee = new Mock<ICalculateFee>();
            //_calculateFee = _fixture.Freeze<Mock<ICalculateFee>>();
            _unitOfWork = new Mock<IUnitOfWork>();
            //_unitOfWork = _fixture.Freeze<Mock<IUnitOfWork>>();
            _enum = new Mock<Enum>();


            _sut = new ParkingService(_unitOfWork.Object, _parkingServiceHelper.Object, _ticketService.Object, _calculateFee.Object);
        }

        [Fact]
        public async Task ParkVehicle_should_return_true()
        {
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            //Arrange
            var request = new ParkingRequestDTO
            {
                LocationName = "mall",
                VehicleNumber = "sjis",
                VehicleType = "truck"
            };
            var location = _fixture.Create<Location>();
            var response = _fixture.Create<ApiResponse<ParkingTicket>>();           
            var expectedVehicle  = _fixture.Create<VehiclesEnum>();
          


            var boolean = true;

            _unitOfWork.Setup(x => x.LocationRepository.GetLocationByName(request.LocationName)).ReturnsAsync(location);
            _parkingServiceHelper.Setup(x => x.CanPark(request.LocationName, request.VehicleType)).ReturnsAsync(boolean);
            _ticketService.Setup(x => x.BookTicket(location, request)).ReturnsAsync(response);

            //_enum.Setup(x => x.TryParse<>());

            //Act
            var result = await _sut.ParkVehicle(request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Status.Should().BeTrue();

        }

        [Fact]
        public async Task ParkVehicle_should_return_false_when_placed_in_wrong_location()
        {
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            //Arrange
            var request = new ParkingRequestDTO
            {
                LocationName = "airport",
                VehicleNumber = "sjis",
                VehicleType = "truck"
            };
            var location = _fixture.Create<Location>();
            var response = _fixture.Create<ApiResponse<ParkingTicket>>();
            var expectedVehicle = _fixture.Create<VehiclesEnum>();



            var boolean = false;

            _unitOfWork.Setup(x => x.LocationRepository.GetLocationByName(request.LocationName)).ReturnsAsync(location);
            _parkingServiceHelper.Setup(x => x.CanPark(request.LocationName, request.VehicleType)).ReturnsAsync(boolean);
            _ticketService.Setup(x => x.BookTicket(location, request)).ReturnsAsync(response);

            //Act
            var result = await _sut.ParkVehicle(request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Status.Should().BeFalse();

        }
        [Fact]
        public async Task ParkVehicle_should_return_false_when_vehicle_is_invalid()
        {
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            //Arrange
            var request = new ParkingRequestDTO
            {
                LocationName = "airport",
                VehicleNumber = "sjis",
                VehicleType = "plane"
            };
            var location = _fixture.Create<Location>();
            var response = _fixture.Create<ApiResponse<ParkingTicket>>();
            var expectedVehicle = _fixture.Create<VehiclesEnum>();



            var boolean = true;

            _unitOfWork.Setup(x => x.LocationRepository.GetLocationByName(request.LocationName)).ReturnsAsync(location);
            _parkingServiceHelper.Setup(x => x.CanPark(request.LocationName, request.VehicleType)).ReturnsAsync(boolean);
            _ticketService.Setup(x => x.BookTicket(location, request)).ReturnsAsync(response);

            //Act
            var result = await _sut.ParkVehicle(request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Status.Should().BeFalse();

        }

        [Fact]
        public async Task UnParkVehicle_should_return_false_invalid_location()
        {
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            //Arrange
            var request = _fixture.Create<UnParkRequestDTO>();
            var location = _fixture.Create<Location>();
            var response = _fixture.Create<ApiResponse<ParkingTicket>>();
            var feeResponse = _fixture.Create<ApiResponse<decimal>>();
          
            var vehicle = _fixture.Create<Vehicle>();
            var parkingTicketResult = _fixture.Create<ParkingTicket>();



            var boolean = true;

            _unitOfWork.Setup(x => x.LocationRepository.GetLocationByName(request.location)).ReturnsAsync(location);
            _unitOfWork.Setup(x => x.VehicleRepository.GetVehicleByTicketNumber(request.TicketNUmber)).ReturnsAsync(vehicle);
            _calculateFee.Setup(x => x.CalculateFees(request.location, parkingTicketResult, vehicle.VehicleType)).Returns(feeResponse);
           


            //Act
            var result = await _sut.UnParkVehicle(request).ConfigureAwait(false);

            //Assert
            result.Should().NotBeNull();
            result.Status.Should().BeFalse();

        }
    }
}

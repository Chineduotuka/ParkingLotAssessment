using ParkingLot.core.DTOs.Response;
using ParkingLot.core.Interfaces;
using System.Net;

namespace ParkingLot.core.Utilities
{
    public class CalculateFee : ICalculateFee
    {
        public  ApiResponse<decimal> CalculateFees(string location, ParkingTicket ticket, string carType)
        {
            var parkingTime = DateTime.Now;
            var TimeDifference = (parkingTime - ticket.EntryDate).TotalHours;

            if (location.ToLowerInvariant() == "mall")
            {
                var fee = MallFeeCalculation(TimeDifference, carType);
                if (fee >= 0)
                {
                    return ApiResponse<decimal>.Success("fee calculated", fee);
                }

                return ApiResponse<decimal>.Fail("something went wrong", (int)HttpStatusCode.BadRequest);
            }
            else if (location.ToLowerInvariant() == "airport")
            {
                var fee = AirportFeeCalculation(TimeDifference, carType);

                if (fee >= 0)
                {
                    return ApiResponse<decimal>.Success("fee calculated", fee);
                }
                return ApiResponse<decimal>.Fail("something went wrong", (int)HttpStatusCode.BadRequest);
            }
            else
            {
                var fee = StadiumFeeCalculation(TimeDifference, carType);
                if (fee >= 0)
                {
                    return ApiResponse<decimal>.Success("fee calculated", fee);
                }
                return ApiResponse<decimal>.Fail("something went wrong", (int)HttpStatusCode.BadRequest);
            }

        }

        private decimal MallFeeCalculation(double time, string carType)
        {
            var multiplier = (decimal)Math.Ceiling(time);
            if (carType.ToLowerInvariant() == "suv" || carType.ToLowerInvariant() == "car")
            {
                var fee = 20;

                return fee * multiplier;

            }
            else if (carType.ToLowerInvariant() == "truck" || carType.ToLowerInvariant() == "bus")
            {
                var fee = 50;


                return fee * multiplier;
            }
            else
            {
                var fee = 10;


                return fee * multiplier;
            }

        }

        private decimal StadiumFeeCalculation(double time, string carType)
        {
            var multiplier = (decimal)Math.Ceiling(time);
            if (carType.ToLowerInvariant() == "motorcycle" || carType.ToLowerInvariant() == "scooter")
            {
                if (multiplier <= 4)
                {
                    return 30;
                }
                else if (multiplier <= 12)
                {
                    return 30 + 60;
                }
                else if (multiplier > 12)
                {
                    var perHrMultiplier = (multiplier - 12) * 100;

                    return 30 + 60 + perHrMultiplier;
                }

            }
            else if (carType.ToLowerInvariant() == "suv" || carType.ToLowerInvariant() == "scooter")
            {
                if (multiplier <= 4)
                {
                    return 60;
                }
                else if (multiplier <= 12)
                {
                    return 60 + 120;
                }
                else if (multiplier > 12)
                {
                    var perHrMultiplier = (multiplier - 12) * 200;

                    return 60 + 120 + perHrMultiplier;
                }
            }

            return -1;
        }

        private decimal AirportFeeCalculation(double time, string carType)
        {
            var multiplier = (decimal)Math.Ceiling(time);
            if (carType.ToLowerInvariant() == "suv" || carType.ToLowerInvariant() == "car")
            {
                if (multiplier <= 1)
                {
                    return 0;
                }
                else if (multiplier <= 8)
                {
                    return 40;
                }
                else if (multiplier <= 24)
                {
                    return 60;
                }
                else if (multiplier > 24)
                {
                    var result = Math.Ceiling(multiplier / 24);
                    return result * 80;
                }
            }
            else if (carType.ToLowerInvariant() == "motorcycle" || carType.ToLowerInvariant() == "scooter")
            {
                if (multiplier <= 12)
                {
                    return 60;
                }
                else if (multiplier <= 24)
                {
                    return 80;
                }
                else if (multiplier > 24)
                {
                    var result = Math.Ceiling(multiplier / 24);
                    return result * 100;
                }
            }

            return -1;
        }

    }
}

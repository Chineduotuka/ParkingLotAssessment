using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ParkingLot.core.DTOs.Response
{
    public class ApiResponse<T>
    {
        public int StatusCode { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; } = null!;
        public T Data { get; set; }
        public static ApiResponse<T> Fail(string errorMessage, int statusCode = (int)HttpStatusCode.NotFound)
        {
            return new ApiResponse<T> { Status = false, Message = errorMessage, StatusCode = statusCode };
        }
        public static ApiResponse<T> Success(string successMessage, T data, int statusCode = (int)HttpStatusCode.OK)
        {
            return new ApiResponse<T> { Status = true, Message = successMessage, Data = data, StatusCode = statusCode };
        }
        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}

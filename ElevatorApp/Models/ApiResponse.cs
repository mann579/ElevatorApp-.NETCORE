using System;
using System.Text.Json.Serialization;

namespace ElevatorApp.Models
{
    public class ApiResponse<T>
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ResponseCodeType ResponseCode { get; set; }

        public string Message { get; set; }

        public T ResponseObject { get; set; }

    }

    public enum ResponseCodeType
    {
        Success,
        Error
    }
}

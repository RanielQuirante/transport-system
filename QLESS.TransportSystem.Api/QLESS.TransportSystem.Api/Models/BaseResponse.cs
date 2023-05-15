using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace QLESS.TransportSystem.Api.Models
{
    public class BaseResponse<T>
    {
        public BaseResponse() { }

        public BaseResponse(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public BaseResponse(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public BaseResponse(bool isSuccess, string message, T data)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }

        [DataMember(Name = "isSuccess")]
        public bool IsSuccess { get; set; }

        [DataMember(Name = "message")]
        public string Message { get; set; }

        [DataMember(Name = "Rows")]
        public int? Rows { get; set; }

        [DataMember(Name = "data")]
        public T Data { get; set; }
    }
}

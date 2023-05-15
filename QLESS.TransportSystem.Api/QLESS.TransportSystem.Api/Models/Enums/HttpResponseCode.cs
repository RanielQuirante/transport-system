using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace QLESS.TransportSystem.Models.Enums
{
    public enum HttpResponseCode
    {
        [Description("OK")]
        OK = 200,
        [Description("Created")]
        Created = 201,
        [Description("Accepted")]
        Accepted = 202,
        [Description("No Content")]
        NoContent = 204,
        [Description("Bad Request")]
        BadRequest = 400,
        [Description("Unauthorized")]
        Unauthorized = 401,
        [Description("Forbidden")]
        Forbidden = 403,
        [Description("Not Found")]
        NotFound = 404,
        [Description("Method Not Allowed")]
        MethodNotAllowed = 405,
        [Description("Not Acceptable")]
        NotAcceptable = 406,
        [Description("Proxy Authentication Required")]
        ProxyAuthenticationRequired = 407,
        [Description("Request Timeout")]
        RequestTimeout = 408,
        [Description("Internal Server Error")]
        InternalServerError = 500,
        [Description("Bad Gateway")]
        BadGateway = 502,
        [Description("Service Unavailable")]
        ServiceUnavailable = 503,
    }
    public static class EnumExtension
    {
        public static string GetDescription(this HttpResponseCode val)
        {
            DescriptionAttribute[] attributes = (DescriptionAttribute[])val
               .GetType()
               .GetField(val.ToString())
               .GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : string.Empty;
        }
    }
}

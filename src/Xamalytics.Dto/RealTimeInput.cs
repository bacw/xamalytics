using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;

namespace Xamalytics.Dto
{
    public class RealTimeInput
    {
        public IFormFile? file { get; set; }
        public string? metadata { get; set; }
    }
}



using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace PersonalProjects.Function
{
    public class TimeTrackingFunction
    {
        private readonly ILogger<TimeTrackingFunction> _logger;

        public TimeTrackingFunction(ILogger<TimeTrackingFunction> log)
        {
            _logger = log;
        }


    }
}


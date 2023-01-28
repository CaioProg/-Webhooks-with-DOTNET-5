using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirlineWeb.Data;
using Microsoft.AspNetCore.Mvc;
using AirlineWeb.Models;
using AirlineWeb.Dtos;

namespace AirlineWeb.Controllers
{   
    [Route("api/[countroller]")]
    [ApiController]
    public class WebhookSubscriptionController : ControllerBase
    {
        private readonly AirlineDbContext _context;

        public WebhookSubscriptionController(AirlineDbContext context)
        {
            _context = context;
        }

        
        public ActionResult<WebhookSubscriptionReadDto> CreateSubscription(WebhookSubscriptionCreateDto webhookSubscription)
        {

        }
        
    }
}
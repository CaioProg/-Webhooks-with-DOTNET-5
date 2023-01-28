using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AirlineWeb.Data;
using Microsoft.AspNetCore.Mvc;
using AirlineWeb.Models;
using AirlineWeb.Dtos;
using AutoMapper;

namespace AirlineWeb.Controllers
{   
    [Route("api/[countroller]")]
    [ApiController]
    public class WebhookSubscriptionController : ControllerBase
    {
        private readonly AirlineDbContext _context;
        private readonly IMapper _mapper;

        public WebhookSubscriptionController(AirlineDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public ActionResult<WebhookSubscriptionReadDto> CreateSubscription(WebhookSubscriptionCreateDto webhookSubscriptionCreateDto)
        {
            var subscription = _context.WebhookSubscriptions.FirstOrDefault(s => s.WebhookURI == webhookSubscriptionCreateDto.WebhookURI);

            if(subscription == null)
            {
                subscription = _mapper.Map<WebhookSubscription>(webhookSubscriptionCreateDto);

                subscription.Secret = Guid.NewGuid().ToString();
                subscription.WebhookPublisher = "PanAus";
                try 
                {
                    _context.WebhookSubscriptions.Add(subscription);
                    _context.SaveChanges();
                }
                catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }

                var webhookSubscriptionReadDto = _mapper.Map<WebhookSubscriptionReadDto>(subscription);
            }
            else
            {
                return NoContent();
            }
        }
        
    }
}
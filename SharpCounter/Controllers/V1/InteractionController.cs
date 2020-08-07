using Application.Commands;
using Application.Mapper;
using Application.Repository;
using Application.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace SharpCounter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InteractionController : ControllerBase
    {
        private readonly ILogger<InteractionController> _Logger;
        private readonly IMediator _Mediator;

        public InteractionController(ILogger<InteractionController> logger, IMediator mediator)
        {
            _Logger = logger;
            _Mediator = mediator;
        }


        [HttpGet("Count")]
        public async Task<ActionResult> CountInteraction(string p, string r, string t, string s, string b, string session, string key)
        {
            CreateInteractionCommand Command = CustomMapper.MapToCreateInteractionCommand(Request,p, t, s, b, session, key);

            CreateInteractionResponse response = await _Mediator.Send(Command);

            return File(response.Image, response.ContentType);

        }
       
    }
}

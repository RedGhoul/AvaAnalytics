using Application.DTO;
using Application.Queries;
using Application.Repository;
using Application.Response;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SharpCounter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class StatsController : ControllerBase
    {
        private readonly ILogger<StatsController> _Logger;
        private readonly IMediator _Mediator;
        private readonly IMapper _Mapper;
        public StatsController(ILogger<StatsController> logger, IMediator mediator, IMapper mapper)
        {
            _Logger = logger;
            _Mediator = mediator;
            _Mapper = mapper;
        }

        // GET: api/Stats/BrowserStats/5
        [HttpPost("BrowserStats/{id}")]
        public async Task<List<BrowserStatsDTO>> GetBrowserStats(int id, DateRangeDTO dateRange)
        {
            GetBrowserStatsQuery query = _Mapper.Map(dateRange, new GetBrowserStatsQuery(id));
            GetBrowserStatsResponse response = await _Mediator.Send(query);
            return response.Data;
        }

        // GET: api/InteractionStats/5
        [HttpPost("InteractionStats/{id}")]
        public async Task<List<InteractionByPathCountsDTO>> GetInteractionStats(int id, DateRangeDTO dateRange)
        {
            GetInteractionByPathCountsStatsQuery query = _Mapper.Map(dateRange, new GetInteractionByPathCountsStatsQuery(id));
            GetInteractionByPathCountsStatsResponse response = await _Mediator.Send(query);
            return response.Data;
        }

        // GET: api/Stats/SystemStats/5
        [HttpPost("SystemStats/{id}")]
        public async Task<List<SystemStatsDTO>> GetSystemStats(int id, DateRangeDTO dateRange)
        {
            GetSystemStatsQuery query = _Mapper.Map(dateRange, new GetSystemStatsQuery(id));
            GetSystemStatsResponse response = await _Mediator.Send(query);
            return response.Data;
        }

        // GET: api/Stats/ScreenSizeStats/5
        [HttpPost("ScreenSizeStats/{id}")]
        public async Task<List<ScreenSizeStatsDTO>> GetScreenSizeStats(int id, DateRangeDTO dateRange)
        {
            GetScreenSizeStatsQuery query = _Mapper.Map(dateRange, new GetScreenSizeStatsQuery(id));
            GetScreenSizeStatsResponse response = await _Mediator.Send(query);
            return response.Data;
        }

        [HttpPost("LocationStats/{id}")]
        public async Task<List<LocationStatsDTO>> GetLocationStats(int id, DateRangeDTO dateRange)
        {
            GetLocationStatsQuery query = _Mapper.Map(dateRange, new GetLocationStatsQuery(id));
            GetLocationStatsResponse response = await _Mediator.Send(query);
            return response.Data;
        }

        [HttpPost("PageViewCountStats/{id}")]
        public async Task<List<PageViewStatsDTO>> GetPageViewCountStats(int id, DateRangeDTO dateRange)
        {
            GetPageViewStatsQuery query = _Mapper.Map(dateRange, new GetPageViewStatsQuery(id));
            GetPageViewStatsResponse response = await _Mediator.Send(query);
            return response.Data;
        }
    }
}

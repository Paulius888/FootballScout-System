using AutoMapper;
using FootballScout.Data.Dtos.Leagues;
using FootballScout.Data.Entities;
using FootballScout.Data.Repositories.Leagues;
using FootballScout.Filter;
using FootballScout.Helpers;
using FootballScout.Services;
using FootballScout.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace FootballScout.Controllers
{
    [ApiController]
    [Route("api/leagues")]
    public class LeaguesController : ControllerBase
    {
        private readonly ILeaguesRepository _leaguesRepository;
        private readonly IMapper _mapper;
        private readonly IUriService uriService;

        public LeaguesController(ILeaguesRepository leaguesRepository, IMapper mapper, IUriService uriService)
        {
            _leaguesRepository = leaguesRepository;
            _mapper = mapper;
            this.uriService = uriService;
        }

        [HttpGet]
        public async Task<ActionResult<LeagueDto>> GetAll([FromQuery] PaginationFilter filter, [FromQuery] string? query = null)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var league = await _leaguesRepository.GetAll(filter, query);
            var leagueResponse = _mapper.Map<IEnumerable<LeagueDto>>(league);
            var totalRecords = await _leaguesRepository.TotalCount(query);
            var pagedResponse = PaginationHelper.CreatePagedReponse<LeagueDto>(leagueResponse, validFilter, totalRecords, uriService, route);
            return Ok(pagedResponse);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LeagueDto>> Get(int id)
        {
            var league = await _leaguesRepository.Get(id);
            if (league == null) return NotFound($"League'{id}' not found");

            return Ok(new Response<LeagueDto>(_mapper.Map<LeagueDto>(league)));
        }

        [HttpPost]
        public async Task<ActionResult<LeagueDto>> Post(CreateLeagueDto leagueDto)
        {
            var league = _mapper.Map<League>(leagueDto);

            await _leaguesRepository.Add(league);

            return Created($"/api/league/{league.Id}", new Response<LeagueDto>(_mapper.Map<LeagueDto>(league)));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<LeagueDto>> Put(int id, UpdateLeagueDto leagueDto)
        {
            var league = await _leaguesRepository.Get(id);
            if (league == null) return NotFound($"League with id '{id}' not found");

            _mapper.Map(leagueDto, league);

            await _leaguesRepository.Update(league);

            return Ok(new Response<LeagueDto>(_mapper.Map<LeagueDto>(league)));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<LeagueDto>> Delete(int id)
        {
            var league = await _leaguesRepository.Get(id);
            if (league == null) return NotFound($"League with id '{id}' not found");

            await _leaguesRepository.Remove(league);

            //204
            return NoContent();
        }
    }
}

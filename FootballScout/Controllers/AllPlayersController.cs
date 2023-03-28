using AutoMapper;
using FootballScout.Data.Dtos.Players;
using FootballScout.Data.Dtos.Teams;
using FootballScout.Data.Entities;
using FootballScout.Data.Repositories.Leagues;
using FootballScout.Data.Repositories.Players;
using FootballScout.Data.Repositories.Teams;
using FootballScout.Filter;
using FootballScout.Helpers;
using FootballScout.Services;
using FootballScout.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace FootballScout.Controllers
{
    [ApiController]
    public class AllPlayersController : ControllerBase
    {
        private readonly IPlayersRepository _playersRepository;
        private readonly ITeamsRepository _teamsRepository;
        private readonly ILeaguesRepository _leaguesRepository;
        private readonly IMapper _mapper;
        private readonly IUriService uriService;

        public AllPlayersController(IPlayersRepository playersRepository, ITeamsRepository teamsRepository, IMapper mapper, ILeaguesRepository leaguesRepository, IUriService uriService)
        {
            _teamsRepository = teamsRepository;
            _mapper = mapper;
            _leaguesRepository = leaguesRepository;
            _playersRepository = playersRepository;
            this.uriService = uriService;
        }

        [HttpGet]
        [Route("api/players")]
        public async Task<ActionResult<AllPlayersDto>> GetAll([FromQuery] PaginationFilter filter, [FromQuery] string? query = null)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var player = await _playersRepository.GetAllPlayers(filter, query);
            var playerResponse = _mapper.Map<List<AllPlayersDto>>(player);
            var totalRecords = await _playersRepository.TotalCount(query);
            var pagedResponse = PaginationHelper.CreatePagedReponse<AllPlayersDto>(playerResponse, validFilter, totalRecords, uriService, route);
            return Ok(pagedResponse);
        }

        [HttpGet("api/players/{playerId}")]
        public async Task<ActionResult<PlayerDto>> Get(int playerId)
        {
            var player = await _playersRepository.Get(playerId);

            if (player == null) return NotFound();

            return Ok(new Response<PlayerDto>(_mapper.Map<PlayerDto>(player)));
        }

        [HttpGet]
        [Route("api/players/field")]
        public async Task<ActionResult<AllPlayersDto>> GetAllFieldPlayers([FromQuery] PaginationFilter filter, [FromQuery] string? query = null)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var player = await _playersRepository.GetAllFieldPlayers(filter, query);
            var teamResponse = _mapper.Map<IList<PlayerDto>>(player);
            var totalRecords = await _playersRepository.TotalCount(query);
            var pagedResponse = PaginationHelper.CreatePagedReponse<PlayerDto>(teamResponse, validFilter, totalRecords, uriService, route);
            return Ok(pagedResponse);
        }

        [HttpGet]
        [Route("api/players/goal")]
        public async Task<ActionResult<AllPlayersDto>> GetAllGoalKeepingPlayers([FromQuery] PaginationFilter filter, [FromQuery] string? query = null)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var player = await _playersRepository.GetAllGoalKeepingPlayers(filter, query);
            var teamResponse = _mapper.Map<IList<PlayerDto>>(player);
            var totalRecords = await _playersRepository.TotalCount(query);
            var pagedResponse = PaginationHelper.CreatePagedReponse<PlayerDto>(teamResponse, validFilter, totalRecords, uriService, route);
            return Ok(pagedResponse);
        }
    }
}

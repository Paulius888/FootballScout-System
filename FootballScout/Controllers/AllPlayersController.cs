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
using Microsoft.AspNetCore.Mvc;

namespace FootballScout.Controllers
{
    [ApiController]
    [Route("api/players")]
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
    }
}

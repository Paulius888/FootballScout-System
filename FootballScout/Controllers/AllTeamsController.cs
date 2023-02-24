using AutoMapper;
using FootballScout.Data.Dtos.Leagues;
using FootballScout.Data.Dtos.Teams;
using FootballScout.Data.Entities;
using FootballScout.Data.Repositories.Leagues;
using FootballScout.Data.Repositories.Teams;
using FootballScout.Filter;
using FootballScout.Helpers;
using FootballScout.Services;
using Microsoft.AspNetCore.Mvc;

namespace FootballScout.Controllers
{
    [ApiController]
    [Route("api/teams")]
    public class AllTeamsController : ControllerBase
    {
        private readonly ITeamsRepository _teamsRepository;
        private readonly ILeaguesRepository _leaguesRepository;
        private readonly IMapper _mapper;
        private readonly IUriService uriService;

        public AllTeamsController(ITeamsRepository teamsRepository, IMapper mapper, ILeaguesRepository leaguesRepository, IUriService uriService)
        {
            _teamsRepository = teamsRepository;
            _mapper = mapper;
            _leaguesRepository = leaguesRepository;
            this.uriService = uriService;
        }

        [HttpGet]
        public async Task<ActionResult<AllTeamsDto>> GetAll([FromQuery] PaginationFilter filter, [FromQuery] string? query = null)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var team = await _teamsRepository.GetAllTeams(filter, query);
            var teamResponse = _mapper.Map<List<AllTeamsDto>>(team);
            var totalRecords = await _teamsRepository.TotalCount(query);
            var pagedResponse = PaginationHelper.CreatePagedReponse<AllTeamsDto>(teamResponse, validFilter, totalRecords, uriService, route);
            return Ok(pagedResponse);
        }
    }
}
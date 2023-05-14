using AutoMapper;
using FootballScout.Data.Dtos.FieldStats;
using FootballScout.Data.Dtos.GoalStats;
using FootballScout.Data.Dtos.Players;
using FootballScout.Data.Entities;
using FootballScout.Data.Repositories.GoalKeeping;
using FootballScout.Data.Repositories.Leagues;
using FootballScout.Data.Repositories.Mentals;
using FootballScout.Data.Repositories.Physicals;
using FootballScout.Data.Repositories.Players;
using FootballScout.Data.Repositories.Teams;
using FootballScout.Data.Repositories.Technicals;
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
        private readonly ITechnicalsRepository _technicalsRepository;
        private readonly IMentalsRepository _mentalsRepository;
        private readonly IPhysicalsRepository _physicalsRepository;
        private readonly IGoalKeepingRepository _goalkeepingRepository;

        public AllPlayersController(IPlayersRepository playersRepository, ITeamsRepository teamsRepository, IMapper mapper, ILeaguesRepository leaguesRepository, IUriService uriService
            , ITechnicalsRepository technicalsRepository, IMentalsRepository mentalsRepository, IPhysicalsRepository physicalsRepository, IGoalKeepingRepository goalkeepingRepository)
        {
            _teamsRepository = teamsRepository;
            _mapper = mapper;
            _leaguesRepository = leaguesRepository;
            _playersRepository = playersRepository;
            this.uriService = uriService;
            _technicalsRepository = technicalsRepository;
            _mentalsRepository = mentalsRepository;
            _physicalsRepository = physicalsRepository;
            _goalkeepingRepository = goalkeepingRepository;
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
        public async Task<ActionResult<Response<PlayerDto>>> Get(int playerId)
        {
            var player = await _playersRepository.Get(playerId);

            if (player == null) return NotFound();

            return Ok(new Response<PlayerDto>(_mapper.Map<PlayerDto>(player)));
        }

        [HttpGet]
        [Route("api/players/field")]
        public async Task<ActionResult<PlayerDto>> GetAllFieldPlayers([FromQuery] PaginationFilter filter, [FromQuery] string? query = null)
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
        public async Task<ActionResult<PlayerDto>> GetAllGoalKeepingPlayers([FromQuery] PaginationFilter filter, [FromQuery] string? query = null)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var player = await _playersRepository.GetAllGoalKeepingPlayers(filter, query);
            var teamResponse = _mapper.Map<IList<PlayerDto>>(player);
            var totalRecords = await _playersRepository.TotalCount(query);
            var pagedResponse = PaginationHelper.CreatePagedReponse<PlayerDto>(teamResponse, validFilter, totalRecords, uriService, route);
            return Ok(pagedResponse);
        }

        [HttpGet]
        [Route("api/players/{playerId}/field/stats")]
        public async Task<ActionResult<FieldStatsDto>> GetFieldPlayerStats(int playerId)
        {
            var technicals = await _technicalsRepository.GetAll(playerId);
            if (technicals == null) return NotFound($"Could not find Technical attributes of player with this id: {playerId}");

            var mentals = await _mentalsRepository.GetAll(playerId);
            if (mentals == null) return NotFound($"Could not find Mental attributes of player with this id: {playerId}");

            var physicals = await _physicalsRepository.GetAll(playerId);
            if (physicals == null) return NotFound($"Could not find Physical attributes of player with this id: {playerId}");

            FieldStatsDto fieldStats = new FieldStatsDto(0, 0, 0, 0, 0, 0, 0, 0, 0);
            var newFieldStats = _mapper.Map<FieldStats>(fieldStats);
            newFieldStats.Defending = (technicals[0].Marking + technicals[0].Tackling + mentals[0].Positioning) / 3;
            newFieldStats.Physicals = (physicals[0].Agility + physicals[0].Balance + physicals[0].Stamina + physicals[0].Strength) / 4;
            newFieldStats.Speed = (physicals[0].Pace + physicals[0].Acceleration) / 2;
            newFieldStats.Vision = (mentals[0].Vision + mentals[0].Anticipation + mentals[0].Composure) / 3;
            newFieldStats.Attacking = (mentals[0].Anticipation + mentals[0].Decisions + mentals[0].OffTheBall) / 3;
            newFieldStats.Technicals = (technicals[0].Dribbling + technicals[0].FirstTouch + technicals[0].Technique) / 3;
            newFieldStats.Aerial = (technicals[0].Heading + mentals[0].Bravery + physicals[0].JumpingReach) / 3;
            newFieldStats.Mentals = (mentals[0].Aggression + mentals[0].Composure + mentals[0].Bravery + mentals[0].Concentration) / 4;
            newFieldStats.Overall = (newFieldStats.Defending + newFieldStats.Physicals + newFieldStats.Speed + newFieldStats.Vision
                + newFieldStats.Attacking + newFieldStats.Technicals + newFieldStats.Aerial + newFieldStats.Mentals) / 8;
            return Ok(_mapper.Map<FieldStatsDto>(newFieldStats));
        }

        [HttpGet]
        [Route("api/players/{playerId}/goal/stats")]
        public async Task<ActionResult<GoalStatsDto>> GetGoalPlayerStats(int playerId)
        {
            var goalkeeping = await _goalkeepingRepository.GetAll(playerId);
            if (goalkeeping == null) return NotFound($"Could not find Goalkeeping attributes of player with this id: {playerId}");

            var mentals = await _mentalsRepository.GetAll(playerId);
            if (mentals == null) return NotFound($"Could not find Mental attributes of player with this id: {playerId}");

            var physicals = await _physicalsRepository.GetAll(playerId);
            if (physicals == null) return NotFound($"Could not find Physical attributes of player with this id: {playerId}");

            GoalStatsDto goalStats = new GoalStatsDto(0, 0, 0, 0, 0, 0, 0, 0, 0);
            var newGoalStats = _mapper.Map<GoalStats>(goalStats);
            newGoalStats.ShotStoping = (goalkeeping[0].Reflexes + mentals[0].Concentration + mentals[0].Anticipation) / 3;
            newGoalStats.Physicals = (physicals[0].JumpingReach + physicals[0].Strength + goalkeeping[0].AerialReach) / 3;
            newGoalStats.Speed = (physicals[0].Pace + physicals[0].Acceleration) / 2;
            newGoalStats.Communication = goalkeeping[0].Communication;
            newGoalStats.Eccentricity = goalkeeping[0].Eccentricity;
            newGoalStats.Distribution = (goalkeeping[0].FirstTouch + goalkeeping[0].Passing + goalkeeping[0].Throwing + goalkeeping[0].Kicking + mentals[0].Vision) / 5;
            newGoalStats.Aerial = (goalkeeping[0].AerialReach + goalkeeping[0].Handling + physicals[0].JumpingReach) / 3;
            newGoalStats.Mentals = (mentals[0].Aggression + mentals[0].Composure + mentals[0].Bravery + mentals[0].Concentration) / 4;
            newGoalStats.Overall = (newGoalStats.ShotStoping + newGoalStats.Physicals + newGoalStats.Speed + newGoalStats.Communication
                + newGoalStats.Eccentricity + newGoalStats.Distribution + newGoalStats.Aerial + newGoalStats.Mentals) / 8;
            return Ok(_mapper.Map<GoalStatsDto>(newGoalStats));
        }
    }
}

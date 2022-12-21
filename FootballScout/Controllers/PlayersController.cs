using AutoMapper;
using FootballScout.Data.Dtos.Players;
using FootballScout.Data.Entities;
using FootballScout.Data.Repositories.Leagues;
using FootballScout.Data.Repositories.Players;
using FootballScout.Data.Repositories.Teams;
using Microsoft.AspNetCore.Mvc;

namespace FootballScout.Controllers
{
    [ApiController]
    [Route("api/leagues/{leagueId}/teams/{teamId}/players")]
    public class PlayersController : ControllerBase
    {
        private readonly IPlayersRepository _playersRepository;
        private readonly ITeamsRepository _teamsRepository;
        private readonly ILeaguesRepository _leaguesRepository;
        private readonly IMapper _mapper;

        public PlayersController(IPlayersRepository playersRepository, ITeamsRepository teamsRepository, IMapper mapper, ILeaguesRepository leaguesRepository)
        {
            _teamsRepository = teamsRepository;
            _mapper = mapper;
            _leaguesRepository = leaguesRepository;
            _playersRepository = playersRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<PlayerDto>> GetAll(int teamId)
        {
            var players = await _playersRepository.GetAll(teamId);
            return players.Select(o => _mapper.Map<PlayerDto>(o));
        }

        [HttpGet("{playerId}")]
        public async Task<ActionResult<PlayerDto>> Get(int teamId, int playerId)
        {
            var player = await _playersRepository.Get(teamId, playerId);

            if (player == null) return NotFound();

            return Ok(_mapper.Map<PlayerDto>(player));
        }

        [HttpPost]
        public async Task<ActionResult<PlayerDto>> Add(int leagueId, int teamId, CreatePlayerDto playerDto)
        {
            var team = await _teamsRepository.Get(leagueId, teamId);
            if (team == null) return NotFound($"Could not find a team with this id {teamId}");

            var player = _mapper.Map<Player>(playerDto);
            player.TeamId = teamId;

            await _playersRepository.Add(player);

            return Created($"/api/leagues/{leagueId}/teams/{team.Id}/players/{player.Id}", _mapper.Map<PlayerDto>(player));
        }

        [HttpPut("{playerId}")]
        public async Task<ActionResult<PlayerDto>> Put(int leagueId, int teamId, int playerId, UpdatePlayerDto playerDto)
        {
            var team = await _teamsRepository.Get(leagueId, teamId);
            if (team == null) return NotFound($"Could not find a team with this id {teamId}");

            var oldPlayer = await _playersRepository.Get(teamId, playerId);
            if (oldPlayer == null) return NotFound();

            _mapper.Map(playerDto, oldPlayer);

            await _playersRepository.Update(oldPlayer);

            return Ok(_mapper.Map<PlayerDto>(oldPlayer));
        }

        [HttpDelete("{playerId}")]
        public async Task<ActionResult> Delete(int teamId, int playerId)
        {
            var player = await _playersRepository.Get(teamId, playerId);
            if (player == null) return NotFound();

            await _playersRepository.Remove(player);

            return NoContent();
        }
    }
}
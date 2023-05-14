using AutoMapper;
using FootballScout.Data.Dtos.Goalkeeping;
using FootballScout.Data.Entities;
using FootballScout.Data.Repositories.GoalKeeping;
using FootballScout.Data.Repositories.Players;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballScout.Controllers
{
    [ApiController]
    [Route("api/teams/{teamId}/players/{playerId}/goalkeeping")]
    public class GoalkeepingController : ControllerBase
    {
        private readonly IPlayersRepository _playersRepository;
        private readonly IGoalKeepingRepository _goalkeepingRepository;
        private readonly IMapper _mapper;


        public GoalkeepingController(IPlayersRepository playersRepository, IMapper mapper, IGoalKeepingRepository goalkeepingRepository)
        {
            _mapper = mapper;
            _playersRepository = playersRepository;
            _goalkeepingRepository = goalkeepingRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<GoalkeepingDto>> GetAll(int playerId)
        {
            var goalkeeping = await _goalkeepingRepository.GetAll(playerId);
            return goalkeeping.Select(o => _mapper.Map<GoalkeepingDto>(o));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GoalkeepingDto>> Add(int leagueId, int teamId, int playerId, CreateGoalkeepingDto goalkeepingDto)
        {
            var player = await _playersRepository.Get(playerId);
            if (player == null) return NotFound($"Could not find a player with this id {playerId}");

            var goalkeeping = _mapper.Map<Goalkeeping>(goalkeepingDto);
            goalkeeping.PlayerId = playerId;

            await _goalkeepingRepository.Add(goalkeeping);

            return Created($"/api/teams/{teamId}/players/{player.Id}/goalkeeping", _mapper.Map<GoalkeepingDto>(goalkeeping));
        }

        [HttpPut("{goalkeepingId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<GoalkeepingDto>> Put(int leagueId, int teamId, int playerId, int goalkeepingId, UpdateGoalkeepingDto goalkeepingDto)
        {
            var player = await _playersRepository.Get(playerId);
            if (player == null) return NotFound($"Could not find a player with this id {playerId}");

            var oldGoalkeeping = await _goalkeepingRepository.Get(playerId, goalkeepingId);
            if (oldGoalkeeping == null) return NotFound();

            _mapper.Map(goalkeepingDto, oldGoalkeeping);

            await _goalkeepingRepository.Update(oldGoalkeeping);

            return Ok(_mapper.Map<GoalkeepingDto>(oldGoalkeeping));
        }

        [HttpDelete("{goalkeepingId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int playerId, int goalkeepingId)
        {
            var goalkeeping = await _goalkeepingRepository.Get(playerId, goalkeepingId);
            if (goalkeeping == null) return NotFound();

            await _goalkeepingRepository.Remove(goalkeeping);

            return NoContent();
        }
    }
}
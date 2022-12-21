using AutoMapper;
using FootballScout.Data.Dtos.Mentals;
using FootballScout.Data.Dtos.Technicals;
using FootballScout.Data.Entities;
using FootballScout.Data.Repositories.Leagues;
using FootballScout.Data.Repositories.Mentals;
using FootballScout.Data.Repositories.Players;
using FootballScout.Data.Repositories.Teams;
using Microsoft.AspNetCore.Mvc;

namespace FootballScout.Controllers
{
    [ApiController]
    [Route("api/leagues/{leagueId}/teams/{teamId}/players/{playerId}/mentals")]
    public class MentalsController : ControllerBase
    {
        private readonly IPlayersRepository _playersRepository;
        private readonly IMentalsRepository _mentalsRepository;
        private readonly IMapper _mapper;


        public MentalsController(IPlayersRepository playersRepository, IMapper mapper, IMentalsRepository mentalsRepository)
        {
            _mapper = mapper;
            _playersRepository = playersRepository;
            _mentalsRepository = mentalsRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<MentalDto>> GetAll(int playerId)
        {
            var mentals = await _mentalsRepository.GetAll(playerId);
            return mentals.Select(o => _mapper.Map<MentalDto>(o));
        }

        [HttpPost]
        public async Task<ActionResult<MentalDto>> Add(int leagueId, int teamId, int playerId, CreateMentalDto mentalDto)
        {
            var player = await _playersRepository.Get(teamId, playerId);
            if (player == null) return NotFound($"Could not find a player with this id {playerId}");

            var mental = _mapper.Map<Mental>(mentalDto);
            mental.PlayerId = playerId;

            await _mentalsRepository.Add(mental);

            return Created($"/api/leagues/{leagueId}/teams/{teamId}/players/{player.Id}/mentals", _mapper.Map<MentalDto>(mental));
        }

        [HttpPut("{mentalId}")]
        public async Task<ActionResult<MentalDto>> Put(int leagueId, int teamId, int playerId, int mentalId, UpdateMentalDto mentalDto)
        {
            var player = await _playersRepository.Get(teamId, playerId);
            if (player == null) return NotFound($"Could not find a player with this id {playerId}");

            var oldMental = await _mentalsRepository.Get(playerId, mentalId);
            if (oldMental == null) return NotFound();

            _mapper.Map(mentalDto, oldMental);

            await _mentalsRepository.Update(oldMental);

            return Ok(_mapper.Map<MentalDto>(oldMental));
        }

        [HttpDelete("{mentalId}")]
        public async Task<ActionResult> Delete(int playerId, int mentalId)
        {
            var mental = await _mentalsRepository.Get(playerId, mentalId);
            if (mental == null) return NotFound();

            await _mentalsRepository.Remove(mental);

            return NoContent();
        }
    }
}
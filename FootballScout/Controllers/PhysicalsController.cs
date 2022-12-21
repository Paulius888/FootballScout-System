using AutoMapper;
using FootballScout.Data.Dtos.Physicals;
using FootballScout.Data.Entities;
using FootballScout.Data.Repositories.Physicals;
using FootballScout.Data.Repositories.Players;
using Microsoft.AspNetCore.Mvc;

namespace FootballScout.Controllers
{
    [ApiController]
    [Route("api/leagues/{leagueId}/teams/{teamId}/players/{playerId}/physicals")]
    public class PhysicalsController : ControllerBase
    {
        private readonly IPlayersRepository _playersRepository;
        private readonly IPhysicalsRepository _physicalsRepository;
        private readonly IMapper _mapper;


        public PhysicalsController(IPlayersRepository playersRepository, IMapper mapper, IPhysicalsRepository physicalsRepository)
        {
            _mapper = mapper;
            _playersRepository = playersRepository;
            _physicalsRepository = physicalsRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<PhysicalDto>> GetAll(int playerId)
        {
            var physicals = await _physicalsRepository.GetAll(playerId);
            return physicals.Select(o => _mapper.Map<PhysicalDto>(o));
        }

        [HttpPost]
        public async Task<ActionResult<PhysicalDto>> Add(int leagueId, int teamId, int playerId, CreatePhysicalDto physicalDto)
        {
            var player = await _playersRepository.Get(teamId, playerId);
            if (player == null) return NotFound($"Could not find a player with this id {playerId}");

            var physical = _mapper.Map<Physical>(physicalDto);
            physical.PlayerId = playerId;

            await _physicalsRepository.Add(physical);

            return Created($"/api/leagues/{leagueId}/teams/{teamId}/players/{player.Id}/physicals", _mapper.Map<PhysicalDto>(physical));
        }

        [HttpPut("{physicalId}")]
        public async Task<ActionResult<PhysicalDto>> Put(int leagueId, int teamId, int playerId, int physicalId, UpdatePhysicalDto physicalDto)
        {
            var player = await _playersRepository.Get(teamId, playerId);
            if (player == null) return NotFound($"Could not find a player with this id {playerId}");

            var oldPhysical = await _physicalsRepository.Get(playerId, physicalId);
            if (oldPhysical == null) return NotFound();

            _mapper.Map(physicalDto, oldPhysical);

            await _physicalsRepository.Update(oldPhysical);

            return Ok(_mapper.Map<PhysicalDto>(oldPhysical));
        }

        [HttpDelete("{physicalId}")]
        public async Task<ActionResult> Delete(int playerId, int physicalId)
        {
            var physical = await _physicalsRepository.Get(playerId, physicalId);
            if (physical == null) return NotFound();

            await _physicalsRepository.Remove(physical);

            return NoContent();
        }
    }
}
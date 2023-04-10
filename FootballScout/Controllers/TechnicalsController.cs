using AutoMapper;
using FootballScout.Data.Dtos.Technicals;
using FootballScout.Data.Entities;
using FootballScout.Data.Repositories.Leagues;
using FootballScout.Data.Repositories.Players;
using FootballScout.Data.Repositories.Teams;
using FootballScout.Data.Repositories.Technicals;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballScout.Controllers
{
    [ApiController]
    [Route("api/teams/{teamId}/players/{playerId}/technicals")]
    public class TechnicalsController : ControllerBase
    {
        private readonly IPlayersRepository _playersRepository;
        private readonly ITechnicalsRepository _technicalsRepository;
        private readonly IMapper _mapper;

        public TechnicalsController(IPlayersRepository playersRepository, ITeamsRepository teamsRepository,
            IMapper mapper, ILeaguesRepository leaguesRepository, ITechnicalsRepository technicalsRepository)
        {
            _mapper = mapper;
            _playersRepository = playersRepository;
            _technicalsRepository = technicalsRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<TechnicalDto>> GetAll(int playerId)
        {
            var technicals = await _technicalsRepository.GetAll(playerId);
            return technicals.Select(o => _mapper.Map<TechnicalDto>(o));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<TechnicalDto>> Add(int leagueId, int teamId, int playerId, CreateTechnicalDto technicalDto)
        {
            var player = await _playersRepository.Get(playerId);
            if (player == null) return NotFound($"Could not find a player with this id {playerId}");

            var technical = _mapper.Map<Technical>(technicalDto);
            technical.PlayerId = playerId;

            await _technicalsRepository.Add(technical);

            return Created($"/api/teams/{teamId}/players/{player.Id}/technicals", _mapper.Map<TechnicalDto>(technical));
        }

        [HttpPut("{technicalId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<TechnicalDto>> Put(int leagueId, int teamId, int playerId, int technicalId, UpdateTechnicalDto technicalDto)
        {
            var player = await _playersRepository.Get(playerId);
            if (player == null) return NotFound($"Could not find a player with this id {playerId}");

            var oldTechnical = await _technicalsRepository.Get(playerId, technicalId);
            if (oldTechnical == null) return NotFound();

            _mapper.Map(technicalDto, oldTechnical);

            await _technicalsRepository.Update(oldTechnical);

            return Ok(_mapper.Map<TechnicalDto>(oldTechnical));
        }

        [HttpDelete("{technicalId}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int playerId, int technicalId)
        {
            var technical = await _technicalsRepository.Get(playerId, technicalId);
            if (technical == null) return NotFound();

            await _technicalsRepository.Remove(technical);

            return NoContent();
        }
    }
}
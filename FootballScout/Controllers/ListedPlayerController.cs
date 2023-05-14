using System.Data;
using AutoMapper;
using FootballScout.Data.Dtos.ListedPlayers;
using FootballScout.Data.Dtos.Players;
using FootballScout.Data.Entities;
using FootballScout.Data.Repositories.ListedPlayers;
using FootballScout.Data.Repositories.Players;
using FootballScout.Data.Repositories.RestUsers;
using FootballScout.Data.Repositories.ShortLists;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FootballScout.Controllers
{
    [ApiController]
    [Route("api/shortlist/{userId}/{id}/listed")]
    public class ListedPlayerController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IShortListsRepository _shortListsRepository;
        private readonly IRestUsersRepository _restUsersRepository;
        private readonly IListedPlayersRepository _listedPlayersRepository;
        private readonly IPlayersRepository _playersRepository;
        int maxShortlistedPlayers = 20;

        public ListedPlayerController(IMapper mapper, IShortListsRepository shortListsRepository, IRestUsersRepository restUsersRepository,
            IListedPlayersRepository listedPlayersRepository, IPlayersRepository playersRepository)
        {
            _mapper = mapper;
            _shortListsRepository = shortListsRepository;
            _restUsersRepository = restUsersRepository;
            _listedPlayersRepository = listedPlayersRepository;
            _playersRepository = playersRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Scout")]
        public async Task<ActionResult<ListedPlayersDto>> GetAll(int id)
        {
            var playerCount = await _listedPlayersRepository.GetShortlistedPlayersCount(id);
            Player[] array = new Player[playerCount.Count()];
            var listedPlayers = await _listedPlayersRepository.GetShortlistedPlayers(id, array);
            var response = listedPlayers.Select(o => _mapper.Map<PlayerDto>(o));
            return Ok(response);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Scout")]
        public async Task<ActionResult<ListedPlayersDto>> Add(int id, CreateListedPlayersDto listedPlayerDto)
        {
            var playerCount = await _listedPlayersRepository.GetShortlistedPlayersCount(id);

            for (int i = 0; i < playerCount.Count; i++)
            {
                if (playerCount[i].PlayerId == listedPlayerDto.PlayerId)
                {
                    return BadRequest($"Such player already exists in the chosen shortlist");
                }
            }

            if (playerCount.Count() >= maxShortlistedPlayers)
            {
                return BadRequest($"20 players is the maximum inside 1 shortlist");
            }

            var shortList = await _shortListsRepository.Get(id);
            if (shortList == null) return NotFound($"Could not find a shortList with this id {id}");

            var listedPlayer = _mapper.Map<ListedPlayer>(listedPlayerDto);
            listedPlayer.ShortListId = id;


            await _listedPlayersRepository.Add(id, listedPlayer);

            return Created($"/api/shortlist/{{userId}}/{id}/listed/{listedPlayer.Id}", _mapper.Map<ListedPlayersDto>(listedPlayer));
        }

        [HttpDelete("{shortlistId}")]
        [Authorize(Roles = "Admin, Scout")]
        public async Task<ActionResult> Delete(int shortlistId)
        {
            var listedId = await _listedPlayersRepository.RetrieveId(shortlistId);
            var listedPlayer = await _listedPlayersRepository.Get(listedId);
            if (listedPlayer == null) return NotFound($"listedPlayer with id '{shortlistId}' not found");

            await _listedPlayersRepository.Remove(listedPlayer);

            //204
            return NoContent();
        }
    }
}

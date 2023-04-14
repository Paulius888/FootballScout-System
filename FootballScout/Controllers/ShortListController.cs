using System.Runtime.CompilerServices;
using AutoMapper;
using FootballScout.Data.Dtos.Leagues;
using FootballScout.Data.Dtos.ShortList;
using FootballScout.Data.Dtos.Teams;
using FootballScout.Data.Entities;
using FootballScout.Data.Repositories.RestUsers;
using FootballScout.Data.Repositories.ShortLists;
using Microsoft.AspNetCore.Mvc;

namespace FootballScout.Controllers
{
    [ApiController]
    [Route("api/shortlist/{userId}")]
    public class ShortListController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IShortListsRepository _shortListsRepository;
        private readonly IRestUsersRepository _restUsersRepository;

        public ShortListController(IMapper mapper, IShortListsRepository shortListsRepository, IRestUsersRepository restUsersRepository)
        {
            _mapper = mapper;
            _shortListsRepository = shortListsRepository;
            _restUsersRepository = restUsersRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<ShortListDto>> GetAll(string userId)
        {
            var shortList = await _shortListsRepository.GetAllUserShortlists(userId);
            return shortList.Select(o => _mapper.Map<ShortListDto>(o));
        }

        [HttpPost]
        public async Task<ActionResult<ShortListDto>> Add(string userId, CreateShortListDto shortListDto)
        {
            var user = await _restUsersRepository.Get(userId);
            if (user == null) return NotFound($"Could not find a user with this name {userId}");

            var shortList = _mapper.Map<ShortList>(shortListDto);
            shortList.UserId = user.Id;

            await _shortListsRepository.Add(shortList);

            return Created($"/api/shortlist/{shortList.Id}", _mapper.Map<ShortListDto>(shortList));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ShortListDto>> Update(int id, UpdateShortListDto shortListDto)
        {
            var shortList = await _shortListsRepository.Get(id);
            if (shortList == null) return NotFound($"ShortList with id '{id}' not found");

            _mapper.Map(shortListDto, shortList);

            await _shortListsRepository.Update(shortList);
            return Ok(_mapper.Map<ShortListDto>(shortList));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var shortList = await _shortListsRepository.Get(id);
            if (shortList == null) return NotFound($"ShortList with id '{id}' not found");

            await _shortListsRepository.Remove(shortList);

            //204
            return NoContent();
        }
    }
}

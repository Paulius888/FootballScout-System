using System.Runtime.CompilerServices;
using AutoMapper;
using FootballScout.Data.Dtos.Leagues;
using FootballScout.Data.Dtos.Players;
using FootballScout.Data.Dtos.ShortList;
using FootballScout.Data.Dtos.Teams;
using FootballScout.Data.Entities;
using FootballScout.Data.Repositories.RestUsers;
using FootballScout.Data.Repositories.ShortLists;
using FootballScout.Filter;
using FootballScout.Helpers;
using FootballScout.Services;
using FootballScout.Wrappers;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IUriService uriService;

        public ShortListController(IMapper mapper, IShortListsRepository shortListsRepository, IRestUsersRepository restUsersRepository,
            IUriService uriService)
        {
            _mapper = mapper;
            _shortListsRepository = shortListsRepository;
            _restUsersRepository = restUsersRepository;
            this.uriService = uriService;
        }

        [Authorize(Roles = "Admin, Scout")]
        [HttpGet]
        public async Task<ActionResult<ShortListDto>> GetAll(string userId, [FromQuery] PaginationFilter filter, [FromQuery] string? query = null)
        {
            var route = Request.Path.Value;
            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var shortList = await _shortListsRepository.GetAllUserShortlists(userId, filter, query);
            var shortListResponse = _mapper.Map<IList<ShortListDto>>(shortList);
            var totalRecords = await _shortListsRepository.TotalCount(userId, query);
            var pagedResponse = PaginationHelper.CreatePagedReponse<ShortListDto>(shortListResponse, validFilter, totalRecords, uriService, route);
            return Ok(pagedResponse);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Scout")]
        public async Task<ActionResult<ShortListDto>> Add(string userId, CreateShortListDto shortListDto)
        {
            var user = await _restUsersRepository.Get(userId);
            if (user == null) return NotFound($"Could not find a user with this name {userId}");

            var shortList = _mapper.Map<ShortList>(shortListDto);
            shortList.UserId = user.Id;

            await _shortListsRepository.Add(shortList);

            return Created($"/api/shortlist/{shortList.Id}", new Response<ShortListDto>(_mapper.Map<ShortListDto>(shortList)));
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin, Scout")]
        public async Task<ActionResult<ShortListDto>> Update(int id, UpdateShortListDto shortListDto)
        {
            var shortList = await _shortListsRepository.Get(id);
            if (shortList == null) return NotFound($"ShortList with id '{id}' not found");

            _mapper.Map(shortListDto, shortList);

            await _shortListsRepository.Update(shortList);
            return Ok(new Response<ShortListDto>(_mapper.Map<ShortListDto>(shortList)));
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Scout")]
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

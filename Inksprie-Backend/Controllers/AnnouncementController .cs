using Inksprie_Backend.Dtos;
using Inksprie_Backend.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inksprie_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnnouncementController : ControllerBase
    {
        private readonly IAnnouncementService _announcementService;

        public AnnouncementController(IAnnouncementService announcementService)
        {
            _announcementService = announcementService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var announcements = await _announcementService.GetAllAsync();
            return Ok(announcements);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var announcement = await _announcementService.GetByIdAsync(id);
            if (announcement == null) return NotFound();
            return Ok(announcement);
        }

        [HttpPost("{createdBy}")]
        //[Authorize]
        public async Task<IActionResult> Create(int createdBy, CreateAnnouncementDto dto)
        {
            var created = await _announcementService.CreateAsync(dto, createdBy);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}/by/{createdBy}")]
        //[Authorize]
        public async Task<IActionResult> Update(int id, int createdBy, CreateAnnouncementDto dto)
        {
            var updated = await _announcementService.UpdateAsync(id, dto, createdBy);
            if (!updated) return NotFound();
            return NoContent();
        }



        [HttpDelete("{id}")]
        //[Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _announcementService.DeleteAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}

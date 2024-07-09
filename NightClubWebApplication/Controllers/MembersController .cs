// Controllers/MembersController.cs
using Microsoft.AspNetCore.Mvc;
using NightClub.Interfaces;
using NightClub.Models;

[Route("api/[controller]")]
[ApiController]
public class MembersController : ControllerBase
{
    private readonly IMemberService _memberService;

    public MembersController(IMemberService memberService)
    {
        _memberService = memberService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateMember([FromBody] Member member)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var createdMember = await _memberService.CreateMemberAsync(member);
        return CreatedAtAction(nameof(GetMemberById), new { id = createdMember.Id }, createdMember);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateMember(int id, [FromBody] Member member)
    {
        if (id != member.Id)
            return BadRequest();

        await _memberService.UpdateMemberAsync(member);
        return NoContent();
    }

    [HttpPost("{id}/blacklist")]
    public async Task<IActionResult> BlacklistMember(int id, [FromBody] DateTime until)
    {
        await _memberService.BlacklistMemberAsync(id, until);
        return NoContent();
    }

    [HttpGet("{id}/blacklisted")]
    public async Task<IActionResult> IsMemberBlacklisted(int id)
    {
        var isBlacklisted = await _memberService.IsMemberBlacklistedAsync(id);
        return Ok(new { isBlacklisted });
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetMemberById(int id)
    {
        var member = await _memberService.GetMemberById(id);
        if (member == null)
            return NotFound();

        return Ok(member);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllMembers()
    {
        var members = await _memberService.GetAllMembersAsync();
        return Ok(members);
    }
}

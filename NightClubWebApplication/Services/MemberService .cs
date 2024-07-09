// Services/MemberService.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NightClub.DataAccess;
using NightClub.Interfaces;
using NightClub.Models;


public class MemberService : IMemberService
{
    private readonly NightClubContext _context;

    public MemberService(NightClubContext context)
    {
        _context = context;
    }

    public async Task<Member> CreateMemberAsync(Member member)
    {
        _context.Members.Add(member);
        await _context.SaveChangesAsync();
        return member;
    }

    public async Task UpdateMemberAsync(Member member)
    {
        _context.Members.Update(member);
        await _context.SaveChangesAsync();
    }

    public async Task BlacklistMemberAsync(int memberId, DateTime until)
    {
        var member = await _context.Members.FindAsync(memberId);
        if (member != null)
        {
            member.IsBlacklisted = true;
            member.BlacklistUntil = until;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> IsMemberBlacklistedAsync(int memberId)
    {
        var member = await GetMemberById(memberId);
        return member?.IsBlacklisted == true && member.BlacklistUntil > DateTime.UtcNow;
    }

    
    public async Task<Member> GetMemberById(int memberId)
    {
        return await _context.Members.FindAsync(memberId);
    }

    public async Task<List<Member>> GetAllMembersAsync()
    {
        return await _context.Members
            .Include(m => m.IdentityCard) // Inclure les cartes d'identité associées
            .Include(m => m.MembershipCards) // Inclure les cartes de membres associées
            .ToListAsync();
    }
}

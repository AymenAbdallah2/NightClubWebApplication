using NightClub.Models;

namespace NightClub.Interfaces
{
    public interface IMemberService
    {
        Task<Member> CreateMemberAsync(Member member);
        Task UpdateMemberAsync(Member member);
        Task BlacklistMemberAsync(int memberId, DateTime until);
        Task<bool> IsMemberBlacklistedAsync(int memberId);
        Task<Member> GetMemberById(int memberId);
        Task<List<Member>> GetAllMembersAsync();
    }
}
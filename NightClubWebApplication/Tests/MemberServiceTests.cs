using Microsoft.EntityFrameworkCore;
using NightClub.DataAccess;
using NightClub.Interfaces;
using NightClub.Models;
using Xunit;

namespace NightClubTests
{
    public class MemberServiceTests
    {
        private readonly IMemberService _memberService;
        private readonly NightClubContext _context;

        public MemberServiceTests()
        {
            var options = new DbContextOptionsBuilder<NightClubContext>()
                .UseInMemoryDatabase(databaseName: "NightClubTestDb")
                .Options;

            _context = new NightClubContext(options);
            _memberService = new MemberService(_context);
        }

        // Méthode d'assistance pour créer un membre
        private Member CreateTestMember(string email, string phoneNumber, string firstName, string lastName, DateTime birthDate, string nationalRegistryNumber, string cardNumber)
        {
            return new Member
            {
                Email = email,
                PhoneNumber = phoneNumber,
                IdentityCard = new IdentityCard
                {
                    FirstName = firstName,
                    LastName = lastName,
                    BirthDate = birthDate,
                    NationalRegistryNumber = nationalRegistryNumber,
                    ValidFrom = DateTime.UtcNow,
                    ValidTo = DateTime.UtcNow.AddYears(10),
                    CardNumber = cardNumber
                }
            };
        }

        [Fact]
        public async Task CreateMember_ShouldAddMember()
        {
            var member = CreateTestMember(
                "Aymen.Abdallah@example.com",
                "+3220235896",
                "Aymen",
                "Abdallah",
                DateTime.UtcNow.AddYears(-29),
                "123.45.67-890-12",
                "987654321"
            );

            var createdMember = await _memberService.CreateMemberAsync(member);

            Assert.NotNull(createdMember);
            Assert.Equal(member.Email, createdMember.Email);
        }

        [Fact]
        public async Task BlacklistMember_ShouldBlacklistMember()
        {
            var member = CreateTestMember(
                "e.fofana@example.com",
                "+3220235896",
                "Eden",
                "Fofana",
                DateTime.UtcNow.AddYears(-50),
                "125.45.67-890-12",
                "787654321"
            );

            var createdMember = await _memberService.CreateMemberAsync(member);

            var untilDate = DateTime.UtcNow.AddDays(30);
            await _memberService.BlacklistMemberAsync(memberId: createdMember.Id, until: untilDate);

            var isMemberBlacklisted = await _memberService.IsMemberBlacklistedAsync(createdMember.Id);
            Assert.True(isMemberBlacklisted);
        }

        [Fact]
        public async Task UpdateMember_ShouldUpdateMember()
        {
            var member = CreateTestMember(
                "e.fofana@example.com",
                "+3220235896",
                "Eden",
                "Fofana",
                DateTime.UtcNow.AddYears(-50),
                "125.45.67-890-12",
                "787654321"
            );

            var createdMember = await _memberService.CreateMemberAsync(member);

            var newPhoneNumber = "+3220235820";
            var newFirstName = "Aymen";

            createdMember.PhoneNumber = newPhoneNumber;
            createdMember.IdentityCard.FirstName = newFirstName;

            await _memberService.UpdateMemberAsync(createdMember);
            var updatedMember = await _memberService.GetMemberById(createdMember.Id);

            Assert.NotNull(updatedMember);
            Assert.Equal(newPhoneNumber, updatedMember.PhoneNumber);
            Assert.Equal(newFirstName, updatedMember.IdentityCard.FirstName);
        }
    }
}

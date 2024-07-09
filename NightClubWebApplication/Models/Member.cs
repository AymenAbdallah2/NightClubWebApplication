namespace NightClub.Models
{
    public class Member
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public IdentityCard IdentityCard { get; set; }
        public ICollection<MembershipCard> MembershipCards { get; set; }
        public bool? IsBlacklisted { get; set; }
        public DateTime? BlacklistUntil { get; set; }
    }
}

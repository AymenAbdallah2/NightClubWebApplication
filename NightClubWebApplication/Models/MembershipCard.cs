namespace NightClub.Models
{
    public class MembershipCard
    {
        public int Id { get; set; }
        public string UniqueIdentifier { get; set; }
        public int MemberId { get; set; }       
    }
}

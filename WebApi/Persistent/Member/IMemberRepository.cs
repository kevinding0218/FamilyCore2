namespace WebApi.Persistent.Member
{
    public interface IMemberRepository
    {
        void AddMember(DomainLibrary.Member.Member newMember);
    }
}

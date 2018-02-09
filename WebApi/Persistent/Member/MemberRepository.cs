namespace WebApi.Persistent.Member
{
    public class MemberRepository : IMemberRepository
    {
        private readonly FcDbContext _context;

        public MemberRepository(FcDbContext context)
        {
            this._context = context;
        }

        #region Create
        public void AddMember(DomainLibrary.Member.Member newMember)
        {
            _context.Add(newMember);
        }
        #endregion
    }
}

namespace PartyApi.Repository.IRepository;

public interface IUnitOfWork
{
    IPartyRepository PartyRepository { get; }
    IMemberRepository MemberRepository { get; }

    IUserRepository UserRepository { get; }

    Task CompleteAsync();
}
namespace PartyApi.Repository.IRepository;

public interface IUnitOfWork
{
    IPartyRepository PartyRepository { get; }
    IMemberRepository MemberRepository { get; }

    Task CompleteAsync();
}
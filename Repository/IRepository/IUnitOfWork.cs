namespace PartyApi.Repository.IRepository;

public interface IUnitOfWork
{
    IPartyRepository PartyRepository { get; }

    IUserRepository UserRepository { get; }

    Task CompleteAsync();
}
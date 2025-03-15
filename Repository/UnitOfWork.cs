using PartyApi.Data;
using PartyApi.Repository.IRepository;

namespace PartyApi.Repository;

public class UnitOfWork(
    PartyContext context,
    ILogger<UnitOfWork> logger
    ) : IUnitOfWork, IDisposable
{
    private readonly PartyContext _context = context;
    private readonly ILogger<UnitOfWork> _logger = logger;

    private IPartyRepository? partyRepository;

    private IUserRepository? userRepository;

    public IPartyRepository PartyRepository
    {
        get
        {
            partyRepository ??= new PartyRepository(
                _context,
                _logger
            );
            return partyRepository;
        }
    }

    public IUserRepository UserRepository
    {
        get
        {
            userRepository ??= new UserRepository(
                _context,
                _logger
            );
            return userRepository;
        }
    }

    public async Task CompleteAsync()
    {
        await _context.SaveChangesAsync();
    }

    private bool disposed = false;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
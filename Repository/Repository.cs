using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PartyApi.Data;
using PartyApi.Repository.IRepository;

namespace PartyApi.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly PartyContext _context;
    internal DbSet<T> _dbSet;

    public Repository(PartyContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task CreateAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await SaveAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }
    
    public async Task<List<T>> GetAllAsync(bool tracked = true)
    {
        IQueryable<T> query = _dbSet;

        if (!tracked)
        {
            query = query.AsNoTracking();
        }

        return await query.ToListAsync();
    }

    public async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await SaveAsync();
    } 

    public async Task DeleteByIdAsync(int id)
    {
        var entity = await _dbSet.FindAsync(id);

        if (entity != null) 
        {
            _dbSet.Remove(entity);
            await SaveAsync();
        }
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}
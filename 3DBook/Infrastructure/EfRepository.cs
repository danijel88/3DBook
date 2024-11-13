using Ardalis.SharedKernel;
using Ardalis.Specification.EntityFrameworkCore;

namespace _3DBook.Infrastructure;

using Ardalis.Specification;

public class EfRepository<T>(ApplicationDbContext dbContext) : RepositoryBase<T>(dbContext), IReadRepositoryBase<T>, IRepository<T> where T : class, IAggregateRoot
{
    
}
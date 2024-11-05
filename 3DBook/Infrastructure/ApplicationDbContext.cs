using System.Reflection;
using _3DBook.Core.FolderAggregate;
using _3DBook.Core.ItemAggregate;
using _3DBook.Core.MachineAggregate;
using Ardalis.SharedKernel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace _3DBook.Infrastructure;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,IDomainEventDispatcher? dispatcher) : IdentityDbContext(options)
{
    private readonly IDomainEventDispatcher? _dispatcher = dispatcher;
    public DbSet<Folder> Folders => Set<Folder>();
    public DbSet<Child> Children => Set<Child>();
    public DbSet<ChildImage> ChildImages => Set<ChildImage>();
    public DbSet<Machine> Machines => Set<Machine>();
    public DbSet<Item> Items => Set<Item>();
    public DbSet<ItemImage> ItemsImages => Set<ItemImage>();
    public DbSet<ItemType> ItemsTypes => Set<ItemType>();

    /// <inheritdoc />
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    /// <inheritdoc />
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

        if (_dispatcher == null) return result;

        var entitiesWithEvents = ChangeTracker.Entries<HasDomainEventsBase>()
            .Select(e => e.Entity)
            .Where(e => e.DomainEvents.Any())
            .ToArray();
        await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);
        return result;
    }

    public override int SaveChanges() => SaveChangesAsync().GetAwaiter().GetResult();
}
using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace _3DBook.Core.ItemAggregate;

public class ItemType : EntityBase
{
    public ItemType(string name)
    {
        GuardClauses.GuardClause.IsNullOrEmptyStringOrWhiteSpace(name,nameof(name));
        Name = name;
    }

    public string Name { get; private set; }
}
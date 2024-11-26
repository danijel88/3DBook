using Ardalis.SharedKernel;

namespace _3DBook.Core.ItemAggregate;

public class ItemImage : EntityBase, IAggregateRoot
{
    public ItemImage(string path, int itemId)
    {
        GuardClauses.GuardClause.IsZeroOrNegative(itemId, nameof(itemId));
        GuardClauses.GuardClause.IsNullOrEmptyStringOrWhiteSpace(path, nameof(path));
        Path = path;
        ItemId = itemId;
    }

    public string Path { get; private set; }
    public int ItemId { get; private set; }
}
using Ardalis.SharedKernel;

namespace _3DBook.Core.FolderAggregate;

public class ChildImage : EntityBase, IAggregateRoot
{
    public ChildImage(string path, int childId)
    {
        GuardClauses.GuardClause.IsZeroOrNegative(childId,nameof(childId));
        GuardClauses.GuardClause.IsNullOrEmptyStringOrWhiteSpace(path, nameof(path));
        Path = path;
        ChildId = childId;
    }

    public string Path { get; private set; }
    public int ChildId { get; private set; }
}
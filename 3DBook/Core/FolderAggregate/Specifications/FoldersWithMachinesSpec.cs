using Ardalis.Specification;

namespace _3DBook.Core.FolderAggregate.Specifications;

public class FoldersWithMachinesSpec : Specification<Folder>
{
    public FoldersWithMachinesSpec()
    {
        Query.Include(x => x.Machine);
    }
}
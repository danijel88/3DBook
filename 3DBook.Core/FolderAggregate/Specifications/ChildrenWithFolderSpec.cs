using Ardalis.Specification;

namespace _3DBook.Core.FolderAggregate.Specifications;

public class ChildrenWithFolderSpec : Specification<Child>
{
    public ChildrenWithFolderSpec(int folderId)
    {
        Query
            .Where(w=>w.FolderId == folderId)
            .Include(x => x.Folder)
            .Include(x=>x.ChildImage);
    }
}
using Ardalis.Specification;

namespace _3DBook.Core.FolderAggregate.Specifications;

public class ChildImagesByChildSpec : Specification<ChildImage>
{
    public ChildImagesByChildSpec(int childId)
    {
        Query
            .Where(w => w.ChildId == childId);
    }
}
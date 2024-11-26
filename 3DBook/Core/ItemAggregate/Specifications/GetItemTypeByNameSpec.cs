using Ardalis.Specification;

namespace _3DBook.Core.ItemAggregate.Specifications;

public class GetItemTypeByNameSpec : Specification<ItemType>
{
    public GetItemTypeByNameSpec(string name)
    {
        Query.Where(w => w.Name.ToLower() == name.ToLower());
    }
}
using Ardalis.Specification;

namespace _3DBook.Core.ItemAggregate.Specifications;

public class GetItemImagesByItemIdSpec : Specification<ItemImage>
{
    public GetItemImagesByItemIdSpec(int itemId)
    {
        Query
            .Where(x => x.ItemId == itemId);

    }
}
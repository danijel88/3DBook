using Ardalis.Specification;
using FluentValidation;

namespace _3DBook.Core.ItemAggregate.Specifications;

public class GetItemsWithTypesAndMachinesSpec : Specification<Item>
{
    public GetItemsWithTypesAndMachinesSpec()
    {
        Query
            .Include(x => x.Machine)
            .Include(x => x.ItemType)
            .Include(x=>x.ItemImage);
    }
}
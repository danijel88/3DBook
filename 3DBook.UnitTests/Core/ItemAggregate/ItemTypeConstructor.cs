using _3DBook.Core.ItemAggregate;

namespace _3DBook.UnitTests.Core.ItemAggregate;

public class ItemTypeConstructor
{
    private string _name = "Other";
    private ItemType? _itemType;

    private ItemType CreateNewItemType()
    {
        return new ItemType(_name);
    }

    [Fact]
    public void InitializeNewItemType()
    {
        _itemType = CreateNewItemType();
        Assert.Equal(_name,_itemType.Name);
    }
}
using _3DBook.Core.ItemAggregate;

namespace _3DBook.UnitTests.Core.ItemAggregate;

public class ItemConstructor
{
    private string _code = "item code";
    private string _plm = null;
    private int _machineId = 1;
    private int _itemTypeId = 1;
    private string _avatar = @"D:\\";
    private Item? _item;

    private Item CreateItem()
    {
        return new Item(_machineId, _plm, _code,_itemTypeId,_avatar);
    }

    [Fact]
    public void InitializeNewItem()
    {
        _item = CreateItem();
        Assert.NotNull(_item);
    }
    [Fact]
    public void InitializeCode()
    {
        _item = CreateItem();
        Assert.Equal(_code,_item.Code);
    }
    [Fact]
    public void InitializeMachineId()
    {
        _item = CreateItem();
        Assert.Equal(_machineId,_item.MachineId);
    }
    [Fact]
    public void InitializeItemTypeId()
    {
        _item = CreateItem();
        Assert.Equal(_machineId,_item.ItemTypeId);
    }

}
using _3DBook.Core.MachineAggregate;
namespace _3DBook.UnitTests.Core.MachineAggregate;

public class MachineConstructor
{
    private string _name = "Machine name";
    private string _sortCode = "MAA";
    private Machine? _machine;

    private Machine CreateMachine()
    {
        return new Machine(_name, _sortCode);
    }

    [Fact]
    public void InitializeMachine()
    {
        _machine = CreateMachine();

        Assert.Equal(_name,_machine.Name);
        Assert.Equal(_sortCode,_machine.SortCode);
    }
}
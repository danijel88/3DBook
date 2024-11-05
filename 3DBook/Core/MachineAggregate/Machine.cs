using Ardalis.SharedKernel;
using GuardClauses;

namespace _3DBook.Core.MachineAggregate;

public class Machine :EntityBase
{
    public Machine(string name, string sortCode)
    {
        GuardClause.IsNullOrEmptyString(name,nameof(name));
        GuardClause.IsNullOrEmptyString(sortCode,nameof(sortCode));
        Name = name;
        SortCode = sortCode;
    }

    public string Name { get; private set; }
    public string SortCode { get; private set; }
}
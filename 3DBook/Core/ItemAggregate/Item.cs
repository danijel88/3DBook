using Ardalis.GuardClauses;
using Ardalis.SharedKernel;

namespace _3DBook.Core.ItemAggregate;

public class Item : EntityBase
{
    public Item(int machineId, string plm, string code, int itemTypeId)
    {
        GuardClauses.GuardClause.IsZeroOrNegative(machineId,nameof(machineId));
        GuardClauses.GuardClause.IsZeroOrNegative(itemTypeId, nameof(itemTypeId));
        GuardClauses.GuardClause.IsNullOrEmptyString(code,nameof(code));
        MachineId = machineId;
        Plm = plm;
        Code = code;
        ItemTypeId = itemTypeId;
    }

    public string Code { get; private set; }
    public string Plm { get; private set; }
    public int MachineId { get; private set; }
    public int ItemTypeId { get; private set; }
}
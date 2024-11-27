using _3DBook.Core.MachineAggregate;
using Ardalis.SharedKernel;

namespace _3DBook.Core.ItemAggregate;

public class Item : EntityBase, IAggregateRoot
{
    public Item(int machineId, string? plm, string code, int itemTypeId, string avatar)
    {
        GuardClauses.GuardClause.IsZeroOrNegative(machineId, nameof(machineId));
        GuardClauses.GuardClause.IsZeroOrNegative(itemTypeId, nameof(itemTypeId));
        GuardClauses.GuardClause.IsNullOrEmptyString(code, nameof(code));
        GuardClauses.GuardClause.IsNullOrEmptyString(avatar,nameof(avatar));
        MachineId = machineId;
        Plm = plm;
        Code = code;
        ItemTypeId = itemTypeId;
        Avatar = avatar;
    }
    public Item(int machineId, string? plm, string code, int itemTypeId, string avatar,Machine machine, ItemType itemType, ItemImage itemImage) : this(machineId, plm, code, itemTypeId, avatar)
    {
        GuardClauses.GuardClause.IsZeroOrNegative(machineId, nameof(machineId));
        GuardClauses.GuardClause.IsZeroOrNegative(itemTypeId, nameof(itemTypeId));
        GuardClauses.GuardClause.IsNullOrEmptyString(code, nameof(code));
        GuardClauses.GuardClause.IsNullOrEmptyString(avatar, nameof(avatar));
        MachineId = machineId;
        Plm = plm;
        Code = code;
        ItemTypeId = itemTypeId;
        Avatar = avatar;
        Machine = machine;
        ItemType = itemType;
        ItemImage = itemImage;
    }


    public string Code { get; private set; }
    public string? Plm { get; private set; }
    public int MachineId { get; private set; }
    public int ItemTypeId { get; private set; }
    public string Avatar { get; private set; }
    public Machine Machine { get; private set; }
    public ItemType ItemType { get; private set; }
    public ItemImage ItemImage { get; private set; }


    public void UpdatePlm(string plm)
    {
        Plm = plm;
    }
}
using Ardalis.SharedKernel;

namespace _3DBook.Core.FolderAggregate;

public class Folder : EntityBase
{
    public Folder(int folds, decimal enter, decimal exit, int machineId,string machineSortCode)
    {
        GuardClauses.GuardClause.IsNegative(folds,nameof(folds));
        GuardClauses.GuardClause.IsNegative(enter, nameof(enter));
        GuardClauses.GuardClause.IsNegative(exit, nameof(exit));
        GuardClauses.GuardClause.IsZeroOrNegative(machineId, nameof(machineId));
        GuardClauses.GuardClause.IsNullOrEmptyString(machineSortCode,nameof(machineSortCode));
        Code = $"{folds}_{enter}_{exit}_{machineSortCode}";
        Folds = folds;
        Enter = enter;
        Exit = exit;
        MachineId = machineId;
    }

    public string Code {  get; private set; }
    public int Folds { get; private set; }
    public decimal Enter { get; private set; }
    public decimal Exit { get; private set; }
    public int MachineId { get; private set; }
}
using _3DBook.Core.MachineAggregate;
using Ardalis.SharedKernel;

namespace _3DBook.Core.FolderAggregate;

public class Folder : EntityBase,IAggregateRoot
{
    private Folder(){} //need for EF
    public Folder(int folds, decimal enter, decimal exit,int machineId,string sortCode)
    {
        
        GuardClauses.GuardClause.IsNegative(folds,nameof(folds));
        GuardClauses.GuardClause.IsNegative(enter, nameof(enter));
        GuardClauses.GuardClause.IsNegative(exit, nameof(exit));
        GuardClauses.GuardClause.IsZeroOrNegative(machineId, nameof(machineId));
        Code = $"{folds}_{enter}_{exit}_{sortCode}";
        Folds = folds;
        Enter = enter;
        Exit = exit;
        MachineId = machineId;
    }
    // Add a new constructor for testing only
    public Folder(int folds, decimal enter, decimal exit, int machineId, string sortCode, Machine machine)
        : this(folds, enter, exit, machineId, sortCode)
    {
        Machine = machine;
    }

    public string Code {  get; private set; }
    public int Folds { get; private set; }
    public decimal Enter { get; private set; }
    public decimal Exit { get; private set; }
    public int MachineId { get; private set; }
    public Machine Machine { get; private set; }
}
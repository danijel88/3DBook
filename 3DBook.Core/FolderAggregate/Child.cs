using Ardalis.SharedKernel;

namespace _3DBook.Core.FolderAggregate;

public class Child : EntityBase,IAggregateRoot
{
    /// <inheritdoc />
    public Child(int elasticSize, int folderId, int mouthLength, int mouthWidth, decimal thickness, string? plm,string avatar)
    {
        GuardClauses.GuardClause.IsNegative(elasticSize, nameof(elasticSize));
        GuardClauses.GuardClause.IsNegative(mouthLength, nameof(mouthLength));
        GuardClauses.GuardClause.IsNegative(mouthWidth, nameof(mouthWidth));
        GuardClauses.GuardClause.IsNegative(thickness, nameof(thickness));
        GuardClauses.GuardClause.IsZeroOrNegative(folderId, nameof(folderId));
        GuardClauses.GuardClause.IsNullOrEmptyString(avatar, nameof(avatar));
        ElasticSize = elasticSize;
        FolderId = folderId;
        MouthLength = mouthLength;
        MouthWidth = mouthWidth;
        Thickness = thickness;
        Plm = plm;
        Code = $"T{thickness}_Mw{mouthWidth}_Ml{mouthLength}_E{elasticSize}";
        Avatar = avatar;
    }

    public Child(int elasticSize, int folderId, int mouthLength, int mouthWidth, decimal thickness, string? plm, string avatar, Folder folder,ChildImage childImage) : this(elasticSize, folderId, mouthLength, mouthWidth, thickness, plm, avatar)
    {
        Folder = folder;
        ChildImage = childImage;
    }

    public decimal Thickness { get; private set; }
    public int MouthWidth { get; private set; }
    public int MouthLength { get; private set; }
    public int ElasticSize { get; private set; }
    public int FolderId { get; private set; }
    public string Code { get; private set; }
    public string? Plm { get; private set; }
    public string Avatar { get; private set; }
    public Folder Folder { get; private set; }
    public ChildImage ChildImage { get; private set; }

    public void UpdatePlm(string plm)
    {
        Plm = plm;
    }
    public void UpdateChild(int elasticSize,  int mouthLength, int mouthWidth, decimal thickness, string? plm)
    {
        GuardClauses.GuardClause.IsNegative(elasticSize, nameof(elasticSize));
        GuardClauses.GuardClause.IsNegative(mouthLength, nameof(mouthLength));
        GuardClauses.GuardClause.IsNegative(mouthWidth, nameof(mouthWidth));
        GuardClauses.GuardClause.IsNegative(thickness, nameof(thickness));
        ElasticSize = elasticSize;
        MouthLength = mouthLength;
        MouthWidth = mouthWidth;
        Thickness = thickness;
        Plm = plm;
        Code = $"T{thickness}_Mw{mouthWidth}_Ml{mouthLength}_E{elasticSize}";
    }
}
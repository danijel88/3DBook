using _3DBook.Core.FolderAggregate;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace _3DBook.UseCases.FolderAggregate.Utils;

public class ChildBuilder
{
    public Child UpdatePlm(Child child, string? plm)
    {
        return new Child(child.ElasticSize,child.FolderId,child.MouthLength,child.MouthWidth,child.Thickness,plm);
    }
}
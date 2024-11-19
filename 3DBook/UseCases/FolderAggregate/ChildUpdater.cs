using _3DBook.Core.FolderAggregate;

namespace _3DBook.UseCases.FolderAggregate;

public class ChildUpdater
{
    /// <summary>
    /// Updates the Plm property of the specified Child instance.
    /// </summary>
    /// <param name="child">The Child instance to update.</param>
    /// <param name="newPlm">The new value for the Plm property.</param>
    /// <exception cref="ArgumentNullException">Thrown if the child is null.</exception>
    public void UpdatePlm(Child child, string? newPlm)
    {
        if (child == null)
            throw new ArgumentNullException(nameof(child), "Child instance cannot be null.");

        if (child.Plm == newPlm)
            return; // No update needed if the value is already the same.

        // Use reflection to update the private setter.
        var plmProperty = typeof(Child).GetProperty(nameof(Child.Plm), System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public);

        if (plmProperty == null || !plmProperty.CanWrite)
            throw new InvalidOperationException("The Plm property cannot be updated.");

        plmProperty.SetValue(child, newPlm);
    }
}
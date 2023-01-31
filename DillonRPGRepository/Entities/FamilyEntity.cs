
namespace DillonRPG.Repository.Entities;

public class FamilyEntity : BaseEntity
{
    private const string DocumentType = nameof(FamilyEntity);

    /// <summary>
    /// Gets the DocumentType.
    /// </summary>
    public override string Type
    {
        get => DocumentType;
        set { }
    }

    /// <summary>
    /// Gets the PartitionKey for Family by Id.
    /// </summary>
    public override string? PartitionKey
    {
        get => GetPartitionKey(Id!);
        set { }
    }

    /// <summary>
    /// Gets or sets the Name.
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Gets the PartitionKey, the Id of the Family.
    /// </summary>
    /// <param name="Id">The Id of the Family.</param>
    /// <returns></returns>
    internal static string GetPartitionKey(string Id) => $"{Id}".ToLowerInvariant();
}


namespace DillonRPG.Repository.Entities;

public class AbilityEntity : BaseEntity
{
    private const string DocumentType = nameof(AbilityEntity);

    /// <summary>
    /// Gets the DocumentType.
    /// </summary>
    public override string Type
    {
        get => DocumentType;
        set { }
    }

    /// <summary>
    /// Gets the PartitionKey for Ability by Id.
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
    /// Gets the PartitionKey, the Id of the ability.
    /// </summary>
    /// <param name="Id">The Id of the ability.</param>
    /// <returns></returns>
    internal static string GetPartitionKey(string Id) => $"{Id}".ToLowerInvariant();
}

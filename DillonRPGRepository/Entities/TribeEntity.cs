
namespace DillonRPG.Repository.Entities;

public class TribeEntity : BaseEntity
{
    private const string DocumentType = nameof(TribeEntity);

    /// <summary>
    /// Gets the DocumentType.
    /// </summary>
    public override string Type
    {
        get => DocumentType;
        set { }
    }

    /// <summary>
    /// Gets the PartitionKey for Tribe by Id.
    /// </summary>
    public override string? PartitionKey
    {
        get => GetPartitionKey(Id!);
        set { }
    }

    /// <summary>
    /// gets or sets the TribeName.
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    public string TribeName { get; set; } = string.Empty;

    /// <summary>
    /// gets or sets the  TribeAbility.
    /// </summary>
    public AbilityEntity? Ability { get; set; }

    /// <summary>
    /// gets or sets the Class.
    /// </summary>
    public ClassEntity? Class { get; set; }

    /// <summary>
    /// gets or sets the Family.
    /// </summary>
    public FamilyEntity? Family { get; set; }

    /// <summary>
    /// Gets the PartitionKey, the Id of the tribe.
    /// </summary>
    /// <param name="Id">The Id of the tribe.</param>
    /// <returns></returns>
    internal static string GetPartitionKey(string Id) => $"{Id}".ToLowerInvariant();
}

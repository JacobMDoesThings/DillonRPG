﻿
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
    /// Gets the PartitionKey for Tribe by TribeName.
    /// </summary>
    public override string? PartitionKey
    {
        get => GetPartitionKey(TribeName);
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
    public AbilityEntity? TribeAbility { get; set; }

    /// <summary>
    /// gets or sets the Class.
    /// </summary>
    public ClassEntity? Class { get; set; }

    /// <summary>
    /// gets or sets the Family.
    /// </summary>
    public FamilyEntity? Family { get; set; }

    /// <summary>
    /// Gets the PartitionKey, the name of the tribe.
    /// </summary>
    /// <param name="tribeName">The name of the tribe.</param>
    /// <returns></returns>
    internal static string GetPartitionKey(string tribeName) => $"{tribeName}".ToLowerInvariant();
}

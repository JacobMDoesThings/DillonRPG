
namespace DillonRPG.Service.Client.DTOs;

public class Tribe : BaseDTO
{
    /// <summary>
    /// gets or sets the TribeName.
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    public string TribeName { get; set; } = string.Empty;

    /// <summary>
    /// gets or sets the Ability.
    /// </summary>
    public Ability? Ability { get; set; }

    /// <summary>
    /// gets or sets the Class.
    /// </summary>
    public Class? Class { get; set; }

    /// <summary>
    /// gets or sets the Family.
    /// </summary>
    public Family? Family { get; set; }
}

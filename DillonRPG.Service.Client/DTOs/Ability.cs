
namespace DillonRPG.Service.Client.DTOs;

public class Ability : BaseDTO
{
    [Required(AllowEmptyStrings = false)]
    public string Name { get; set; } = string.Empty;
}

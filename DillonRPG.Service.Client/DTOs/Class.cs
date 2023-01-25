
namespace DillonRPG.Service.Client.DTOs;

public class Class : BaseDTO
{
    [Required(AllowEmptyStrings = false)]
    public string Name { get; set; } = string.Empty;
}

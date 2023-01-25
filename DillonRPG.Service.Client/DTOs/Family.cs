
namespace DillonRPG.Service.Client.DTOs;

public class Family : BaseDTO 
{
    [Required(AllowEmptyStrings = false)]
    public string Name { get; set; } = string.Empty;
}


namespace DillonRPG.Service.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class AbilitiesController : ControllerBase
{
    [Authorize(Policy = "GodModePolicy")]
    [HttpGet(Name = "GetAbilities")]
    public ActionResult<IEnumerable<AbilityEntity>> Get()
    {
        return new AbilityEntity[]
        {
            new AbilityEntity()
            {
                Name = "Ability 1",
                Id = "1"
            },
                new AbilityEntity()
            {
                Name = "Ability 2",
                Id = "2"
            },
                new AbilityEntity()
            {
                Name = "Ability 3",
                Id = "3"
            },
                new AbilityEntity()
            {
                Name = "Ability 4",
                Id = "4"
            }
        };
    }
}
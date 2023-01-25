
namespace DillonRPG.Service.Controllers;

[Route("[controller]")]
[ApiController]
public class FamiliesController : ControllerBase
{
    [Authorize(Policy = "GodModePolicy")]
    [HttpGet(Name = "GetFamilies")]
    public ActionResult<IEnumerable<FamilyEntity>> Get()
    {
        return new FamilyEntity[]
        {
    new FamilyEntity()
    {
        Name = "Family 1",
        Id = "1"
    },
        new FamilyEntity()
    {
        Name = "Family 2",
        Id = "2"
    },
        new FamilyEntity()
    {
        Name = "Family 3",
        Id = "3"
    },
        new FamilyEntity()
    {
        Name = "Family 4",
        Id = "4"
    }
        };
    }
}

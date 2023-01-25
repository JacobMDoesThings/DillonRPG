
namespace DillonRPG.Service.Controllers;

[Route("[controller]")]
[ApiController]
public class ClassesController : ControllerBase
{
    [Authorize(Policy = "GodModePolicy")]
    [HttpGet(Name = "GetClasses")]
    public ActionResult<IEnumerable<ClassEntity>> Get()
    {
        return new ClassEntity[]
        {
        new ClassEntity()
        {
            Name = "Class 1",
            Id = "1"
        },
            new ClassEntity()
        {
            Name = "Class 2",
            Id = "2"
        },
            new ClassEntity()
        {
            Name = "Class 3",
            Id = "3"
        },
            new ClassEntity()
        {
            Name = "Class 4",
            Id = "4"
        }
        };
    }
}

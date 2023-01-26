
namespace DillonRPG.Service.Controllers;

public class ClassesController : BaseController
{
    public ClassesController(DillonRPGContext context, ILogger<BaseController> logger)
    : base(context, logger)
    {
    }

    [Authorize(Policy = "GodModePolicy")]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return await GetEntitiesAsync<ClassEntity>().ConfigureAwait(false);
    }

    [Authorize(Policy = "GodModePolicy")]
    [HttpPost]
    public async Task<IActionResult> Post(ClassEntity entity)
    {
        return (await PostEntityAsync(entity).ConfigureAwait(false)).ActionResult;
    }
}

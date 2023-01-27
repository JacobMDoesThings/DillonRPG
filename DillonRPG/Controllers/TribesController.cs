namespace DillonRPG.Service.Controllers;

public class TribesController : BaseController
{
    public TribesController(DillonRPGContext context, ILogger<BaseController> logger)
    : base(context, logger)
    {
    }

    [Authorize(Policy = "GodModePolicy")]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return await GetEntitiesAsync<TribeEntity>().ConfigureAwait(false);
    }

    [Authorize(Policy = "GodModePolicy")]
    [HttpPost]
    public async Task<IActionResult> Post(TribeEntity entity)
    {
        return (await PostEntityAsync(entity).ConfigureAwait(false)).ActionResult;
    }
}

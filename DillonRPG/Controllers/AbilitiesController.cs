namespace DillonRPG.Service.Controllers;


public class AbilitiesController : BaseController
{
    public AbilitiesController(DillonRPGContext context, ILogger<BaseController> logger)
    : base(context, logger)
    {
    }
    [Authorize(Policy = "GodModePolicy")]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return await GetEntitiesAsync<AbilityEntity>().ConfigureAwait(false);
    }

    [Authorize(Policy = "GodModePolicy")]
    [HttpPost]
    public async Task<IActionResult> Post(AbilityEntity entity)
    {
        return (await PostEntityAsync(entity).ConfigureAwait(false)).ActionResult;
    }

    [Authorize(Policy = "GodModePolicy")]
    [HttpDelete]
    public async Task<IActionResult> Delete(AbilityEntity entity)
    {
        return await DeleteEntityAsync<AbilityEntity>(entity).ConfigureAwait(false);
    }
}
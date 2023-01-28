
namespace DillonRPG.Service.Controllers;


public class AbilitiesController : BaseController
{
    public AbilitiesController(DillonRPGContext context, ILogger<AbilitiesController> logger)
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
    public async Task<IActionResult> Delete(string id)
    {
        if (!_context.Set<TribeEntity>().AsEnumerable()
            .Where(x => x.Ability != null
            && !string.IsNullOrEmpty(x.Ability.Id))
            .Any(r => r.Ability!.Id!.Equals(id)))
        {
            return await DeleteEntityAsync<ClassEntity>(id);
        }
        else
        {
            _logger.LogError("Failed attempt to delete {Entity} with the Id: {Id} due to it existing in a {TribeEntity}",
                nameof(ClassEntity), id, nameof(TribeEntity));
            return Conflict($"A relationship exists between Class with Id {id} and at least one {nameof(TribeEntity)}, " +
                "this relationship must be resolved to continue...");
        }
    }
}
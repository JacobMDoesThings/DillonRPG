
namespace DillonRPG.Service.Controllers;

public class FamiliesController : BaseController
{
    public FamiliesController(DillonRPGContext context, ILogger<FamiliesController> logger) 
        : base(context, logger)
    {     
    }

    [Authorize(Policy = "GodModePolicy")]
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return await GetEntitiesAsync<FamilyEntity>().ConfigureAwait(false);
    }

    [Authorize(Policy = "GodModePolicy")]
    [HttpPost]
    public async Task<IActionResult> Post(FamilyEntity family)
    {
        return (await PostEntityAsync(family).ConfigureAwait(false)).ActionResult;
    }
    [Authorize(Policy = "GodModePolicy")]
    [HttpDelete]
    public async Task<IActionResult> Delete(string id)
    {
        if (!_context.Set<TribeEntity>().AsEnumerable()
            .Where(x => x.Family != null
            && !string.IsNullOrEmpty(x.Family.Id))
            .Any(r => r.Ability!.Id!.Equals(id)))
        {
            return await DeleteEntityAsync<FamilyEntity>(id);
        }
        else
        {
            _logger.LogError("Failed attempt to delete {Entity} with the Id: {Id} due to it existing in a {TribeEntity}",
                nameof(FamilyEntity), id, nameof(TribeEntity));
            return Conflict($"A relationship exists between FamilyEntity with Id {id} and at least one {nameof(TribeEntity)}, " +
                "this relationship must be resolved to continue...");
        }
    }
}

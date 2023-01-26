
namespace DillonRPG.Service.Controllers;

public class FamiliesController : BaseController
{
    public FamiliesController(DillonRPGContext context, ILogger<BaseController> logger) 
        : base(context, logger)
    {     
    }

    //[Authorize(Policy = "GodModePolicy")]
    //[HttpGet]
    //public async Task<IActionResult> Get(string key)
    //{
    //    return await GetEntityAsync<FamilyEntity>(key).ConfigureAwait(false);
    //}

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


//    context.Database.EnsureCreated()
}

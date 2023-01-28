
using System.Reflection.Metadata;

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
        try
        {
            var tribeSet = _context.Set<TribeEntity>();

            if (tribeSet is not null)
            {
                foreach (var set in tribeSet)
                {
                    await _context.Entry(set).Reference(a => a.Ability).LoadAsync();
                    await _context.Entry(set).Reference(c => c.Class).LoadAsync();
                    await _context.Entry(set).Reference(f => f.Family).LoadAsync();
                }
                return Ok(tribeSet);
            }
        }
        catch(Exception ex) 
        {
            _logger.LogError("Error encountered on getting records for entities of type {type} from database. {exception}", typeof(TribeEntity).Name, ex);
            return StatusCode(500);
        }

        return NotFound();
    }

    [Authorize(Policy = "GodModePolicy")]
    [HttpPost]
    public async Task<IActionResult> Post(TribeEntity entity)
    {
        return (await PostEntityAsync(entity).ConfigureAwait(false)).ActionResult;
    }
}

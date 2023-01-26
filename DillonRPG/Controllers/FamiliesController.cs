
using Microsoft.Graph;

namespace DillonRPG.Service.Controllers;

[Route("[controller]")]
[ApiController]
public class FamiliesController : ControllerBase
{
    private readonly DbContext _context;

    public FamiliesController(DillonRPGContext context)
    {
        _context = context;
    }

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
    [Authorize(Policy = "GodModePolicy")]
    [HttpPost(Name = "PostFamilies")]
    public async Task<IActionResult> Post(FamilyEntity family)
    {
        // _context.Database.EnsureCreated();

        _context.Add(family);

        var entityEntry = _context.Set<FamilyEntity>().Add(family);

        try
        {
            var added = await _context.SaveChangesAsync();
            if (added > 0)
            {
                await entityEntry.ReloadAsync();
            }
        }
        catch (Exception ex)
        {
            //_logger.LogError(ErrorLogEventIds.DatabaseOperationErrorEvent, ex, $"Error encountered on creating new records for entity of type {typeof(T).Name} in database.");
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        //if (returnIdOnly)
        //{
        //    return (Ok(entityEntry.Entity.Id), true);
        //}

        return StatusCode((int)HttpStatusCode.OK, entityEntry.Entity);
    }


//    context.Database.EnsureCreated()
}

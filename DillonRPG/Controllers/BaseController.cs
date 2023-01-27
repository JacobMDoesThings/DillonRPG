
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;

namespace DillonRPG.Service.Controllers;

//[Authorize]
[ApiController]
[Route("[controller]")]
public class BaseController : ControllerBase
{
    private protected readonly DbContext _context;
    private ILogger<BaseController> _logger;

    public BaseController(DillonRPGContext context, ILogger<BaseController> logger)
    {
        _context = context;
        _logger = logger;
    }

    /// <summary>
    /// Gets the entities asynchronous.
    /// </summary>
    /// <typeparam name="T">Type of Entity.</typeparam>
    /// <param name="handler">object handler.</param>
    /// <returns>Returns an entities result.</returns>
    protected virtual async Task<IActionResult> GetEntitiesAsync<T>()
        where T : BaseEntity
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var entitySet = _context.Set<T>();
            if (await entitySet.CountAsync() == 0)
            {
                return NotFound();
            }

            return Ok(entitySet);
        }
        catch (Exception ex)
        {
            _logger.LogError("Error encountered on getting records for entities of type {type} from database. {exception}", typeof(T).Name, ex);
            return StatusCode(500);
        }
    }

    /// <summary>
    /// Gets the entity asynchronous.
    /// </summary>
    /// <typeparam name="T">Type of Entity.</typeparam>
    /// <param name="key">The identifier.</param>
    /// <returns>Returns an entity result.</returns>
    protected virtual async Task<IActionResult> GetEntityAsync<T>(string key)
        where T : BaseEntity
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var entity = await _context.Set<T>().FirstOrDefaultAsync(e => e.Id == key);
            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }
        catch (Exception ex)
        {           
            _logger.LogError("Error encountered on getting records for entity with Id#{key} of type {type} from database. {exception}", key, typeof(T).Name, ex);
            return StatusCode(500);
        }
    }

    protected virtual async Task<IActionResult> DeleteEntityAsync<T>(T entity)
       where T : BaseEntity
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
           // var entity =  await _context.Set<T>().FirstAsync(e => e.Id == key);
            //_context.Attach(entity);
            _context.Remove(entity);
            _context.SaveChanges();
            if (entity == null)
            {
                return NotFound();
            }

            return Ok();
        }
        catch (Exception ex)
        {
            _logger.LogError("Error encountered on getting records for entity  of type {type} from database. {exception}", typeof(T).Name, ex);
            return StatusCode(500);
        }
    }

    protected virtual async Task<(IActionResult ActionResult, bool IsSuccess)> PostEntityAsync<T>(T entity, bool returnIdOnly = false)
       where T : BaseEntity
    {
        _context.Attach(entity);
         if (!ModelState.IsValid)
         {
             return (BadRequest(ModelState), default);
         }

        var entityEntry = _context.Set<T>().Add(entity);

        try
        {
            var added = await _context.SaveChangesAsync();
            if (added > 0)
            {
                await entityEntry.ReloadAsync();
            }
        }

        // Todo : Handle Cosmos DB Exception violating Key Constraints
        /* catch (DbUpdateException ex)
         {
             if (ex.InnerException is SqlException sqlEx && sqlEx != null)
             {
                 if (sqlEx.Number == 2627)
                 {
                     ModelState.AddModelError(nameof(entity), $"Violation of unique key constraints, {typeof(T).Name} already exists with unique constraint values.");
                     return (UnprocessableEntity(ModelState), default);
                 }
             }

             return (UnprocessableEntity(entity), default);
         }*/

        catch (Exception ex)
        {
            _logger.LogError("Error encountered on creating new records for entity of type {type} in database. {exception} ",typeof(T).Name, ex);
            return (StatusCode(500), default);
        }

        if (returnIdOnly)
        {
            return (Ok(entityEntry.Entity.Id), true);
        }

        return (Ok(entityEntry.Entity), true);
    }

    /// <summary>
    /// Unexpected the server error.
    /// </summary>
    /// <returns>Returns <see cref="ObjectResult"/> for response.</returns>
   // public ObjectResult InternalServerError() => StatusCode((int) HttpStatusCode.InternalServerError, "Unexpected server error, please try again later or contact support.");

}

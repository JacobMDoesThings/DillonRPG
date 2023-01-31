
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DillonRPG.Service.Controllers;

//[Authorize]
[ApiController]
[Route("[controller]")]
public class BaseController : ControllerBase
{
    private protected readonly DbContext _context;
    protected ILogger _logger { get; }

    public BaseController(DillonRPGContext context, ILogger logger)
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

    protected virtual async Task<IActionResult> DeleteEntityAsync<T>(string id)
       where T : BaseEntity
    {
        T? entity = null;
        try
        {
            entity = await _context.FindAsync<T>(id);

            if (entity is null)
            {
                return NotFound();
            }
            _context.Remove(entity);
            _context.SaveChanges();
            return Ok();
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return Conflict(entity);
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

        catch (Exception ex)
        {
            _logger.LogError("Error encountered on creating new records for entity of type {type} in database. {exception} ", typeof(T).Name, ex);
            return (StatusCode(500), default);
        }

        if (returnIdOnly)
        {
            return (Ok(entityEntry.Entity.Id), true);
        }

        return (Ok(entityEntry.Entity), true);
    }

    /// <summary>
    /// Update an entity asynchronous in data store.
    /// </summary>
    /// <typeparam name="T">Type of Entity.</typeparam>
    /// <param name="key">The entity identifier.</param>
    /// <param name="entity">The entity.</param>
    /// <returns>
    /// Returns an updated entity result.
    /// </returns>
    protected virtual async Task<(IActionResult ActionResult, bool IsSuccess)> PatchEntityAsync<T>(string key, T entity)
        where T : BaseEntity
    {
        if (!ModelState.IsValid)
        {
            return (BadRequest(ModelState), default);
        }

        EntityEntry<T>? entityEntry = default;
        try
        {
            entityEntry = _context.Entry(entity);
            entityEntry.State = EntityState.Modified;
            entityEntry.RemoveBaseMetadataFromUpdate();

            var entitySet = _context.Set<T>();


            T? storedEntity = await entitySet.FirstOrDefaultAsync(e => e.Id == key);

            if (storedEntity == null)
            {
                return (NotFound(), default);
            }

            entitySet.Update(entity);

            _context.SaveChanges();

        }
        catch (DbUpdateConcurrencyException ex)
        {
            if (entityEntry is not null)
            {
                await entityEntry.ReloadAsync();
            }

            return (Conflict(entityEntry?.Entity), default);
        }
        catch (DbUpdateException ex)
        {
            return (UnprocessableEntity(entity), default);
        }

        return (Ok(entityEntry.Entity), true);
    }

}


namespace DillonRPG.Repository;

public class DillonRPGContext : DbContext
{

    //private readonly Action<EntityTypeBuilder<ImageEntity>> _configureImageEntity = (_entityTypeBuilder) =>
    //{
    //    _entityTypeBuilder.Property(x => x.UserEmail);
    //    _entityTypeBuilder.Property(x => x.FileName);
    //    _entityTypeBuilder.Property(x => x.FolderName);
    //};

    public DillonRPGContext(DbContextOptions options)
        : base(options)
    { 
        ChangeTracker.LazyLoadingEnabled = true;
        ChangeTracker.AutoDetectChangesEnabled = true;
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //modelBuilder
        //    .ConfigureEntityDefault<ImageEntity>("images", _configureImageEntity);
    }
}

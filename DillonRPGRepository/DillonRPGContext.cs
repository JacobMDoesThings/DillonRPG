
namespace DillonRPG.Repository;

public class DillonRPGContext : DbContext
{
    private readonly Action<EntityTypeBuilder<AbilityEntity>> _configureAbilityEntitity = (_entityTypeBuilder) =>
    {
        _entityTypeBuilder.Property(x => x.Name);
    };

    private readonly Action<EntityTypeBuilder<FamilyEntity>> _configureFamilyEntity = (_entityTypeBuilder) =>
    {
        _entityTypeBuilder.Property(x => x.Name);
    };

    private readonly Action<EntityTypeBuilder<ClassEntity>> _configureClassEntity = (_entityTypeBuilder) =>
    {
        _entityTypeBuilder.Property(x => x.Name);
    };

    private readonly Action<EntityTypeBuilder<TribeEntity>> _configureTribeEntity = (_entityTypeBuilder) =>
    {
        _entityTypeBuilder.Property(x => x.TribeName);
        _entityTypeBuilder.HasOne(x => x.Ability).WithMany();
        _entityTypeBuilder.HasOne(x => x.Class).WithMany();
        _entityTypeBuilder.HasOne(x => x.Family).WithMany();
    };

    public DillonRPGContext(DbContextOptions options)
        : base(options)
    { 
        ChangeTracker.AutoDetectChangesEnabled = true;
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .ConfigureEntityDefault("abilities", _configureAbilityEntitity)
            .ConfigureEntityDefault("families", _configureFamilyEntity)
            .ConfigureEntityDefault("classes", _configureClassEntity)
            .ConfigureEntityDefault("tribes", _configureTribeEntity);
    }
}

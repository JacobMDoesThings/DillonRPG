namespace DillonRPG.Repository;

using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Cosmos;
public static class RepositoryExtensions
{
    public static ModelBuilder ConfigureEntityDefault<T>(this ModelBuilder builder,
                                                      string containerName,
                                                      Action<EntityTypeBuilder<T>>? configure = default,
                                                      bool ignoreDefaultConfig = default)
        where T : BaseEntity
    { 
        var entityBuilder = builder.Entity<T>();
        if (!ignoreDefaultConfig)
        {
            entityBuilder.ConfigureDefaultProperties(containerName);
        }

        configure?.Invoke(entityBuilder);
        return builder;
    }

    public static EntityTypeBuilder<T> ConfigureDefaultProperties<T>(this EntityTypeBuilder<T> entityTypeBuilder,
                                                                     string containerName)
        where T : BaseEntity
    { 
        entityTypeBuilder.ToContainer(containerName);
        entityTypeBuilder.HasKey(e => e.Id);
        entityTypeBuilder.Property(e => e.Id).ToJsonProperty("id").ValueGeneratedOnAdd();
        entityTypeBuilder.Property(e => e.Type);
        entityTypeBuilder.HasPartitionKey(e => e.PartitionKey);
        entityTypeBuilder.Property(e => e.PartitionKey).ValueGeneratedOnAdd();
        entityTypeBuilder.Property(e => e.CreatedOn).ValueGeneratedOnAdd();
        entityTypeBuilder.Property(e => e.Etag).IsETagConcurrency();
        entityTypeBuilder.Property(e => e.UpdatedOn).ValueGeneratedOnAddOrUpdate();
        entityTypeBuilder.HasDiscriminator(e => e.Type);

        return entityTypeBuilder;
    }

    public static void RemoveBaseMetadataFromUpdate<T>(this EntityEntry<T> entityEntry)
        where T : BaseEntity
    {
        entityEntry.Property(x => x.Id).IsModified = false;
        entityEntry.Property(x => x.PartitionKey).IsModified = false;
        entityEntry.Property(x => x.Type).IsModified = false;
        entityEntry.Property(x => x.CreatedOn).IsModified = false;
        entityEntry.Property(x => x.UpdatedOn).IsModified = false;
        entityEntry.Property(x => x.Etag).IsModified = false;
    }
}
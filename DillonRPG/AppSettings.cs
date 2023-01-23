namespace DillonRPG.Service;

internal class AppSettings
{
    public CosmosRepositoryOptions? CosmosRepositoryOptions { get; set; }
    public GraphApi? GraphApi { get; set; }
    public BlobStorage? BlobStorage { get; set; }
    public SecurityGroups? SecurityGroups { get; set; }
}

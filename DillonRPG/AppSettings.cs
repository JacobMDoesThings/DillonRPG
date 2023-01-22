namespace DillonRPG.Service;

public class AppSettings
{
    public CosmosRepositoryOptions? CosmosRepositoryOptions { get; set; }
    public GraphApi? GraphApi { get; set; }
    public BlobStorage? BlobStorage { get; set; }

}

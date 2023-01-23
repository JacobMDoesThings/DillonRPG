
namespace DillonRPG.Common.Configuration
{
    public class CosmosRepositoryOptions : CosmosClientOptions
    {
        /// <summary>
        /// Gets or sets the Cosmos EndPoint.
        /// </summary>
        public string? EndPoint { get; set; }

        /// <summary>
        /// Gets or sets the AccountKey.
        /// </summary>
        public string? AccountKey { get; set; }

        /// <summary>
        /// Gets or sets the Database Name.
        /// </summary>
        public string? DatabaseName { get; set; }
    }
}


namespace DillonRPG.Repository.Entities;

public class WeatherForecastEntity : BaseEntity
{
    private const string DocumentType = nameof(WeatherForecastEntity);

    /// <summary>
    /// Gets the PartitionKey for images by Date.
    /// </summary>
    public override string? PartitionKey 
    { 
        get => GetPartitionKey(Date.ToString()); 
        set { } 
    }

    /// <summary>
    /// Gets the DocumentType.
    /// </summary>
    public override string Type
    {
        get => DocumentType;
        set { }
    }

    public DateTime Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }

    /// <summary>
    /// Gets the PartitionKey, date.
    /// </summary>
    /// <param name="date">The date of the weather.</param>
    /// <returns></returns>
    internal static string GetPartitionKey(string date) => $"{date}".ToLowerInvariant();
}

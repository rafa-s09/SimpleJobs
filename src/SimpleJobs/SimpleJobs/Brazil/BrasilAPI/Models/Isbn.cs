namespace SimpleJobs.Brazil.BrasilAPI;

public class Isbn
{
    /// <summary>
    /// Field Name: isbn
    /// </summary>
    [JsonPropertyName("isbn")]
    public string? IsbnCode { get; set; }

    /// <summary>
    /// Field Name: title
    /// </summary>
    [JsonPropertyName("title")]
    public string? Title { get; set; }

    /// <summary>
    /// Field Name: subtitle
    /// </summary>
    [JsonPropertyName("subtitle")]
    public string? Subtitle { get; set; }

    /// <summary>
    /// Field Name: authors
    /// </summary>
    [JsonPropertyName("authors")]
    public List<string>? Authors { get; set; }

    /// <summary>
    /// Field Name: publisher
    /// </summary>
    [JsonPropertyName("publisher")]
    public string? Publisher { get; set; }

    /// <summary>
    /// Field Name: synopsis
    /// </summary>
    [JsonPropertyName("synopsis")]
    public string? Synopsis { get; set; }

    /// <summary>
    /// Field Name: dimensions
    /// </summary>
    [JsonPropertyName("dimensions")]
    public IsbnDimensions? Dimensions { get; set; }

    /// <summary>
    /// Field Name: year
    /// </summary>
    [JsonPropertyName("year")]
    public int? Year { get; set; }

    /// <summary>
    /// Field Name: format
    /// </summary>
    [JsonPropertyName("format")]
    public string? Format { get; set; }

    /// <summary>
    /// Field Name: page_count
    /// </summary>
    [JsonPropertyName("page_count")]
    public int? Pages { get; set; }

    /// <summary>
    /// Field Name: subjects
    /// </summary>
    [JsonPropertyName("subjects")]
    public List<string>? Subjects { get; set; }

    /// <summary>
    /// Field Name: location
    /// </summary>
    [JsonPropertyName("location")]
    public string? Location { get; set; }

    /// <summary>
    /// Field Name: retail_price
    /// </summary>
    [JsonPropertyName("retail_price")]
    public IsbnRetailPrice? RetailPrice { get; set; }

    /// <summary>
    /// Field Name: cover_url
    /// </summary>
    [JsonPropertyName("cover_url")]
    public string? CoverUrl { get; set; }

    /// <summary>
    /// Field Name: provider
    /// </summary>
    [JsonPropertyName("provider")]
    public string? Provider { get; set; }
}

public class IsbnDimensions
{
    /// <summary>
    /// Field Name: width
    /// </summary>
    [JsonPropertyName("width")]
    public float? Width { get; set; }

    /// <summary>
    /// Field Name: height
    /// </summary>
    [JsonPropertyName("height")]
    public float? Height { get; set; }

    /// <summary>
    /// Field Name: unit
    /// </summary>
    [JsonPropertyName("unit")]
    public string? Unit { get; set; }
}

public class IsbnRetailPrice
{
    /// <summary>
    /// Field Name: currency
    /// </summary>
    [JsonPropertyName("currency")]
    public string? Currency { get; set; }

    /// <summary>
    /// Field Name: amount
    /// </summary>
    [JsonPropertyName("amount")]
    public float? Amount { get; set; }
}




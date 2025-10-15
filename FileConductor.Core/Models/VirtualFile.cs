using System.Text.Json.Serialization;

namespace FileConductor.Core.Models;

/// <summary>
/// Represents a single file entry, mapping to the actual data in the VHDX.
/// </summary>
public class VirtualFile
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "Untitled File";
    
    [JsonPropertyName("contentHash")]
    public string ContentHash { get; set; } = string.Empty;

    [JsonPropertyName("partNumber")]
    public int PartNumber { get; set; } = 1;
}
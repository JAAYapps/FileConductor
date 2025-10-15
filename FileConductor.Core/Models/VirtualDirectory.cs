using System.Text.Json.Serialization;

namespace FileConductor.Core.Models;

/// <summary>
/// Represents a directory within the virtual filesystem.
/// </summary>
public class VirtualDirectory
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "/";

    [JsonPropertyName("directories")]
    public List<VirtualDirectory> Directories { get; set; } = new();

    [JsonPropertyName("files")]
    public List<VirtualFile> Files { get; set; } = new();
}
using System.Text.Json.Serialization;

namespace FileConductor.Core.Models;

// This context is used for AOT compilation and performance improvements
[JsonSerializable(typeof(Project))]
public partial class ProjectJsonContext : JsonSerializerContext {}

/// <summary>
/// Represents the top-level project manifest.
/// </summary>
public class Project
{
    [JsonPropertyName("name")]
    public string Name { get; set; } = "Untitled Project";

    [JsonPropertyName("version")]
    public int Version { get; set; } = 1;

    [JsonPropertyName("root")]
    public VirtualDirectory Root { get; set; } = new();
}
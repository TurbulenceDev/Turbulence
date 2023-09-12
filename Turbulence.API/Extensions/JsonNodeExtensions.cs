using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Turbulence.API.Extensions;

public static class JsonNodeExtensions
{
    /// <summary>Converts the <see cref="T:System.Text.Json.Nodes.JsonNode" /> representing a single JSON value into a
    /// <typeparamref name="TValue" />.</summary>
    /// <param name="node">The <see cref="T:System.Text.Json.Nodes.JsonNode" /> to convert.</param>
    /// <param name="obj">A <typeparamref name="TValue" /> representation of the JSON value.</param>
    /// <param name="options">Options to control the behavior during parsing.</param>
    /// <typeparam name="TValue">The type to deserialize the JSON value into.</typeparam>
    /// <exception cref="T:System.Text.Json.JsonException">
    /// <typeparamref name="TValue" /> is not compatible with the JSON.</exception>
    /// <exception cref="T:System.NotSupportedException">There is no compatible
    /// <see cref="T:System.Text.Json.Serialization.JsonConverter" /> for <typeparamref name="TValue" /> or its
    /// serializable members.</exception>
    /// <returns>True if the deserialization was successful, false otherwise.</returns>
    [RequiresUnreferencedCode("JSON serialization and deserialization might require types that cannot be statically analyzed. Use the overload that takes a JsonTypeInfo or JsonSerializerContext, or make sure all of the required types are preserved.")]
    [RequiresDynamicCode("JSON serialization and deserialization might require types that cannot be statically analyzed and might need runtime code generation. Use System.Text.Json source generation for native AOT applications.")]
    public static bool TryDeserialize<TValue>(this JsonNode? node,
        [NotNullWhen(true)] out TValue? obj,
        JsonSerializerOptions? options = null)
    {
        try
        {
            if (node.Deserialize<TValue>(options) is { } valid)
            {
                obj = valid;
                return true;
            }
        }
        catch (JsonException)
        {
        }
        
        obj = default;
        return false;
    }
}
using System.Text.Json.Serialization;
using System.Text.Json;
using CQRS.Core.Events;
using CQRS.Core.Exceptions;

namespace CQRS.Core.JsonConverters
{
    public class EventJsonConverter : JsonConverter<Event>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsAssignableFrom(typeof(Event));
        }

        public override Event Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (!JsonDocument.TryParseValue(ref reader, out var doc))
            {
                throw new JsonException($"Failed to parse {nameof(JsonDocument)}!");
            }

            if (!doc.RootElement.TryGetProperty("Type", out var type))
            {
                throw new JsonException("Could not detect the Type discriminator property!");
            }

            var typeDiscriminator = type.GetString();
            var json = doc.RootElement.GetRawText();

            return (Event)JsonSerializer.Deserialize(json, Type.GetType(typeDiscriminator), options);
        }

        public override void Write(Utf8JsonWriter writer, Event value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}

using System.Text.Json;
using System.Text.Json.Serialization;

namespace Infrastructure.ClickUp.DTOs
{
    public class UpdateWebhookRequest
    {
        public required string Endpoint { get; set; }
        public required string Status { get; set; }

        [JsonConverter(typeof(EventsConverter))]
        public List<string> Events { get; set; } = new();

    }

    public class UpdateWebhookResponse
    {
        public required Guid Id { get; set; }
        public required WebHookInfo Webhook { get; set; }
    }

    public class EventsConverter : JsonConverter<List<string>>
    {
        public override List<string> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var value = reader.GetString();
                if (value == "*")
                    return ["*"];
                else
                    return [value];
            }

            if (reader.TokenType == JsonTokenType.StartArray)
            {
                var list = JsonSerializer.Deserialize<List<string>>(ref reader, options);
                return list ?? [];
            }

            throw new JsonException("Invalid JSON for Events");
        }

        public override void Write(Utf8JsonWriter writer, List<string> value, JsonSerializerOptions options)
        {
            if (value.Count == 1 && value[0] == "*")
                writer.WriteStringValue("*");
            else
                JsonSerializer.Serialize(writer, value, options);
        }
    }
}

using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace Domain;

public class Event
{
    public Guid Id { get; set; }
    public Guid CorrelationId { get; set; }
    public DateTime DateTime { get; set; }
    public string EventType { get; set; } = null!;
    public string PayloadJson { get; set; } = "{}";

    [NotMapped]
    public JsonDocument Payload
    {
        get => JsonDocument.Parse(PayloadJson);
        set => PayloadJson = value.RootElement.GetRawText();
    }
}

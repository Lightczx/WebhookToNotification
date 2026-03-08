namespace WebhookToNotification.Models;

public class WebhookPayload
{
    public required string Type { get; init; }

    public required string Title { get; init; }

    public required string Content { get; init; }
}

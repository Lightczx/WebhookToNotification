using Microsoft.AspNetCore.Mvc;
using System;
using WebhookToNotification.Models;
using WebhookToNotification.Services;

namespace WebhookToNotification.Controllers;

[Route("[controller]")]
public class WebhookController : ControllerBase
{
    private readonly ToastNotificationService toastService;

    public WebhookController(ToastNotificationService toastService)
    {
        this.toastService = toastService;
    }

    [HttpPost]
    public IActionResult Post([FromBody] WebhookPayload payload)
    {
        try
        {
            toastService.ShowToast(payload.Title, payload.Content);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500);
        }
    }
}

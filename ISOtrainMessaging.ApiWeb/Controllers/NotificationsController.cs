using ISOtrainMessaging.BussinesLayer.Interface;
using ISOtrainMessaging.Model;
using Microsoft.AspNetCore.Mvc;

namespace ISOtrainMessaging.ApiWeb.Controllers
{
    [ApiController]
    [Route("api/notifications")]
    public class NotificationsController : ControllerBase
    {
        private readonly ISenderBL _sender;

        public NotificationsController(ISenderBL sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// Envío de notificaciones de acuerdo al tipo indicado Type = ( Email, Push, SMS)
        /// Para este ejemplo solo se implementó SMTP
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("sendNotification")]
        public async Task<IActionResult> SendNotification(NotificationRequest request)
        {
            var hayError = await _sender.SendNotificationAsync(request);

            if (hayError)
            {
                return BadRequest();
            }
            return Ok(new { Status = "Notificación enviada con éxito" });
        }
    }
}

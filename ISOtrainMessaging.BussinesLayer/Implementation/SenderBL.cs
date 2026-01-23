using ISOtrainMessaging.BussinesLayer.Interface;
using ISOtrainMessaging.Model;
using ISOtrainMessaging.TemplateService.Interface;

namespace ISOtrainMessaging.BussinesLayer.Implementation
{
    public class SenderBL : ISenderBL
    {
        private readonly ITemplateService _templateService;
        private readonly IEnumerable<ISendingStrategy> _strategies;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="templateService"></param>
        public SenderBL(IEnumerable<ISendingStrategy> strategies, ITemplateService templateService)
        {
            _strategies = strategies;
            _templateService = templateService;
        }

        /// <summary>
        /// Enviar mensaje de notificación por el canal según la solicitud
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        public async Task<bool> SendNotificationAsync(NotificationRequest notification)
        {
            // 1. Obtener el cuerpo del mensaje (Plantilla vs mensaje Directo)
            string finalBody = !string.IsNullOrEmpty(notification.TemplateId)
                ? _templateService.Render(notification.TemplateId, notification.TemplateData ?? new())
                : notification.Body ?? "";

            //Para este ejemplo, sobreescribimos los parámetros enviados en la solicitud
            notification.Subject = "Prueba concepto ISOtrainMessaging Service";
            notification.Body = "Esta es una prueba de concepto";
            notification.To = "framirez@softekexport.com";
            finalBody = notification.Body;

            // 2. Buscar canal por donde enviar notificación según la solicitud
            var strategy = _strategies.FirstOrDefault(s => s.SupportedChannel == notification.Type);

            if (strategy == null) throw new HttpRequestException("Canal no soportado");

            //Registrar notificación en BD
            #region Guardar en BD
            //TODO
            #endregion

            // 3. Enviar notificación
            var response = await strategy.SendAsync(notification.To, notification.Subject, finalBody);

            return response;
        }
    }
}

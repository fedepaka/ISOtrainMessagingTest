namespace ISOtrainMessaging.Model
{
    public enum MessageType { Email, Push, SMS }

    /// <summary>
    /// Define datos necasarios para el mensake
    /// </summary>
    public class NotificationRequest
    {
        /// <summary>
        /// Tipo de mensaje: email, SMS, Push etc.
        /// </summary>
        public MessageType Type { get; set; }

        // Opción A: Usar Plantilla
        #region Template

        /// <summary>
        /// Identificador de template de email
        /// </summary>
        public string? TemplateId { get; set; }

        /// <summary>
        /// Colección de datos para completar template
        /// </summary>
        public Dictionary<string, string>? TemplateData { get; set; }
        #endregion

        // Opción B: Mensaje Directo
        #region Direct Message

        /// <summary>
        /// Destinatario
        /// </summary>
        public string To { get; set; } = string.Empty;

        /// <summary>
        /// Asunto
        /// </summary>
        public string Subject { get; set; } = string.Empty;

        /// <summary>
        /// Cuerpo del mensaje
        /// </summary>
        public string Body { get; set; } = string.Empty;
        #endregion

        
    }
}

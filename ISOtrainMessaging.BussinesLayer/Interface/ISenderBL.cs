using ISOtrainMessaging.Model;

namespace ISOtrainMessaging.BussinesLayer.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISenderBL
    {
        Task<bool> SendNotificationAsync(NotificationRequest notification);
    }
}

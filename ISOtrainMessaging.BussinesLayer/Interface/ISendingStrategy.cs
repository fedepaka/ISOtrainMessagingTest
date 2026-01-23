using ISOtrainMessaging.Model;

namespace ISOtrainMessaging.BussinesLayer.Interface
{
    public interface ISendingStrategy
    {
        MessageType SupportedChannel { get; }

        Task<bool> SendAsync(string to, string subject, string body);
    }
}

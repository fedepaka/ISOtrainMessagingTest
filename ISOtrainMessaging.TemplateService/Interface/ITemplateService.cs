namespace ISOtrainMessaging.TemplateService.Interface
{
    public interface ITemplateService
    {
        string Render(string templateId, object data);
    }
}

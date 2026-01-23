using HandlebarsDotNet;
using ISOtrainMessaging.TemplateService.Interface;

namespace ISOtrainMessaging.TemplateService.Implementation
{
    public class TemplateService : ITemplateService
    {
        private const string IKAT_TEMP_BIENVENIDA = "Bienvenida";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="templateId"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public string Render(string templateId, object data)
        {
            // En un caso real, leerías el archivo .html desde una carpeta o desde tabla de BD
            // Por ahora, simulamos una lectura de archivo:
            string source = templateId == IKAT_TEMP_BIENVENIDA
                ? "<h1>Hola {{Nombre}}!</h1><p>Bienvenido a {{App}}.</p>"
                : "Contenido genérico: {{Mensaje}}";

            var template = Handlebars.Compile(source);
            return template(data);
        }
    }
}

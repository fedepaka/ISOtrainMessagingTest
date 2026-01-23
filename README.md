Proyecto realizado en VS 2026
.net 9

1-Antes de ejecutar la applicación se deben agragar configuraciones necesarias en appsettings.json
....
"NotificationSettings": {
  "Smtp": {
    "Server": "smtp.gmail.com",
    "Port": 587,
    "SenderName": "ISOtrain Messaging Service",
    "SenderEmail": "no-reply@tuempresa.com",
    "Username": "tu_email@gmail.com", //usar cuenta de gmail
    "Password": "tu_pass", //password de pruebas generado por gmail
    "EnableSsl": true
  }

  Los anteriores:
  - username y password: para esta prueba se utilizaron los que proporciona gmail con una cuenta válida y generando password de prueba.

2- La clase Sender.BL actualmente tiene configurado hardcoded las propiedades,  subject, body y to.
Esto de hizo para tener un control estricto del destinatario y el mensaje enviado (evitar prueba libre de envío de mail)

3- Ejecutar la aplicación y desde swagger llamar al método "sendNotification"
<img width="1584" height="806" alt="image" src="https://github.com/user-attachments/assets/62178a17-d855-4ccc-9805-c222238fdb04" />
No es necesario asignar valor a los parámetros (ejecutarlo tal y como está la anterior captura de pantalla respetando el parámetro "type" = "Email")

Nota: 
- Pendiente de almacenar o registrar emails en tablas de notificaciones de ISOtrain
- Pendiente manejo de Cola de mensajes (RabbitMQ o Azure Service Bus)
- Manejo de errores http
- Manejo de logs

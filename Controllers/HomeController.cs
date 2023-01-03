using Twilio;
using Twilio.AspNet.Common;
using Twilio.AspNet.Mvc;
using Twilio.Rest.Api.V2010.Account;
using Twilio.TwiML;
using Twilio.Types;

namespace WebApplicationTwilio.Controllers
{
    public class HomeController : TwilioController
    {
        public TwiMLResult Index(SmsRequest incomingMessage)
        {
            TwilioClient.Init("", "");
            if (incomingMessage.Body == null)
                Send("Hello, from Twilio!"); 
            else 
                Receive(incomingMessage);
            var response = new VoiceResponse();
            response.Say("Twilio POC");
            return TwiML(response);
        }
        private void Send(string body)
        {
            MessageResource.Create(
                body: body,
                from: new PhoneNumber("whatsapp:"),
                to: new PhoneNumber("whatsapp:")
            );
        }
        private void SendWithOptions(string body)
        {
            var messageOptions = new CreateMessageOptions(new PhoneNumber("whatsapp:"))
            {
                From = new PhoneNumber("whatsapp:"),
                Body = body
            };
            MessageResource.Create(messageOptions);
        }
        private void Receive(SmsRequest incomingMessage)
        {
            Send($"You said: {incomingMessage.Body}");
        }
    }
}


using Company.G03.PL.Settings;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace Company.G03.PL.Helpers
{
    public class TwilioService(IOptions<TwilioSettings> _options) : ITwilioService
    {

        public MessageResource SendSms(Sms sms)
        {
            // Initialize Connection
            TwilioClient.Init(_options.Value.AccountSID, _options.Value.AuthToken);

            // Build Message
            var message = MessageResource.Create(
                body: sms.Body,
                to: sms.To,
                from: new Twilio.Types.PhoneNumber( _options.Value.PhoneNumber)
            );

            // Retrun Message
            return message;
        }
    }
}

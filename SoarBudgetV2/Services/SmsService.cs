using Repository.Models;
using SoarBudgetV2.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace SoarBudgetV2.Services
{
    public class SmsService : ISmsServices
    {
        private readonly string Account_SID = Api_Keys.Account_SID;
        private readonly string AUTH_TOKEN = Api_Keys.AUTH_TOKEN;
        private readonly string TwilioPhoneNumber = Api_Keys.TwilioPhoneNumber;
        public SmsService()
        {

        }

        public async Task SendSMS(Budgeteer budgeteer, List<string> upcomingBills)
        {
            string messageBody = "";
            foreach(var bill in upcomingBills)
            {
                messageBody += $"-{bill + "\n"}";
            }

            TwilioClient.Init(Account_SID, AUTH_TOKEN);

            var message = await MessageResource.CreateAsync(
                body: messageBody,
                from: new Twilio.Types.PhoneNumber(TwilioPhoneNumber),
                to: new Twilio.Types.PhoneNumber($"1{budgeteer.PhoneNumber}")
            );
        }
    }
}

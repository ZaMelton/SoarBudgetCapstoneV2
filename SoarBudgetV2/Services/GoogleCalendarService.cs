using Google.Apis.Auth.OAuth2;
using Google.Apis.Calendar.v3;
using Google.Apis.Calendar.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Repository.Models;
using SoarBudgetV2.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SoarBudgetV2.Services
{
    public class GoogleCalendarService : IGoogleCalendarServices
    {
        static string[] Scopes = { CalendarService.Scope.Calendar };
        static string ApplicationName = "Soar Budget";

        public GoogleCalendarService()
        {

        }

        public void AddBillEvent(Bill bill)
        {
            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {

                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            EventsResource.ListRequest request = service.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            var billEvent = new Event();
            EventDateTime start = new EventDateTime();
            start.DateTime = bill.DueDate;

            EventDateTime end = new EventDateTime();
            end.DateTime = bill.DueDate.AddMinutes(30);

            billEvent.Start = start;
            billEvent.End = end;
            billEvent.Summary = $"{bill.BillName} due date.";
            billEvent.Description = $"Your {bill.BillName} is due!"; ;

            var calendarId = "primary";
            billEvent = service.Events.Insert(billEvent, calendarId).Execute();
        }

        public void AddDebtItemEvent(DebtItem debtItem)
        {
            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            var service = new CalendarService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            EventsResource.ListRequest request = service.Events.List("primary");
            request.TimeMin = DateTime.Now;
            request.ShowDeleted = false;
            request.SingleEvents = true;
            request.MaxResults = 10;
            request.OrderBy = EventsResource.ListRequest.OrderByEnum.StartTime;

            var debtItemEvent = new Event();
            EventDateTime start = new EventDateTime();
            start.DateTime = debtItem.DueDate;

            EventDateTime end = new EventDateTime();
            end.DateTime = debtItem.DueDate.AddMinutes(30);

            debtItemEvent.Start = start;
            debtItemEvent.End = end;
            debtItemEvent.Summary = $"{debtItem.DebtItemName} due date";
            debtItemEvent.Description = $"Your {debtItem.DebtItemName} is due!"; ;

            var calendarId = "primary";
            debtItemEvent = service.Events.Insert(debtItemEvent, calendarId).Execute();
        }
    }
}

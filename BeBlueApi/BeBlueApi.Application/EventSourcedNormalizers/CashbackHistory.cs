using BeblueApi.Domain.Core.Events;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BeBlueApi.Application.EventSourcedNormalizers
{
    public class CashbackHistory
    {
        public static IList<CashbackHistoryData> HistoryData { get; set; }

        public static IList<CashbackHistoryData> ToJavaScriptCashbackHistory(IList<StoredEvent> storedEvents)
        {
            HistoryData = new List<CashbackHistoryData>();
            CashbackHistoryDeserializer(storedEvents);

            var sorted = HistoryData.OrderBy(c => c.When);
            var list = new List<CashbackHistoryData>();
            var last = new CashbackHistoryData();

            foreach (var change in sorted)
            {
                var jsSlot = new CashbackHistoryData
                {
                    Id = change.Id == Guid.Empty.ToString() || change.Id == last.Id
                        ? ""
                        : change.Id,
                    IdGender = string.IsNullOrWhiteSpace(change.IdGender) || change.IdGender == last.IdGender
                        ? ""
                        : change.IdGender,
                    WeekDay = string.IsNullOrWhiteSpace(change.WeekDay) || change.WeekDay == last.WeekDay
                        ? ""
                        : change.WeekDay,
                    Percent = string.IsNullOrWhiteSpace(change.Percent) || change.Percent == last.Percent
                        ? ""
                        : change.Percent,
                    Action = string.IsNullOrWhiteSpace(change.Action) ? "" : change.Action,
                    When = change.When,
                    Who = change.Who
                };

                list.Add(jsSlot);
                last = change;
            }
            return list;
        }

        private static void CashbackHistoryDeserializer(IEnumerable<StoredEvent> storedEvents)
        {
            foreach (var e in storedEvents)
            {
                var slot = new CashbackHistoryData();
                dynamic values;

                switch (e.MessageType)
                {
                    case "CashbackRegisteredEvent":
                        values = JsonConvert.DeserializeObject<dynamic>(e.Data);
                        slot.Percent = values["Percent"];
                        slot.WeekDay = values["WeekDay"];
                        slot.IdGender = values["IdGender"];
                        slot.Action = "Registered";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;
                    case "CashbackUpdatedEvent":
                        values = JsonConvert.DeserializeObject<dynamic>(e.Data);
                        slot.Percent = values["Percent"];
                        slot.WeekDay = values["WeekDay"];
                        slot.IdGender = values["IdGender"];
                        slot.Action = "Updated";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;
                    case "CashbackRemovedEvent":
                        values = JsonConvert.DeserializeObject<dynamic>(e.Data);
                        slot.Action = "Removed";
                        slot.When = values["Timestamp"];
                        slot.Id = values["Id"];
                        slot.Who = e.User;
                        break;
                }
                HistoryData.Add(slot);
            }
        }
    }
}

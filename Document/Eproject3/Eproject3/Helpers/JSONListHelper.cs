using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eproject3.Helpers
{
    public static class JSONListHelper
    {
        public static string GetEventListJSONString(List<Models.Event> events)
        {
            var eventlist = new List<Event>();
            foreach (var model in events)
            {
                var myevent = new Event()
                {
                    id = model.Id,
                    start = model.StartTime,
                    end = model.EndTime,
                    resourceId = model.Labs.LabsId,
                    description = model.Name,
                    title = model.Labs.LabsName
                };
                eventlist.Add(myevent);
            }
            return System.Text.Json.JsonSerializer.Serialize(eventlist);
        }

        public static string GetResourceListJSONString(List<Models.Lab> labs)
        {
            var resourcelist = new List<Resource>();

            foreach (var la in labs)
            {
                var resource = new Resource()
                {
                    id = la.LabsId,
                    title = la.LabsName
                };
                resourcelist.Add(resource);
            }
            return System.Text.Json.JsonSerializer.Serialize(resourcelist);
        }
    }

    public class Event
    {
        public int id { get; set; }
        public string title { get; set; }
        public DateTime start { get; set; }
        public DateTime end { get; set; }
        public int resourceId { get; set; }
        public string description { get; set; }
    }

    public class Resource
    {
        public int id { get; set; }
        public string title { get; set; }

    }
}
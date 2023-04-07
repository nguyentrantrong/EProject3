using Eproject3.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Eproject3.Models.ViewModels
{
    public class EventViewModel
    {
        public Event Event { get; set; }
        public List<SelectListItem> Lab = new List<SelectListItem>();
        public string LabName { get; set; }

        public EventViewModel(Event myevent, List<Lab> labs)
        {
            Event = myevent;
            LabName = myevent.Labs.LabsName;
            foreach (var la in labs)
            {
                Lab.Add(new SelectListItem() { Text = la.LabsName });
            }
        }

        public EventViewModel(List<Lab> labs)
        {
            foreach (var la in labs)
            {
                Lab.Add(new SelectListItem() { Text = la.LabsName });
            }
        }

        public EventViewModel()
        {

        }
    }
}
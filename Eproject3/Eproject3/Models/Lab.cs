using System;
using System.Collections.Generic;

namespace Eproject3.Models
{
    public partial class Lab
    {
        public Lab()
        {
            Devices = new HashSet<Device>();
            Events = new HashSet<Event>();
            Slots = new HashSet<Slot>();
        }

        public int LabsId { get; set; }
        public string LabsName { get; set; }
        public int? Quantity { get; set; }

        public virtual ICollection<Device> Devices { get; set; }
        public virtual ICollection<Event> Events { get; set; }
        public virtual ICollection<Slot> Slots { get; set; }
    }
}

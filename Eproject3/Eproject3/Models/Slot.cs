using System;
using System.Collections.Generic;

namespace Eproject3.Models
{
    public partial class Slot
    {
        public int SlotId { get; set; }
        public DateTime? Day { get; set; }
        public string Slot1 { get; set; } = null!;
        public int LabId { get; set; }
        public string AdminsId { get; set; } = null!;
        public virtual Admin Admins { get; set; } = null!;
        public virtual Lab Lab { get; set; } = null!;
    }
}

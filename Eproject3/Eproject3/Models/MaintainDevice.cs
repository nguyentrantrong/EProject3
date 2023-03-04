using System;
using System.Collections.Generic;

namespace Eproject3.Models
{
    public partial class MaintainDevice
    {
        public int MaintnId { get; set; }
        public string? Descriptions { get; set; }
        public string? Reason { get; set; }
        public DateTime? Date { get; set; }
        public string? Creater { get; set; }
        public string DevicesId { get; set; } = null!;
        public string Id { get; set; } = null!;
        public string? Status { get; set; }
        public int? Step { get; set; }
        public bool? IsFinished { get; set; }

        public virtual Device Devices { get; set; } = null!;
        public virtual Admin IdNavigation { get; set; } = null!;
    }
}

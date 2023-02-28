using System;
using System.Collections.Generic;

namespace Eproject3.Models
{
    public partial class MaintainceDevice
    {
        public int MaintnId { get; set; }
        public string Descriptions { get; set; } = null!;
        public string Reason { get; set; } = null!;
        public DateTime Date { get; set; }
        public string Creater { get; set; } = null!;
        public string DevicesId { get; set; } = null!;
        public string Id { get; set; } = null!;

        public virtual Device Devices { get; set; } = null!;
        public virtual Admin IdNavigation { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;

namespace Eproject3.Models
{
    public partial class MaintainceDevice
    {
        public int MaintnId { get; set; }
        public string Descriptions { get; set; }
        public string Reason { get; set; }
        public DateTime Date { get; set; }
        public string Creater { get; set; }
        public int DevicesId { get; set; }
        public string Status { get; set; }
        public int Step { get; set; }
        public bool isFinished { get; set; }

        public virtual Device Devices { get; set; }

    }
}

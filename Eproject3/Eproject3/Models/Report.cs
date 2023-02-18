using System;
using System.Collections.Generic;

namespace Eproject3.Models
{
    public partial class Report
    {
        public int ReportId { get; set; }
        public string? Descriptions { get; set; }
        public DateTime ReportDate { get; set; }
        public string Reciver { get; set; } = null!;
        public int ComplainId { get; set; }
        public string DevicesId { get; set; } = null!;

        public virtual Complain Complain { get; set; } = null!;
        public virtual Device Devices { get; set; } = null!;
    }
}

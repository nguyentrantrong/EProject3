using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eproject3.Models
{
    public partial class Device
    {
        public Device()
        {
            Reports = new HashSet<Report>();
        }
        [Key]
        public string DevicesId { get; set; } 
        public string DeviceName { get; set; } 
        public string DeviceType { get; set; }
        public string Status { get; set; }
        public string DateMaintance { get; set; }
        public string DeviceImg { get; set; }
        public int LabsId { get; set; }
        public int Supplier_ID { get; set; }

        public virtual Lab Labs { get; set; } 
        public virtual Supplier Supplier { get; set; } 
        public virtual ICollection<Report> Reports { get; set; }
    }
}

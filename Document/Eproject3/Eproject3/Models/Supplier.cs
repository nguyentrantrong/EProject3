using System;
using System.Collections.Generic;

namespace Eproject3.Models
{
    public partial class Supplier
    {
        public Supplier()
        {
            Devices = new HashSet<Device>();
        }

        public int SupplierId { get; set; }
        public string SupplierName { get; set; } = null!;

        public virtual ICollection<Device> Devices { get; set; }
    }
}

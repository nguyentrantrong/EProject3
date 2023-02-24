using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eproject3.Models
{
    [Table("Suppliers")]
    public partial class Supplier
    {
        public Supplier()
        {
            Devices = new HashSet<Device>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Supplier_ID { get; set; }
        public string SupplierName { get; set; } = null!;

        public virtual ICollection<Device> Devices { get; set; }
    }
}

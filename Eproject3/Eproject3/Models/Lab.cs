using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eproject3.Models
{
    [Table("Labs")]
    public partial class Lab
    {
        public Lab()
        {
            Devices = new HashSet<Device>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LabsId { get; set; }
        public string? LabsName { get; set; }

        public virtual ICollection<Device> Devices { get; set; }
    }
}

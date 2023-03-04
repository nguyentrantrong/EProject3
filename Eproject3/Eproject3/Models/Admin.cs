using System;
using System.Collections.Generic;

namespace Eproject3.Models
{
    public partial class Admin
    {
        public Admin()
        {
            Complains = new HashSet<Complain>();
            MaintainDevices = new HashSet<MaintainDevice>();
            Slots = new HashSet<Slot>();
        }

        public string Id { get; set; } = null!;
        public string AdminName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;

        public virtual ICollection<Complain> Complains { get; set; }
        public virtual ICollection<MaintainDevice> MaintainDevices { get; set; }
        public virtual ICollection<Slot> Slots { get; set; }
    }
}

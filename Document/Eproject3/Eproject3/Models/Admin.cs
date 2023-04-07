using System;
using System.Collections.Generic;

namespace Eproject3.Models
{
    public partial class Admin
    {
        public Admin()
        {
            Slots = new HashSet<Slot>();
        }

        public string Id { get; set; } = null!;
        public string AdminName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;
        public bool IsActive { get; set; }

        public virtual ICollection<Slot> Slots { get; set; }
    }
}

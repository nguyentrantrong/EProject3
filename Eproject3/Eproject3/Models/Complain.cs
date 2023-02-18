using System;
using System.Collections.Generic;

namespace Eproject3.Models
{
    public partial class Complain
    {
        public Complain()
        {
            Reports = new HashSet<Report>();
        }

        public int ComplainId { get; set; }
        public string Description { get; set; } = null!;
        public int UsersId { get; set; }
        public DateTime? DateComplaint { get; set; }

        public virtual User Users { get; set; } = null!;
        public virtual ICollection<Report> Reports { get; set; }
    }
}

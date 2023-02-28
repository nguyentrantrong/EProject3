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
        public string Reason { get; set; } = null!;
        public string StatusCp { get; set; } = null!;
        public DateTime DateCp { get; set; }
        public string Category { get; set; } = null!;
        public string Id { get; set; } = null!;

        public virtual Admin IdNavigation { get; set; } = null!;
        public virtual ICollection<Report> Reports { get; set; }
    }
}

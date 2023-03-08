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
        public string Description { get; set; } 
        public string Reason { get; set; } 
        public string StatusCp { get; set; } 
        public DateTime DateCp { get; set; }
        public string Category { get; set; }
        public string Id { get; set; }
        public string Reply { get; set; } = null!;

        public virtual Admin IdNavigation { get; set; } = null!; 
        public virtual ICollection<Report> Reports { get; set; }
    }
}

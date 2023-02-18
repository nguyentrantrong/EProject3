using System;
using System.Collections.Generic;

namespace Eproject3.Models
{
    public partial class User
    {
        public User()
        {
            Complains = new HashSet<Complain>();
        }

        public int UsersId { get; set; }
        public string Username { get; set; } = null!;
        public string Passwords { get; set; } = null!;
        public string Email { get; set; } = null!;

        public virtual ICollection<Complain> Complains { get; set; }
    }
}

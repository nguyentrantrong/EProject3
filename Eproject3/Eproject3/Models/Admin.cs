using System;
using System.Collections.Generic;

namespace Eproject3.Models
{
    public partial class Admin
    {
        public string Id { get; set; } = null!;
        public string AdminName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace agendamentosmanager_api.Models
{
    public partial class User
    {
        public int Id { get; set; }

        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Role { get; set; } = null!;

        public string Token { get; set; } = null!;
    }
}
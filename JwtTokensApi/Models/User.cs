using System;
using System.Collections.Generic;

#nullable disable

namespace JwtTokensApi.Models
{
    public partial class User
    {
        public User()
        {
            RefreshTokens = new HashSet<RefreshToken>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public int RoleId { get; set; }

        public virtual Role Role { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }
    }
}

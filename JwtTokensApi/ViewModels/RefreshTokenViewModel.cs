using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtTokensApi.ViewModels
{
    public class RefreshTokenViewModel
    {
        public int RefreshTokenId { get; set; }
        public string Token { get; set; }
        public DateTime ExpiryOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? RevokedOn { get; set; }
        public int CreatedBy { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JwtTokensApi.ViewModels
{
    public class UpdateRoleViewModel
    {
        [Required]
        public int RoleId { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}

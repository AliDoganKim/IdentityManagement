using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityManagement.Server.Model
{
    public class ChangePasswordModel
    {
        public int Id { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string PasswordAgain { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace smack.application.DTOs.User
{
    public class UserDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public int UserType { get; set; }

        public string Email { get; set; }
    }
}

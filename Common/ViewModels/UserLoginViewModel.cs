using System;
using System.Collections.Generic;
using System.Text;


namespace Common.ViewModels
{
    public class UserLoginViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public RoleViewModel Role { get; set; }
    }
}

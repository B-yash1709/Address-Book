using ModelLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Interface
{
    public interface IAuthService
    {
        public ResponseModelSMD<UserResponse> Register(UserRequest dto);
        public ResponseModelSMD<UserResponse> Login(LoginDTO dto);
        public ResponseModelSMD<string> ForgotPassword(ForgotPasswordDTO model);
        public ResponseModelSMD<string> ResetPassword(ResetPasswordDTO model);
    }
}

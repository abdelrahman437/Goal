using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goal.Core.DTO;

namespace Goal.Core.Interfaces.Services
{
    public interface IAuthServices
    {
        Task<AuthModel> RegisterAsync(RegisterDTO registerModel);
        Task<AuthModel> login(LoginDTO login);
    }
}

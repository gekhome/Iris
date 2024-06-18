using Iris.Models;
using System.Collections.Generic;

namespace Iris.Services
{
    public interface IAdminAccountService
    {
        void Create(UserAdminViewModel data);
        void Destroy(UserAdminViewModel data);
        IEnumerable<UserAdminViewModel> Read();
        void Update(UserAdminViewModel data);
    }
}
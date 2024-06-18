using Iris.Models;
using System.Collections.Generic;

namespace Iris.Services
{
    public interface IAdmin2AccountService
    {
        void Create(UserAdmin2ViewModel data);
        void Destroy(UserAdmin2ViewModel data);
        IEnumerable<UserAdmin2ViewModel> Read();
        void Update(UserAdmin2ViewModel data);
    }
}
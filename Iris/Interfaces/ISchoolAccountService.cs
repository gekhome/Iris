using Iris.Models;
using System.Collections.Generic;

namespace Iris.Services
{
    public interface ISchoolAccountService
    {
        void Create(UserSchoolViewModel data);
        void Destroy(UserSchoolViewModel data);
        IEnumerable<UserSchoolViewModel> Read();
        UserSchoolViewModel Refresh(int entityId);
        void Update(UserSchoolViewModel data);
    }
}
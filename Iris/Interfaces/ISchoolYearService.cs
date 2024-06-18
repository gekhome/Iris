using Iris.Models;
using System.Collections.Generic;

namespace Iris.Services
{
    public interface ISchoolYearService
    {
        void Create(SysSchoolYearViewModel data);
        void Destroy(SysSchoolYearViewModel data);
        IEnumerable<SysSchoolYearViewModel> Read();
        SysSchoolYearViewModel Refresh(int entityId);
        void Update(SysSchoolYearViewModel data);
    }
}
using Iris.Models;
using System.Collections.Generic;

namespace Iris.Services
{
    public interface ISchoolService
    {
        void Create(SchoolsGridViewModel data);
        void Destroy(SchoolsGridViewModel data);
        IEnumerable<SchoolsGridViewModel> Read(string dir);
        SchoolsGridViewModel Refresh(int entityId);
        void Update(SchoolsGridViewModel data);
    }
}
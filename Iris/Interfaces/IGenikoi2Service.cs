using Iris.Models;
using System.Collections.Generic;

namespace Iris.Services
{
    public interface IGenikoi2Service
    {
        void Create(DirectorGeneralViewModel data);
        void Destroy(DirectorGeneralViewModel data);
        IEnumerable<DirectorGeneralViewModel> Read();
        DirectorGeneralViewModel Refresh(int entityId);
        void Update(DirectorGeneralViewModel data);
    }
}
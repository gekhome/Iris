using Iris.Models;
using System.Collections.Generic;

namespace Iris.Services
{
    public interface IPeriferiakesService
    {
        void Create(SysPeriferiakiViewModel data);
        void Destroy(SysPeriferiakiViewModel data);
        IEnumerable<SysPeriferiakiViewModel> Read();
        SysPeriferiakiViewModel Refresh(int entityId);
        void Update(SysPeriferiakiViewModel data);
    }
}
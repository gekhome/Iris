using Iris.Models;
using System.Collections.Generic;

namespace Iris.Services
{
    public interface IEidikotitesService
    {
        void Create(SysEidikotitesViewModel data);
        void Destroy(SysEidikotitesViewModel data);
        IEnumerable<SysEidikotitesViewModel> Read();
        SysEidikotitesViewModel Refresh(int entityId);
        void Update(SysEidikotitesViewModel data);
    }
}
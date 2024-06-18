using Iris.Models;
using System.Collections.Generic;

namespace Iris.Services
{
    public interface IEidikotitesOldService
    {
        void Create(SysEidikotitesOldViewModel data);
        void Destroy(SysEidikotitesOldViewModel data);
        IEnumerable<SysEidikotitesOldViewModel> Read();
        SysEidikotitesOldViewModel Refresh(int entityId);
        void Update(SysEidikotitesOldViewModel data);
    }
}
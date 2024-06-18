using Iris.Models;
using System.Collections.Generic;

namespace Iris.Services
{
    public interface IEpitropesApofasiService
    {
        void Create(SysEpitropesViewModel data, int schoolyearId, int periferiakiId);
        void Destroy(SysEpitropesViewModel data);
        IEnumerable<SysEpitropesViewModel> Read(int schoolyearId, int periferiakiId);
        SysEpitropesViewModel Refresh(int entityId);
        void Update(SysEpitropesViewModel data, int schoolyearId, int periferiakiId);
    }
}
using Iris.Models;
using System.Collections.Generic;

namespace Iris.ServicesApofaseis
{
    public interface IApofasiInitialService
    {
        void Create(ApofasiInitialGridViewModel data);
        void Destroy(ApofasiInitialGridViewModel data);
        ApofasiInitialViewModel GetRecord(int apofasiId);
        IEnumerable<ApofasiInitialGridViewModel> Read(int schoolyearId = 0, int adminId = 0);
        IEnumerable<ApofasiInitialGridViewModel> Read2(int schoolyearId = 0, int adminId = 0);
        ApofasiInitialGridViewModel Refresh(int entityId);
        void Update(ApofasiInitialGridViewModel data);
        void UpdateRecord(ApofasiInitialViewModel data, int apofasiId, int departement);
    }
}
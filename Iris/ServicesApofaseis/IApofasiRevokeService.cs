using Iris.Models;
using System.Collections.Generic;

namespace Iris.ServicesApofaseis
{
    public interface IApofasiRevokeService
    {
        void Create(ApofasiRevokeGridViewModel data);
        void Destroy(ApofasiRevokeGridViewModel data);
        ApofasiRevokeViewModel GetRecord(int apofasiId);
        IEnumerable<ApofasiRevokeGridViewModel> Read(int schoolyearId = 0, int adminId = 0);
        IEnumerable<ApofasiRevokeGridViewModel> Read2(int schoolyearId = 0, int adminId = 0);
        ApofasiRevokeGridViewModel Refresh(int entityId);
        void Update(ApofasiRevokeGridViewModel data);
        void UpdateRecord(ApofasiRevokeViewModel data, int apofasiId, int departement);
    }
}
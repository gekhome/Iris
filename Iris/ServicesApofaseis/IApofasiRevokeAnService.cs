using Iris.Models;
using System.Collections.Generic;

namespace Iris.ServicesApofaseis
{
    public interface IApofasiRevokeAnService
    {
        void Create(ApofasiRevokeAnaplirotesGridViewModel data);
        void Destroy(ApofasiRevokeAnaplirotesGridViewModel data);
        ApofasiRevokeAnaplirotesViewModel GetRecord(int apofasiId);
        IEnumerable<ApofasiRevokeAnaplirotesGridViewModel> Read(int schoolyearId = 0, int adminId = 0);
        ApofasiRevokeAnaplirotesGridViewModel Refresh(int entityId);
        void Update(ApofasiRevokeAnaplirotesGridViewModel data);
        void UpdateRecord(ApofasiRevokeAnaplirotesViewModel data, int apofasiId, int departement);
    }
}
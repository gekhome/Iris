using Iris.Models;
using System.Collections.Generic;

namespace Iris.ServicesApofaseis
{
    public interface IApofasiCancelAnService
    {
        void Create(ApofasiCancelAnaplirotesGridViewModel data);
        void Destroy(ApofasiCancelAnaplirotesGridViewModel data);
        ApofasiCancelAnaplirotesViewModel GetRecord(int apofasiId);
        IEnumerable<ApofasiCancelAnaplirotesGridViewModel> Read(int schoolyearId = 0, int adminId = 0);
        ApofasiCancelAnaplirotesGridViewModel Refresh(int entityId);
        void Update(ApofasiCancelAnaplirotesGridViewModel data);
        void UpdateRecord(ApofasiCancelAnaplirotesViewModel data, int apofasiId, int departement);
    }
}
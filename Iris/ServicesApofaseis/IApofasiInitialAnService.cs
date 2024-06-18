using Iris.Models;
using System.Collections.Generic;

namespace Iris.ServicesApofaseis
{
    public interface IApofasiInitialAnService
    {
        void Create(ApofasiInitialAnaplirotesGridViewModel data);
        void Destroy(ApofasiInitialAnaplirotesGridViewModel data);
        ApofasiInitialAnaplirotesViewModel GetRecord(int apofasiId);
        IEnumerable<ApofasiInitialAnaplirotesGridViewModel> Read(int schoolyearId = 0, int adminId = 0);
        ApofasiInitialAnaplirotesGridViewModel Refresh(int entityId);
        void Update(ApofasiInitialAnaplirotesGridViewModel data);
        void UpdateRecord(ApofasiInitialAnaplirotesViewModel data, int apofasiId, int departement);
    }
}
using Iris.Models;
using System.Collections.Generic;

namespace Iris.ServicesApofaseis
{
    public interface IApofasiSupplementAnService
    {
        void Create(ApofasiSupplementAnaplirotesGridViewModel data);
        void Destroy(ApofasiSupplementAnaplirotesGridViewModel data);
        ApofasiSupplementAnaplirotesViewModel GetRecord(int apofasiId);
        IEnumerable<ApofasiSupplementAnaplirotesGridViewModel> Read(int schoolyearId = 0, int adminId = 0);
        ApofasiSupplementAnaplirotesGridViewModel Refresh(int entityId);
        void Update(ApofasiSupplementAnaplirotesGridViewModel data);
        void UpdateRecord(ApofasiSupplementAnaplirotesViewModel data, int apofasiId, int departement);
    }
}
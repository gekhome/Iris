using Iris.Models;
using System.Collections.Generic;

namespace Iris.ServicesApofaseis
{
    public interface IApofasiModifyAnService
    {
        void Create(ApofasiModifyAnaplirotesGridViewModel data);
        void Destroy(ApofasiModifyAnaplirotesGridViewModel data);
        ApofasiModifyAnaplirotesViewModel GetRecord(int apofasiId);
        IEnumerable<ApofasiModifyAnaplirotesGridViewModel> Read(int schoolyearId = 0, int adminId = 0);
        ApofasiModifyAnaplirotesGridViewModel Refresh(int entityId);
        void Update(ApofasiModifyAnaplirotesGridViewModel data);
        void UpdateRecord(ApofasiModifyAnaplirotesViewModel data, int apofasiId, int departement);
    }
}
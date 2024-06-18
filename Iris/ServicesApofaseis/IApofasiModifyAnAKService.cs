using Iris.Models;
using System.Collections.Generic;

namespace Iris.ServicesApofaseis
{
    public interface IApofasiModifyAnAKService
    {
        void Create(ApofasiModifyAnaplirotesAKGridViewModel data);
        void Destroy(ApofasiModifyAnaplirotesAKGridViewModel data);
        ApofasiModifyAnaplirotesAKViewModel GetRecord(int apofasiId);
        IEnumerable<ApofasiModifyAnaplirotesAKGridViewModel> Read(int schoolyearId = 0, int adminId = 0);
        ApofasiModifyAnaplirotesAKGridViewModel Refresh(int entityId);
        void Update(ApofasiModifyAnaplirotesAKGridViewModel data);
        void UpdateRecord(ApofasiModifyAnaplirotesAKViewModel data, int apofasiId, int departement);
    }
}
using Iris.Models;
using System.Collections.Generic;

namespace Iris.ServicesApofaseis
{
    public interface IApofasiModifyAKService
    {
        void Create(ApofasiModifyAKGridViewModel data);
        void Destroy(ApofasiModifyAKGridViewModel data);
        ApofasiModifyAKViewModel GetRecord(int apofasiId);
        IEnumerable<ApofasiModifyAKGridViewModel> Read(int schoolyearId = 0, int adminId = 0);
        IEnumerable<ApofasiModifyAKGridViewModel> Read2(int schoolyearId = 0, int adminId = 0);
        ApofasiModifyAKGridViewModel Refresh(int entityId);
        void Update(ApofasiModifyAKGridViewModel data);
        void UpdateRecord(ApofasiModifyAKViewModel data, int apofasiId, int departement);
    }
}
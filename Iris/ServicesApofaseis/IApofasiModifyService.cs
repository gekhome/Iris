using Iris.Models;
using System.Collections.Generic;

namespace Iris.ServicesApofaseis
{
    public interface IApofasiModifyService
    {
        void Create(ApofasiModifyGridViewModel data);
        void Destroy(ApofasiModifyGridViewModel data);
        ApofasiModifyViewModel GetRecord(int apofasiId);
        IEnumerable<ApofasiModifyGridViewModel> Read(int schoolyearId = 0, int adminId = 0);
        IEnumerable<ApofasiModifyGridViewModel> Read2(int schoolyearId = 0, int adminId = 0);
        ApofasiModifyGridViewModel Refresh(int entityId);
        void Update(ApofasiModifyGridViewModel data);
        void UpdateRecord(ApofasiModifyViewModel data, int apofasiId, int departement);
    }
}
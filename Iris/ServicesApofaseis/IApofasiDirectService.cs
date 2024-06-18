using Iris.Models;
using System.Collections.Generic;

namespace Iris.ServicesApofaseis
{
    public interface IApofasiDirectService
    {
        void Create(ApofasiDirectGridViewModel data);
        void Destroy(ApofasiDirectGridViewModel data);
        ApofasiDirectViewModel GetRecord(int apofasiId);
        IEnumerable<ApofasiDirectGridViewModel> Read(int schoolyearId = 0, int adminId = 0);
        IEnumerable<ApofasiDirectGridViewModel> Read2(int schoolyearId = 0, int adminId = 0);
        ApofasiDirectGridViewModel Refresh(int entityId);
        void Update(ApofasiDirectGridViewModel data);
        void UpdateRecord(ApofasiDirectViewModel data, int apofasiId, int departement);
    }
}
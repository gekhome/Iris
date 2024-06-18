using Iris.Models;
using System.Collections.Generic;

namespace Iris.ServicesApofaseis
{
    public interface IApofasiCancelService
    {
        void Create(ApofasiCancelGridViewModel data);
        void Destroy(ApofasiCancelGridViewModel data);
        ApofasiCancelViewModel GetRecord(int apofasiId);
        IEnumerable<ApofasiCancelGridViewModel> Read(int schoolyearId = 0, int adminId = 0);
        IEnumerable<ApofasiCancelGridViewModel> Read2(int schoolyearId = 0, int adminId = 0);
        ApofasiCancelGridViewModel Refresh(int entityId);
        void Update(ApofasiCancelGridViewModel data);
        void UpdateRecord(ApofasiCancelViewModel data, int apofasiId, int departement);
    }
}
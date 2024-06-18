using Iris.Models;
using System.Collections.Generic;

namespace Iris.ServicesApofaseis
{
    public interface IApofasiSupplementAKService
    {
        void Create(ApofasiSupplementAKGridViewModel data);
        void Destroy(ApofasiSupplementAKGridViewModel data);
        ApofasiSupplementAKViewModel GetRecord(int apofasiId);
        IEnumerable<ApofasiSupplementAKGridViewModel> Read(int schoolyearId = 0, int adminId = 0);
        IEnumerable<ApofasiSupplementAKGridViewModel> Read2(int schoolyearId = 0, int adminId = 0);
        ApofasiSupplementAKGridViewModel Refresh(int entityId);
        void Update(ApofasiSupplementAKGridViewModel data);
        void UpdateRecord(ApofasiSupplementAKViewModel data, int apofasiId, int departement);
    }
}
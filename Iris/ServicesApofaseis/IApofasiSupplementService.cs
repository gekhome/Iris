using Iris.Models;
using System.Collections.Generic;

namespace Iris.ServicesApofaseis
{
    public interface IApofasiSupplementService
    {
        void Create(ApofasiSupplementGridViewModel data);
        void Destroy(ApofasiSupplementGridViewModel data);
        ApofasiSupplementViewModel GetRecord(int apofasiId);
        IEnumerable<ApofasiSupplementGridViewModel> Read(int schoolyearId = 0, int adminId = 0);
        IEnumerable<ApofasiSupplementGridViewModel> Read2(int schoolyearId = 0, int adminId = 0);
        ApofasiSupplementGridViewModel Refresh(int entityId);
        void Update(ApofasiSupplementGridViewModel data);
        void UpdateRecord(ApofasiSupplementViewModel data, int apofasiId, int departement);
    }
}
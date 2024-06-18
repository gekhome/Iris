using Iris.Models;
using System.Collections.Generic;

namespace Iris.ServicesApofaseis
{
    public interface IApofasiCorrectService
    {
        void Create(ApofasiCorrectGridViewModel data);
        void Destroy(ApofasiCorrectGridViewModel data);
        ApofasiCorrectViewModel GetRecord(int apofasiId);
        IEnumerable<ApofasiCorrectGridViewModel> Read(int schoolyearId = 0, int adminId = 0);
        IEnumerable<ApofasiCorrectGridViewModel> Read2(int schoolyearId = 0, int adminId = 0);
        ApofasiCorrectGridViewModel Refresh(int entityId);
        void Update(ApofasiCorrectGridViewModel data);
        void UpdateRecord(ApofasiCorrectViewModel data, int apofasiId, int departement);
    }
}
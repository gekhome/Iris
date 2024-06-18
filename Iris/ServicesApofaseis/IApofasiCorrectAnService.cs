using Iris.Models;
using System.Collections.Generic;

namespace Iris.ServicesApofaseis
{
    public interface IApofasiCorrectAnService
    {
        void Create(ApofasiCorrectAnaplirotesGridViewModel data);
        void Destroy(ApofasiCorrectAnaplirotesGridViewModel data);
        ApofasiCorrectAnaplirotesViewModel GetRecord(int apofasiId);
        IEnumerable<ApofasiCorrectAnaplirotesGridViewModel> Read(int schoolyearId = 0, int adminId = 0);
        ApofasiCorrectAnaplirotesGridViewModel Refresh(int entityId);
        void Update(ApofasiCorrectAnaplirotesGridViewModel data);
        void UpdateRecord(ApofasiCorrectAnaplirotesViewModel data, int apofasiId, int departement);
    }
}
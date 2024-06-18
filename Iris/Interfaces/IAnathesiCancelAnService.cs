using Iris.Models;
using System.Collections.Generic;

namespace Iris.Services
{
    public interface IAnathesiCancelAnService
    {
        void Create(AnathesiCancelAnaplirotesViewModel data, int schoolyearId, int schoolId);
        void Destroy(AnathesiCancelAnaplirotesViewModel data);
        IEnumerable<AnathesiCancelAnaplirotesViewModel> Read(ApofasiParameters ap);
        IEnumerable<AnathesiCancelAnaplirotesViewModel> Read(int schoolyearId, int schoolId);
        AnathesiCancelAnaplirotesViewModel Refresh(int entityId);
        void Update(AnathesiCancelAnaplirotesViewModel data, ApofasiParameters ap);
        void Update(AnathesiCancelAnaplirotesViewModel data, int schoolyearId, int schoolId);
    }
}
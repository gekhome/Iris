using Iris.Models;
using System.Collections.Generic;

namespace Iris.Services
{
    public interface IAnathesiSupplementAnService
    {
        void Create(AnathesiSupplementAnaplirotesViewModel data, int schoolyearId, int schoolId);
        void Destroy(AnathesiSupplementAnaplirotesViewModel data);
        IEnumerable<AnathesiSupplementAnaplirotesViewModel> Read(ApofasiParameters ap);
        IEnumerable<AnathesiSupplementAnaplirotesViewModel> Read(int schoolyearId, int schoolId);
        AnathesiSupplementAnaplirotesViewModel Refresh(int entityId);
        void Update(AnathesiSupplementAnaplirotesViewModel data, ApofasiParameters ap);
        void Update(AnathesiSupplementAnaplirotesViewModel data, int schoolyearId, int schoolId);
    }
}
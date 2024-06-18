using Iris.Models;
using System.Collections.Generic;

namespace Iris.Services
{
    public interface IAnathesiModifyAnService
    {
        void Create(AnathesiModifyAnaplirotesViewModel data, int schoolyearId, int schoolId);
        void Destroy(AnathesiModifyAnaplirotesViewModel data);
        IEnumerable<AnathesiModifyAnaplirotesViewModel> Read(ApofasiParameters ap);
        IEnumerable<AnathesiModifyAnaplirotesViewModel> Read(int schoolyearId, int schoolId);
        AnathesiModifyAnaplirotesViewModel Refresh(int entityId);
        void Update(AnathesiModifyAnaplirotesViewModel data, ApofasiParameters ap);
        void Update(AnathesiModifyAnaplirotesViewModel data, int schoolyearId, int schoolId);
    }
}
using Iris.Models;
using System.Collections.Generic;

namespace Iris.Services
{
    public interface IAnathesiModifyAnAKService
    {
        void Create(AnathesiModifyAnaplirotesAKViewModel data, int schoolyearId, int schoolId);
        void Destroy(AnathesiModifyAnaplirotesAKViewModel data);
        IEnumerable<AnathesiModifyAnaplirotesAKViewModel> Read(ApofasiParameters ap);
        IEnumerable<AnathesiModifyAnaplirotesAKViewModel> Read(int schoolyearId, int schoolId);
        AnathesiModifyAnaplirotesAKViewModel Refresh(int entityId);
        void Update(AnathesiModifyAnaplirotesAKViewModel data, ApofasiParameters ap);
        void Update(AnathesiModifyAnaplirotesAKViewModel data, int schoolyearId, int schoolId);
    }
}
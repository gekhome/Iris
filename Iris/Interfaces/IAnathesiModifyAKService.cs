using Iris.Models;
using System.Collections.Generic;

namespace Iris.Services
{
    public interface IAnathesiModifyAKService
    {
        void Create(AnathesiModifyAKViewModel data, int schoolyearId, int schoolId);
        void Destroy(AnathesiModifyAKViewModel data);
        IEnumerable<AnathesiModifyAKViewModel> Read(ApofasiParameters ap);
        IEnumerable<AnathesiModifyAKViewModel> Read(int schoolyearId, int schoolId);
        AnathesiModifyAKViewModel Refresh(int entityId);
        void Update(AnathesiModifyAKViewModel data, ApofasiParameters ap);
        void Update(AnathesiModifyAKViewModel data, int schoolyearId, int schoolId);
    }
}
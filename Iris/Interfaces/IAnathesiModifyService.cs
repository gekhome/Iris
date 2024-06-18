using Iris.Models;
using System.Collections.Generic;

namespace Iris.Services
{
    public interface IAnathesiModifyService
    {
        void Create(AnathesiModifyViewModel data, int schoolyearId, int schoolId);
        void Destroy(AnathesiModifyViewModel data);
        IEnumerable<AnathesiModifyViewModel> Read(ApofasiParameters ap);
        IEnumerable<AnathesiModifyViewModel> Read(int schoolyearId, int schoolId);
        AnathesiModifyViewModel Refresh(int entityId);
        void Update(AnathesiModifyViewModel data, ApofasiParameters ap);
        void Update(AnathesiModifyViewModel data, int schoolyearId, int schoolId);
    }
}
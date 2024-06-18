using Iris.Models;
using System.Collections.Generic;

namespace Iris.Services
{
    public interface IAnathesiSupplementAKService
    {
        void Create(AnathesiSupplementAKViewModel data, int schoolyearId, int schoolId);
        void Destroy(AnathesiSupplementAKViewModel data);
        IEnumerable<AnathesiSupplementAKViewModel> Read(ApofasiParameters ap);
        IEnumerable<AnathesiSupplementAKViewModel> Read(int schoolyearId, int schoolId);
        AnathesiSupplementAKViewModel Refresh(int entityId);
        void Update(AnathesiSupplementAKViewModel data, ApofasiParameters ap);
        void Update(AnathesiSupplementAKViewModel data, int schoolyearId, int schoolId);
    }
}
using Iris.Models;
using System.Collections.Generic;

namespace Iris.Services
{
    public interface IAnathesiSupplementService
    {
        void Create(AnathesiSupplementViewModel data, int schoolyearId, int schoolId);
        void Destroy(AnathesiSupplementViewModel data);
        IEnumerable<AnathesiSupplementViewModel> Read(ApofasiParameters ap);
        IEnumerable<AnathesiSupplementViewModel> Read(int schoolyearId, int schoolId);
        AnathesiSupplementViewModel Refresh(int entityId);
        void Update(AnathesiSupplementViewModel data, ApofasiParameters ap);
        void Update(AnathesiSupplementViewModel data, int schoolyearId, int schoolId);
    }
}
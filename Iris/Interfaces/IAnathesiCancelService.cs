using Iris.Models;
using System.Collections.Generic;

namespace Iris.Services
{
    public interface IAnathesiCancelService
    {
        void Create(AnathesiCancelViewModel data, int schoolyearId, int schoolId);
        void Destroy(AnathesiCancelViewModel data);
        IEnumerable<AnathesiCancelViewModel> Read(ApofasiParameters ap);
        IEnumerable<AnathesiCancelViewModel> Read(int schoolyearId, int schoolId);
        AnathesiCancelViewModel Refresh(int entityId);
        void Update(AnathesiCancelViewModel data, ApofasiParameters ap);
        void Update(AnathesiCancelViewModel data, int schoolyearId, int schoolId);
    }
}
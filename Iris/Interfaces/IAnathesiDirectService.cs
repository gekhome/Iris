using Iris.Models;
using System.Collections.Generic;

namespace Iris.Services
{
    public interface IAnathesiDirectService
    {
        void Create(AnathesiDirectViewModel data, int schoolyearId, int schoolId);
        void Destroy(AnathesiDirectViewModel data);
        IEnumerable<AnathesiDirectViewModel> Read(ApofasiParameters ap);
        IEnumerable<AnathesiDirectViewModel> Read(int schoolyearId, int schoolId);
        AnathesiDirectViewModel Refresh(int entityId);
        void Update(AnathesiDirectViewModel data, ApofasiParameters ap);
        void Update(AnathesiDirectViewModel data, int schoolyearId, int schoolId);
    }
}
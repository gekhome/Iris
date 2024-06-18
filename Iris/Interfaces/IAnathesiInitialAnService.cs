using Iris.Models;
using System.Collections.Generic;

namespace Iris.Services
{
    public interface IAnathesiInitialAnService
    {
        void Create(AnathesiInitialAnaplirotesViewModel data, int schoolyearId, int schoolId);
        void Destroy(AnathesiInitialAnaplirotesViewModel data);
        IEnumerable<AnathesiInitialAnaplirotesViewModel> Read(ApofasiParameters ap);
        IEnumerable<AnathesiInitialAnaplirotesViewModel> Read(int schoolyearId, int schoolId);
        AnathesiInitialAnaplirotesViewModel Refresh(int entityId);
        void Update(AnathesiInitialAnaplirotesViewModel data, ApofasiParameters ap);
        void Update(AnathesiInitialAnaplirotesViewModel data, int schoolyearId, int schoolId);
    }
}
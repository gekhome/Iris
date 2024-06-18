using Iris.Models;
using System.Collections.Generic;

namespace Iris.Services
{
    public interface IAnathesiRevokeAnService
    {
        void Create(AnathesiRevokeAnaplirotesViewModel data, int schoolyearId, int schoolId);
        void Destroy(AnathesiRevokeAnaplirotesViewModel data);
        IEnumerable<AnathesiRevokeAnaplirotesViewModel> Read(ApofasiParameters ap);
        IEnumerable<AnathesiRevokeAnaplirotesViewModel> Read(int schoolyearId, int schoolId);
        AnathesiRevokeAnaplirotesViewModel Refresh(int entityId);
        void Update(AnathesiRevokeAnaplirotesViewModel data, ApofasiParameters ap);
        void Update(AnathesiRevokeAnaplirotesViewModel data, int schoolyearId, int schoolId);
    }
}